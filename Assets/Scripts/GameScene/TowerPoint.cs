using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private GameObject tower = null;
    public TowerInfo towerInfo = null;

    public List<int> list_TowerID;

    /// <summary>
    /// 建造防御塔方法
    /// </summary>
    /// <param name="id">防御塔ID</param>
    public void BuildTower(int id)
    {
        TowerInfo info = GameDataManager.Instance.towerInfos[id - 1];

        // 检查钱够不够
        if (info.money > GameLevelManager.Instance.playerObj.money)
            return;

        // 扣钱
        GameLevelManager.Instance.playerObj.AddMoney(-info.money);

        // 检查该造塔点是否已建造防御塔
        if (tower != null)
        {
            DestroyImmediate(tower);
            tower = null;
        }

        // 实例化并初始化信息
        tower = Instantiate(Resources.Load<GameObject>(info.res), transform.position, transform.rotation);
        tower.GetComponent<TowerObject>().Init(info);

        // 记录数据
        towerInfo = info;

        // 建造结束后更新游戏面板
        if (towerInfo.next != 0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateTowerButton(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateTowerButton(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否还有升级
        if (towerInfo != null && towerInfo.next == 0)
            return;

        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerButton(this);
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerButton(null);
    }
}
