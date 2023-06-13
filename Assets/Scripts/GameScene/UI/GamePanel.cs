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

    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            SceneManager.LoadScene("BeginScene");
        });

        transBotBar.gameObject.SetActive(false);
        hpWidth = imgHP.rectTransform.sizeDelta.x;
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
    /// 
    /// </summary>
    /// <param name="currentWave"></param>
    /// <param name="maxWave"></param>
    public void UpdateWave(int currentWave, int maxWave)
    {
        txtWave.text = currentWave + " / " + maxWave;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="money"></param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }
}
