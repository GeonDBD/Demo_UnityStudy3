using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseRolePanel : BasePanel
{
    [Header("玩家金钱数")]
    public Text txtMoney;

    [Header("角色切换按钮")]
    public Button btnLeft;
    public Button btnRight;

    [Header("角色信息")]
    public Text txtRoleName;

    [Header("角色解锁")]
    public Button btnUnlock;
    public Text txtUnlock;

    [Header("面板操作按钮")]
    public Button btnStart;
    public Button btnBack;

    private Transform rolePos;
    private GameObject nowRoleObj;
    private RoleInfo nowRoleInfo;
    private int nowIndex;

    public override void Init()
    {
        // 获取角色预制体生成位置
        rolePos = GameObject.Find("RolePos").transform;
        nowIndex = 0;
        UpdateRoleObj();

        // 更新玩家金钱
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
                print("提示面板：购买成功");
            }
            else
            {
                print("提示面板：金钱不足");
            }
        });

        btnStart.onClick.AddListener(() =>
        {
            GameDataManager.Instance.nowRoleInfo = this.nowRoleInfo;
            UIManager.Instance.HidePanel<ChooseRolePanel>();
            print("加载游戏场景");
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
    /// 更新加载角色对象
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
    /// 更新解锁角色按钮
    /// </summary>
    private void UpdateUnlockButton()
    {
        // 角色解锁
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
