using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    private int atk;
    public int money;
    private float rotateSpeed = 50;

    [Header("武器伤害检测")]
    public Transform firePoint;

    public bool isOpenPlayerMove;   // 是否开启玩家角色移动
    private bool isMove;
    private bool isOpenRoll = true;
    private float moveMultiple = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        isOpenPlayerMove = true;
    }

    void Update()
    {
        if (isOpenPlayerMove)
        {
            Move();
            Crouch();
            Roll();
            Fire();
        }
    }

    #region 移动 & 开火
    private void Move()
    {
        animator.SetFloat("VSpeed", Input.GetAxisRaw("Vertical") * moveMultiple);
        animator.SetFloat("HSpeed", Input.GetAxisRaw("Horizontal") * moveMultiple);
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger("Roll");
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fire");
        }
    }

    public void OpenPlayerMove()
    {
        isOpenPlayerMove = true;
    }

    public void ClosePlayerMove()
    {
        isOpenPlayerMove = false;
    }
    #endregion

    /// <summary>
    /// 初始化玩家属性
    /// </summary>
    /// <param name="atk">攻击力</param>
    /// <param name="money">金钱</param>
    public void InitPlayerInfo(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }

    #region 武器伤害检测函数
    /// <summary>
    /// 枪支武器伤害检测方法
    /// </summary>
    public void ShootEvent()
    {
        if (firePoint == null) return;

        // 播放音效
        GameDataManager.Instance.PlaySound("Music/Gun");

        // 射线检测
        RaycastHit[] raycastHits = Physics.RaycastAll(new Ray(firePoint.position, transform.forward), 1000, 1 << LayerMask.NameToLayer("Zombie"));
        for (int i = 0; i < raycastHits.Length; i++)
        {
            // 获得怪物脚本，调用其受伤方法
            ZombieObject zombieObject = raycastHits[i].transform.gameObject.GetComponent<ZombieObject>();

            if (zombieObject != null && !zombieObject.isDead)
            {
                // 特效创建
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataManager.Instance.nowRoleInfo.hitEff));
                effObj.transform.position = raycastHits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(raycastHits[i].normal);
                Destroy(effObj, 2.5f);

                zombieObject.Wound(atk);
                break;
            }
        }
    }
    #endregion

    /// <summary>
    /// 更新游戏面板的金钱
    /// </summary>
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// 添加金钱方法
    /// </summary>
    /// <param name="money">添加的金钱数</param>
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }
}
