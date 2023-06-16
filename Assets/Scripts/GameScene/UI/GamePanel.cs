using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    [Header("��ȫ��Ѫ�����")]
    public Image imgHP;
    public Text txtHP;
    public float hpWidth;

    [Header("��Ϸ��Ϣ")]
    public Text txtWave;
    public Text txtMoney;

    [Header("��ť")]
    public Button btnQuit;

    [Header("��������")]
    public Transform transBotBar;
    public List<TowerButton> towerButtons = new List<TowerButton>();

    private TowerPoint towerPoint;
    private bool isOpenTowerButtonInput;

    protected override void Update()
    {
        base.Update();

        // �������
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            LockAndHideCursor();
        }

        // ����������
        if (isOpenTowerButtonInput)
        {
            // �ж��Ƿ��ѽ��������
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
    /// ��ʼ����Ϸ��巽��
    /// </summary>
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        LockAndHideCursor();

        // һ��ʼ�����������
        transBotBar.gameObject.SetActive(false);

        // Ѫ�������UI����ϵ�һ��
        hpWidth = imgHP.rectTransform.sizeDelta.x;
    }

    /// <summary>
    /// �������������
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
    /// ����ɥʬ�˳�����
    /// </summary>
    /// <param name="currentWave"></param>
    /// <param name="maxWave"></param>
    public void UpdateWave(int currentWave, int maxWave)
    {
        txtWave.text = currentWave + " / " + maxWave;
    }

    /// <summary>
    /// ������Ϸ����Ľ�Ǯ����
    /// </summary>
    /// <param name="money">��Ǯ��</param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }

    /// <summary>
    /// ����������ť����
    /// </summary>
    /// <param name="towerPoint">���������</param>
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

            // ����Ƿ��ѽ��������
            if (towerPoint.towerInfo == null)
            {
                for (int i = 0; i < towerButtons.Count; i++)
                {
                    towerButtons[i].gameObject.SetActive(true);
                    towerButtons[i].Init(this.towerPoint.list_TowerID[i], "���ּ�" + (i + 1));
                }
            }
            else
            {
                for (int i = 0; i < towerButtons.Count; i++)
                {
                    towerButtons[i].gameObject.SetActive(false);
                }

                towerButtons[1].gameObject.SetActive(true);
                towerButtons[1].Init(this.towerPoint.towerInfo.next, "�ո��");
            }
        }
    }
}
