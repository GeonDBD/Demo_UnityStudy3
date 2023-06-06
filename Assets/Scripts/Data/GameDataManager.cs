using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理器
/// </summary>
public class GameDataManager : MonoBehaviour
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance => instance;

    public PlayerData playerData;       // 玩家数据
    public MusicData musicData;         // 音乐数据
    public List<RoleInfo> roleInfos;    // 角色信息数据

    private GameDataManager()
    {
        // 初始化数据
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
    }

    #region 数据处理
    /// <summary>
    /// 保存玩家数据
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    /// <summary>
    /// 保存音乐数据
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    #endregion
}
