using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseRolePanel : BasePanel
{
    [Header("��ҽ�Ǯ��")]
    public Text txtMoney;

    [Header("��ɫ�л���ť")]
    public Button btnLeft;
    public Button btnRight;

    [Header("��ɫ��Ϣ")]
    public Text txtRoleName;

    [Header("��ɫ����")]
    public Button btnUnlock;
    public Text txtUnlock;

    [Header("��������ť")]
    public Button btnStart;
    public Button btnBack;

    private Transform rolePos;
    private GameObject nowRoleObj;
    private RoleInfo nowRoleInfo;
    private int nowIndex;

    public override void Init()
    {
        // ��ȡ��ɫԤ��������λ��
        rolePos = GameObject.Find("RolePos").transform;
        nowIndex = 0;
        UpdateRoleObj();

        // ������ҽ�Ǯ
        txtMoney.text = "$: " + GameDataManager.Instance.playerData.money.ToString();

        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;

            if (nowIndex < 0)
            {
                nowIndex = GameDataManager.Instance.roleInfos.Count - 1;
            }

            UpdateRoleObj();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;

            if (nowIndex >= GameDataManager.Instance.roleInfos.Count)
            {
                nowIndex = 0;
            }

            UpdateRoleObj();
        });

        btnUnlock.onClick.AddListener(() =>
        {
            PlayerData playerData = GameDataManager.Instance.playerData;
            if (playerData == null) return;
            if (playerData.money >= nowRoleInfo.lockMoney)
            {
                playerData.money -= nowRoleInfo.lockMoney;
                txtMoney.text = "$: " + playerData.money.ToString();
                playerData.unlockedRole.Add(nowRoleInfo.id);
                GameDataManager.Instance.SavePlayerData();
                UpdateUnlockButton();
                print("��ʾ��壺����ɹ�");
            }
            else
            {
                print("��ʾ��壺��Ǯ����");
            }
        });

        btnStart.onClick.AddListener(() =>
        {
            GameDataManager.Instance.nowRoleInfo = this.nowRoleInfo;
            UIManager.Instance.HidePanel<ChooseRolePanel>();
            print("������Ϸ����");
        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseRolePanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            });
        });
    }

    /// <summary>
    /// ���¼��ؽ�ɫ����
    /// </summary>
    private void UpdateRoleObj()
    {
        if (rolePos != null)
        {
            Destroy(nowRoleObj);
            nowRoleObj = null;
        }

        nowRoleInfo = GameDataManager.Instance.roleInfos[nowIndex];
        nowRoleObj = Instantiate(Resources.Load<GameObject>(nowRoleInfo.res), rolePos.position, rolePos.rotation);
        txtRoleName.text = nowRoleInfo.tips;

        UpdateUnlockButton();
    }

    /// <summary>
    /// ���½�����ɫ��ť
    /// </summary>
    private void UpdateUnlockButton()
    {
        // ��ɫ����
        if (nowRoleInfo.lockMoney > 0 && !GameDataManager.Instance.playerData.unlockedRole.Contains(nowRoleInfo.id))
        {
            btnUnlock.gameObject.SetActive(true);
            txtUnlock.text = "$" + nowRoleInfo.lockMoney.ToString();
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnlock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }

    public override void HidePanel(UnityAction callBack)
    {
        base.HidePanel(callBack);
        if (nowRoleObj != null)
        {
            DestroyImmediate(nowRoleObj);
        }
    }
}
