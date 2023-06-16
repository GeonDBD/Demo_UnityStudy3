using System.Collections.Generic;
using UnityEngine;

public class ZombiePoint : MonoBehaviour
{
    [Header("丧尸最大生成浪潮波数")]
    public int maxWaveCount;

    [Header("每波浪潮丧尸数量")]
    public int perWaveZombieCount;
    private int currentWaveZombieCount;         // 当前波数生成丧尸数量

    [Header("浪潮丧尸库")]
    public List<int> zombieList;
    private int currentZombieID;                // 当前生成的丧尸ID

    [Header("第一波丧尸浪潮生成间隔时间")]
    public float firstWaveDelayTime;

    [Header("每波丧尸浪潮生成间隔时间")]
    public float perWaveDelayTime;

    [Header("丧尸生成间隔时间")]
    public float generateZombieDelayTime;

    void Start()
    {
        // 使用延迟函数，进行间隔一段时间调用函数
        Invoke("GenerateOneWave", firstWaveDelayTime);

        GameLevelManager.Instance.AddZombiePoint(this);
        GameLevelManager.Instance.UpdateMaxWaveCount(maxWaveCount);
    }

    /// <summary>
    /// 生成一波丧尸浪潮方法
    /// </summary>
    private void GenerateOneWave()
    {
        // 获取信息
        currentZombieID = zombieList[Random.Range(0, zombieList.Count)];
        currentWaveZombieCount = perWaveZombieCount;

        // 生成丧尸
        GenerateZombie();

        // 生成完成，减少总波数
        --maxWaveCount;

        // 在管理器中更新浪潮波数
        GameLevelManager.Instance.UpdateCurrentWaveNum(1);
    }

    /// <summary>
    /// 生成丧尸方法
    /// </summary>
    private void GenerateZombie()
    {
        ZombieInfo zombieInfo = GameDataManager.Instance.zombieInfos[currentZombieID - 1];
        GameObject obj = Instantiate(Resources.Load<GameObject>(zombieInfo.res), transform.position, Quaternion.identity);
        ZombieObject zombieObj = obj.AddComponent<ZombieObject>();
        zombieObj.Init(zombieInfo);

        // 为关卡管理器增加丧尸数量
        GameLevelManager.Instance.AddZombie(zombieObj);

        // 减少当前波数的丧尸生成数量
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
    /// 检测丧尸浪潮生成是否结束
    /// </summary>
    /// <returns>结束标识</returns>
    public bool CheckWaveOver()
    {
        return currentWaveZombieCount == 0 && maxWaveCount == 0;
    }
}
