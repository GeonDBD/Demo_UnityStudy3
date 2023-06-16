using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public Text txtNumber;
    public Image imgTower;
    public Text txtTowerCost;

    /// <summary>
    /// 初始化防御塔建造按钮方法
    /// </summary>
    /// <param name="id">防御塔ID</param>
    /// <param name="keyName">建造所需按下的按键名</param>
    public void Init(int id, string keyName)
    {
        TowerInfo towerInfo = GameDataManager.Instance.towerInfos[id - 1];
        txtNumber.text = keyName;
        imgTower.sprite = Resources.Load<Sprite>(towerInfo.imgRes);

        if (towerInfo.money > GameLevelManager.Instance.playerObj.money)
            txtTowerCost.text = "金钱不足";
        else
            txtTowerCost.text = "$" + towerInfo.money;
    }
}
