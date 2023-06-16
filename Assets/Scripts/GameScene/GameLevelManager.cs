using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ�ؿ�������
/// </summary>
public class GameLevelManager
{
    private static GameLevelManager instance = new GameLevelManager();
    public static GameLevelManager Instance => instance;
    private GameLevelManager() { }

    public PlayerObject playerObj;

    private List<ZombiePoint> zombiePointList = new List<ZombiePoint>();    // ���ֵ��
    private int currentWaveNum = 0;         // ��ǰ�˳�����
    private int maxWaveCount = 0;           // ��ɥʬ�˳�����

    private List<ZombieObject> zombieObjectOnSceneList = new List<ZombieObject>();  // ��¼��ǰ���ϵ�ɥʬ�б�

    /// <summary>
    /// ��Ϸ�ؿ���ʼ��
    /// </summary>
    public void Init(SceneInfo sceneInfo)
    {
        // ��ʾ��Ϸ���
        UIManager.Instance.ShowPanel<GamePanel>();

        // �������
        RoleInfo roleInfo = GameDataManager.Instance.nowRoleInfo;

        // ��ȡ�������λ��
        Transform playerBornPos = GameObject.Find("PlayerBornPos").transform;

        GameObject roleObj = Object.Instantiate(Resources.Load<GameObject>(roleInfo.res), playerBornPos.position, playerBornPos.rotation);

        playerObj = roleObj.GetComponent<PlayerObject>();
        playerObj.InitPlayerInfo(roleInfo.atk, sceneInfo.money);

        Camera.main.GetComponent<CameraMove>().SetTarget(roleObj.transform);

        // ��ʼ����ȫ��
        SafeZoneObject.Instance.UpdateHp(sceneInfo.zoneHP, sceneInfo.zoneHP);
    }

    /// <summary>
    /// �������ɥʬ�˳���
    /// </summary>
    /// <param name="count">���ɥʬ�˳���</param>
    public void UpdateMaxWaveCount(int count)
    {
        maxWaveCount += count;
        currentWaveNum = maxWaveCount;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(currentWaveNum, maxWaveCount);
    }

    /// <summary>
    /// ���µ�ǰɥʬ�˳�ʣ�ನ��
    /// </summary>
    /// <param name="num">ɥʬ�˳�����</param>
    public void UpdateCurrentWaveNum(int num)
    {
        currentWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(currentWaveNum, maxWaveCount);
    }

    /// <summary>
    /// ��ӳ��ֵ�
    /// </summary>
    /// <param name="zombiePoint">���ֵ�ű�����</param>
    public void AddZombiePoint(ZombiePoint zombiePoint)
    {
        zombiePointList.Add(zombiePoint);
    }

    /// <summary>
    /// ���ɥʬ����ǰ�����б�
    /// </summary>
    /// <param name="zombieObject">ɥʬ����</param>
    public void AddZombie(ZombieObject zombieObject)
    {
        zombieObjectOnSceneList.Add(zombieObject);
    }

    /// <summary>
    /// �ӵ�ǰ�����б����Ƴ�ɥʬ
    /// </summary>
    /// <param name="zombieObject">ɥʬ����</param>
    public void RemoveZombie(ZombieObject zombieObject)
    {
        zombieObjectOnSceneList.Remove(zombieObject);
    }

    /// <summary>
    /// Ѱ�ҵ���ɥʬĿ��
    /// </summary>
    /// <param name="pos">������λ��</param>
    /// <param name="range">������������Χ</param>
    /// <returns>ɥʬ����</returns>
    public ZombieObject FindZombieTarget(Vector3 pos, int range)
    {
        for (int i = 0; i < zombieObjectOnSceneList.Count; i++)
        {
            if (!zombieObjectOnSceneList[i].isDead && Vector3.Distance(pos, zombieObjectOnSceneList[i].transform.position) <= range)
            {
                return zombieObjectOnSceneList[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Ѱ�Ҷ��ɥʬĿ��
    /// </summary>
    /// <param name="pos">������λ��</param>
    /// <param name="range">������������Χ</param>
    /// <returns>ɥʬ�����б�</returns>
    public List<ZombieObject> FindZombieTargets(Vector3 pos, int range)
    {
        List<ZombieObject> list = new List<ZombieObject>();

        for (int i = 0; i < zombieObjectOnSceneList.Count; i++)
        {
            if (!zombieObjectOnSceneList[i].isDead && Vector3.Distance(pos, zombieObjectOnSceneList[i].transform.position) <= range)
            {
                list.Add(zombieObjectOnSceneList[i]);
            }
        }

        return list;
    }

    /// <summary>
    /// ������г��ֵ��Ƿ��������
    /// </summary>
    /// <returns>������ʶ</returns>
    public bool CheckAllOver()
    {
        for (int i = 0; i < zombiePointList.Count; i++)
        {
            if (!zombiePointList[i].CheckWaveOver()) return false;
        }

        if (zombieObjectOnSceneList.Count > 0) return false;

        return true;
    }

    /// <summary>
    /// ��չؿ�����
    /// </summary>
    public void Clear()
    {
        zombiePointList.Clear();
        zombieObjectOnSceneList.Clear();
        currentWaveNum = maxWaveCount = 0;
        playerObj = null;
    }
}
