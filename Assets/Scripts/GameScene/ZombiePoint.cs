using System.Collections.Generic;
using UnityEngine;

public class ZombiePoint : MonoBehaviour
{
    [Header("ɥʬ��������˳�����")]
    public int maxWaveCount;

    [Header("ÿ���˳�ɥʬ����")]
    public int perWaveZombieCount;
    private int currentWaveZombieCount;         // ��ǰ��������ɥʬ����

    [Header("�˳�ɥʬ��")]
    public List<int> zombieList;
    private int currentZombieID;                // ��ǰ���ɵ�ɥʬID

    [Header("��һ��ɥʬ�˳����ɼ��ʱ��")]
    public float firstWaveDelayTime;

    [Header("ÿ��ɥʬ�˳����ɼ��ʱ��")]
    public float perWaveDelayTime;

    [Header("ɥʬ���ɼ��ʱ��")]
    public float generateZombieDelayTime;

    void Start()
    {
        // ʹ���ӳٺ��������м��һ��ʱ����ú���
        Invoke("GenerateOneWave", firstWaveDelayTime);

        GameLevelManager.Instance.AddZombiePoint(this);
        GameLevelManager.Instance.UpdateMaxWaveCount(maxWaveCount);
    }

    /// <summary>
    /// ����һ��ɥʬ�˳�����
    /// </summary>
    private void GenerateOneWave()
    {
        // ��ȡ��Ϣ
        currentZombieID = zombieList[Random.Range(0, zombieList.Count)];
        currentWaveZombieCount = perWaveZombieCount;

        // ����ɥʬ
        GenerateZombie();

        // ������ɣ������ܲ���
        --maxWaveCount;

        // �ڹ������и����˳�����
        GameLevelManager.Instance.UpdateCurrentWaveNum(1);
    }

    /// <summary>
    /// ����ɥʬ����
    /// </summary>
    private void GenerateZombie()
    {
        ZombieInfo zombieInfo = GameDataManager.Instance.zombieInfos[currentZombieID - 1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(zombieInfo.res), transform.position, Quaternion.identity);
        ZombieObject zombieObj = obj.AddComponent<ZombieObject>();
        zombieObj.Init(zombieInfo);

        // Ϊ�ؿ�����������ɥʬ����
        GameLevelManager.Instance.AddZombie(zombieObj);

        // ���ٵ�ǰ������ɥʬ��������
        --currentWaveZombieCount;

        if (currentWaveZombieCount == 0)
        {
            if (maxWaveCount > 0)
            {
                Invoke("GenerateOneWave", perWaveDelayTime);
            }
        }
        else
        {
            Invoke("GenerateZombie", generateZombieDelayTime);
        }
    }

    /// <summary>
    /// ���ɥʬ�˳������Ƿ����
    /// </summary>
    /// <returns>������ʶ</returns>
    public bool CheckWaveOver()
    {
        return currentWaveZombieCount == 0 && maxWaveCount == 0;
    }
}
