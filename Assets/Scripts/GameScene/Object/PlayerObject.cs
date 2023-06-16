using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    private int atk;
    public int money;
    private float rotateSpeed = 50;

    [Header("�����˺����")]
    public Transform firePoint;

    public bool isOpenPlayerMove;   // �Ƿ�����ҽ�ɫ�ƶ�
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

    #region �ƶ� & ����
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
    /// ��ʼ���������
    /// </summary>
    /// <param name="atk">������</param>
    /// <param name="money">��Ǯ</param>
    public void InitPlayerInfo(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }

    #region �����˺���⺯��
    /// <summary>
    /// ǹ֧�����˺���ⷽ��
    /// </summary>
    public void ShootEvent()
    {
        if (firePoint == null) return;

        // ������Ч
        GameDataManager.Instance.PlaySound("Music/Gun");

        // ���߼��
        RaycastHit[] raycastHits = Physics.RaycastAll(new Ray(firePoint.position, transform.forward), 1000, 1 << LayerMask.NameToLayer("Zombie"));
        for (int i = 0; i < raycastHits.Length; i++)
        {
            // ��ù���ű������������˷���
            ZombieObject zombieObject = raycastHits[i].transform.gameObject.GetComponent<ZombieObject>();

            if (zombieObject != null && !zombieObject.isDead)
            {
                // ��Ч����
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
    /// ������Ϸ���Ľ�Ǯ
    /// </summary>
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// ��ӽ�Ǯ����
    /// </summary>
    /// <param name="money">��ӵĽ�Ǯ��</param>
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }
}
