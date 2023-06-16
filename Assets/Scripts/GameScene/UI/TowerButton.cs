using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public Text txtNumber;
    public Image imgTower;
    public Text txtTowerCost;

    /// <summary>
    /// ��ʼ�����������찴ť����
    /// </summary>
    /// <param name="id">������ID</param>
    /// <param name="keyName">�������谴�µİ�����</param>
    public void Init(int id, string keyName)
    {
        TowerInfo towerInfo = GameDataManager.Instance.towerInfos[id - 1];
        txtNumber.text = keyName;
        imgTower.sprite = Resources.Load<Sprite>(towerInfo.imgRes);

        if (towerInfo.money > GameLevelManager.Instance.playerObj.money)
            txtTowerCost.text = "��Ǯ����";
        else
            txtTowerCost.text = "$" + towerInfo.money;
    }
}
