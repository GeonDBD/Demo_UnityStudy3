using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private GameObject tower = null;
    public TowerInfo towerInfo = null;

    public List<int> list_TowerID;

    /// <summary>
    /// �������������
    /// </summary>
    /// <param name="id">������ID</param>
    public void BuildTower(int id)
    {
        TowerInfo info = GameDataManager.Instance.towerInfos[id - 1];

        // ���Ǯ������
        if (info.money > GameLevelManager.Instance.playerObj.money)
            return;

        // ��Ǯ
        GameLevelManager.Instance.playerObj.AddMoney(-info.money);

        // �����������Ƿ��ѽ��������
        if (tower != null)
        {
            DestroyImmediate(tower);
            tower = null;
        }

        // ʵ��������ʼ����Ϣ
        tower = Instantiate(Resources.Load<GameObject>(info.res), transform.position, transform.rotation);
        tower.GetComponent<TowerObject>().Init(info);

        // ��¼����
        towerInfo = info;

        // ��������������Ϸ���
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
        // ����Ƿ�������
        if (towerInfo != null && towerInfo.next == 0)
            return;

        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerButton(this);
    }

    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerButton(null);
    }
}
