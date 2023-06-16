using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ɥʬ����
/// </summary>
public class ZombieObject : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private ZombieInfo zombieInfo;

    private int hp;
    public bool isDead = false;

    private float frontAttackTime = 0;  // ��¼ǰһ�ι�����ʱ��

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isDead) return;

        animator.SetBool("Run", navMeshAgent.velocity != Vector3.zero); // ����ٶ������Ż�ֹͣ����

        // �жϾ��룬�Լ��жϹ������ʱ��
        if (Vector3.Distance(
            transform.position, SafeZoneObject.Instance.transform.position) < 5 &&
            Time.time - frontAttackTime >= zombieInfo.atkOffset)
        {
            frontAttackTime = Time.time;
            animator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// ɥʬ��ʼ������
    /// </summary>
    /// <param name="info">ɥʬ��Ϣ����</param>
    public void Init(ZombieInfo info)
    {
        if (info == null) return;

        // ��ȡ����
        zombieInfo = info;

        // ��̬����״̬��
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);

        // Ѫ��
        hp = info.hp;

        // Ѱ·AI
        navMeshAgent.speed = navMeshAgent.acceleration = info.moveSpeed;
        navMeshAgent.angularSpeed = info.rotateSpeed;
    }

    /// <summary>
    /// ɥʬ���˷���
    /// </summary>
    /// <param name="damage">�˺���</param>
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
            // ����������Ч
            GameDataManager.Instance.PlaySound("Music/Wound");
        }
    }

    /// <summary>
    /// ɥʬ��������
    /// </summary>
    public void Dead()
    {
        isDead = true;

        // ֹͣѰ·ϵͳ
        //navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        // ������������
        animator.SetBool("Dead", true);

        // ������Ч
        GameDataManager.Instance.PlaySound("Music/dead");

        // ��Ǯ
        GameLevelManager.Instance.playerObj.AddMoney(zombieInfo.awardMoney);
    }

    /// <summary>
    /// �����������Ž����¼�
    /// </summary>
    public void BornOver()
    {
        // ��AI����Ŀ���
        navMeshAgent.SetDestination(SafeZoneObject.Instance.transform.position);

        // ����ǰ������
        animator.SetBool("Run", true);
    }

    /// <summary>
    /// �����������Ž����¼�
    /// </summary>
    public void DeadEvent()
    {
        // ���ٳ���ɥʬ����
        GameLevelManager.Instance.RemoveZombie(this);

        // �Ƴ��Լ�
        Destroy(gameObject, 0.2f);

        // �����Ϸ�Ƿ����
        if (GameLevelManager.Instance.CheckAllOver())
        {
            GameOverPanel gameOverPanel = UIManager.Instance.ShowPanel<GameOverPanel>();
            gameOverPanel.InitInfo(GameLevelManager.Instance.playerObj.money, true);
        }
    }

    /// <summary>
    /// �����˺�����¼�
    /// </summary>
    public void AtkEvent()
    {
        // ������Ч
        GameDataManager.Instance.PlaySound("Music/Eat");

        // ��ײ���
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1, 1 << LayerMask.NameToLayer("SafeZone"));

        // ����Ҫ���������Ƿ���ײ����ȫ������Ϊ����ʱ���ܻ���ײ��������ײ��
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == SafeZoneObject.Instance.gameObject)
            {
                SafeZoneObject.Instance.Wound(zombieInfo.atk);
            }
        }
    }
}
