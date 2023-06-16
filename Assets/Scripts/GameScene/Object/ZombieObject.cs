using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 丧尸对象
/// </summary>
public class ZombieObject : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private ZombieInfo zombieInfo;

    private int hp;
    public bool isDead = false;

    private float frontAttackTime = 0;  // 记录前一次攻击的时间

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isDead) return;

        animator.SetBool("Run", navMeshAgent.velocity != Vector3.zero); // 检测速度来播放或停止动画

        // 判断距离，以及判断攻击间隔时间
        if (Vector3.Distance(
            transform.position, SafeZoneObject.Instance.transform.position) < 5 &&
            Time.time - frontAttackTime >= zombieInfo.atkOffset)
        {
            frontAttackTime = Time.time;
            animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// 丧尸初始化方法
    /// </summary>
    /// <param name="info">丧尸信息数据</param>
    public void Init(ZombieInfo info)
    {
        if (info == null) return;

        // 获取数据
        zombieInfo = info;

        // 动态加载状态机
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);

        // 血量
        hp = info.hp;

        // 寻路AI
        navMeshAgent.speed = navMeshAgent.acceleration = info.moveSpeed;
        navMeshAgent.angularSpeed = info.rotateSpeed;
    }

    /// <summary>
    /// 丧尸受伤方法
    /// </summary>
    /// <param name="damage">伤害量</param>
    public void Wound(int damage)
    {
        if (isDead) return;

        hp -= damage;
        animator.SetTrigger("Wound");

        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            // 播放受伤音效
            GameDataManager.Instance.PlaySound("Music/Wound");
        }
    }

    /// <summary>
    /// 丧尸死亡方法
    /// </summary>
    public void Dead()
    {
        isDead = true;

        // 停止寻路系统
        //navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        // 播放死亡动画
        animator.SetBool("Dead", true);

        // 播放音效
        GameDataManager.Instance.PlaySound("Music/dead");

        // 加钱
        GameLevelManager.Instance.playerObj.AddMoney(zombieInfo.awardMoney);
    }

    /// <summary>
    /// 出生动画播放结束事件
    /// </summary>
    public void BornOver()
    {
        // 给AI设置目标点
        navMeshAgent.SetDestination(SafeZoneObject.Instance.transform.position);

        // 播放前进动画
        animator.SetBool("Run", true);
    }

    /// <summary>
    /// 死亡动画播放结束事件
    /// </summary>
    public void DeadEvent()
    {
        // 减少场上丧尸数量
        GameLevelManager.Instance.RemoveZombie(this);

        // 移除自己
        Destroy(gameObject, 0.2f);

        // 检查游戏是否结束
        if (GameLevelManager.Instance.CheckAllOver())
        {
            GameOverPanel gameOverPanel = UIManager.Instance.ShowPanel<GameOverPanel>();
            gameOverPanel.InitInfo(GameLevelManager.Instance.playerObj.money, true);
        }
    }

    /// <summary>
    /// 攻击伤害检测事件
    /// </summary>
    public void AtkEvent()
    {
        // 播放音效
        GameDataManager.Instance.PlaySound("Music/Eat");

        // 碰撞检测
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("SafeZone"));

        // 还需要遍历数组是否碰撞到安全区，因为攻击时可能会碰撞到其他碰撞盒
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == SafeZoneObject.Instance.gameObject)
            {
                SafeZoneObject.Instance.Wound(zombieInfo.atk);
            }
        }
    }
}
