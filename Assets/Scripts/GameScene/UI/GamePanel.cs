using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    [Header("安全区血量相关")]
    public Image imgHP;
    public Text txtHP;
    public float hpWidth;

    [Header("游戏信息")]
    public Text txtWave;
    public Text txtMoney;

    [Header("按钮")]
    public Button btnQuit;

    [Header("塔防建造")]
    public Transform transBotBar;
    public List<TowerButton> towerButtons = new List<TowerButton>();

    private TowerPoint towerPoint;
    private bool isOpenTowerButtonInput;

    protected override void Update()
    {
        base.Update();

        // 解锁鼠标
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            LockAndHideCursor();
        }

        // 防御塔建造
        if (isOpenTowerButtonInput)
        {
            // 判断是否已建造防御塔
            if (towerPoint.towerInfo == null)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    towerPoint.BuildTower(towerPoint.list_TowerID[0]);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    towerPoint.BuildTower(towerPoint.list_TowerID[1]);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    towerPoint.BuildTower(towerPoint.list_TowerID[2]);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    towerPoint.BuildTower(towerPoint.towerInfo.next);
                }
            }
        }
    }

    /// <summary>
    /// 初始化游戏面板方法
    /// </summary>
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        LockAndHideCursor();

        // 一开始隐藏造塔面板
        transBotBar.gameObject.SetActive(false);

        // 血条宽度与UI面板上的一致
        hpWidth = imgHP.rectTransform.sizeDelta.x;
    }

    /// <summary>
    /// 锁定并隐藏鼠标
    /// </summary>
    private void LockAndHideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currrentHP"></param>
    /// <param name="maxHP"></param>
    public void UpdateSafeZoneHP(int currrentHP, int maxHP)
    {
        txtHP.text = currrentHP + " / " + maxHP;
        imgHP.rectTransform.sizeDelta = new Vector2((float)currrentHP / maxHP * hpWidth, imgHP.rectTransform.sizeDelta.y);
    }

    /// <summary>
    /// 更新丧尸浪潮波数
    /// </summary>
    /// <param name="currentWave"></param>
    /// <param name="maxWave"></param>
    public void UpdateWave(int currentWave, int maxWave)
    {
        txtWave.text = currentWave + " / " + maxWave;
    }

    /// <summary>
    /// 更新游戏界面的金钱方法
    /// </summary>
    /// <param name="money">金钱数</param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }

    /// <summary>
    /// 更新造塔按钮方法
    /// </summary>
    /// <param name="towerPoint">造塔点对象</param>
    public void UpdateTowerButton(TowerPoint towerPoint)
    {
        if (towerPoint == null)
        {
            transBotBar.gameObject.SetActive(false);
            isOpenTowerButtonInput = false;
        }
        else
        {
            transBotBar.gameObject.SetActive(true);
            isOpenTowerButtonInput = true;

            this.towerPoint = towerPoint;

            // 检查是否已建造防御塔
            if (towerPoint.towerInfo == null)
            {
                for (int i = 0; i < towerButtons.Count; i++)
                {
                    towerButtons[i].gameObject.SetActive(true);
                    towerButtons[i].Init(this.towerPoint.list_TowerID[i], "数字键" + (i + 1));
                }
            }
            else
            {
                for (int i = 0; i < towerButtons.Count; i++)
                {
                    towerButtons[i].gameObject.SetActive(false);
                }

                towerButtons[1].gameObject.SetActive(true);
                towerButtons[1].Init(this.towerPoint.towerInfo.next, "空格键");
            }
        }
    }
}
