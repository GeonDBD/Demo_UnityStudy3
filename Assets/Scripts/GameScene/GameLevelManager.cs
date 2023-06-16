using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏关卡管理器
/// </summary>
public class GameLevelManager
{
    private static GameLevelManager instance = new GameLevelManager();
    public static GameLevelManager Instance => instance;
    private GameLevelManager() { }

    public PlayerObject playerObj;

    private List<ZombiePoint> zombiePointList = new List<ZombiePoint>();    // 出怪点表
    private int currentWaveNum = 0;         // 当前浪潮波数
    private int maxWaveCount = 0;           // 总丧尸浪潮波数

    private List<ZombieObject> zombieObjectOnSceneList = new List<ZombieObject>();  // 记录当前场上的丧尸列表

    /// <summary>
    /// 游戏关卡初始化
    /// </summary>
    public void Init(SceneInfo sceneInfo)
    {
        // 显示游戏面板
        UIManager.Instance.ShowPanel<GamePanel>();

        // 创建玩家
        RoleInfo roleInfo = GameDataManager.Instance.nowRoleInfo;

        // 获取玩家生成位置
        Transform playerBornPos = GameObject.Find("PlayerBornPos").transform;

        GameObject roleObj = Object.Instantiate(Resources.Load<GameObject>(roleInfo.res), playerBornPos.position, playerBornPos.rotation);

        playerObj = roleObj.GetComponent<PlayerObject>();
        playerObj.InitPlayerInfo(roleInfo.atk, sceneInfo.money);

        Camera.main.GetComponent<CameraMove>().SetTarget(roleObj.transform);

        // 初始化安全区
        SafeZoneObject.Instance.UpdateHp(sceneInfo.zoneHP, sceneInfo.zoneHP);
    }

    /// <summary>
    /// 更新最大丧尸浪潮数
    /// </summary>
    /// <param name="count">最大丧尸浪潮数</param>
    public void UpdateMaxWaveCount(int count)
    {
        maxWaveCount += count;
        currentWaveNum = maxWaveCount;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(currentWaveNum, maxWaveCount);
    }

    /// <summary>
    /// 更新当前丧尸浪潮剩余波数
    /// </summary>
    /// <param name="num">丧尸浪潮波数</param>
    public void UpdateCurrentWaveNum(int num)
    {
        currentWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(currentWaveNum, maxWaveCount);
    }

    /// <summary>
    /// 添加出怪点
    /// </summary>
    /// <param name="zombiePoint">出怪点脚本对象</param>
    public void AddZombiePoint(ZombiePoint zombiePoint)
    {
        zombiePointList.Add(zombiePoint);
    }

    /// <summary>
    /// 添加丧尸至当前场上列表
    /// </summary>
    /// <param name="zombieObject">丧尸对象</param>
    public void AddZombie(ZombieObject zombieObject)
    {
        zombieObjectOnSceneList.Add(zombieObject);
    }

    /// <summary>
    /// 从当前场上列表中移除丧尸
    /// </summary>
    /// <param name="zombieObject">丧尸对象</param>
    public void RemoveZombie(ZombieObject zombieObject)
    {
        zombieObjectOnSceneList.Remove(zombieObject);
    }

    /// <summary>
    /// 寻找单个丧尸目标
    /// </summary>
    /// <param name="pos">防御塔位置</param>
    /// <param name="range">防御塔攻击范围</param>
    /// <returns>丧尸对象</returns>
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
    /// 寻找多个丧尸目标
    /// </summary>
    /// <param name="pos">防御塔位置</param>
    /// <param name="range">防御塔攻击范围</param>
    /// <returns>丧尸对象列表</returns>
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
    /// 检查所有出怪点是否结束方法
    /// </summary>
    /// <returns>结束标识</returns>
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
    /// 清空关卡数据
    /// </summary>
    public void Clear()
    {
        zombiePointList.Clear();
        zombieObjectOnSceneList.Clear();
        currentWaveNum = maxWaveCount = 0;
        playerObj = null;
    }
}
