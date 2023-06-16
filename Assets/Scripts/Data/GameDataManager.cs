using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理器
/// </summary>
public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance => instance;

    public PlayerData playerData;       // 玩家数据
    public MusicData musicData;         // 音乐数据

    public List<RoleInfo> roleInfos;    // 角色信息数据表
    public RoleInfo nowRoleInfo;        // 当前游戏选择的角色信息

    public List<SceneInfo> sceneInfos;  // 场景信息数据表
    public SceneInfo nowSceneInfo;      // 当前游戏选择的场景信息

    public List<ZombieInfo> zombieInfos;    // 丧尸信息数据表

    public List<TowerInfo> towerInfos;      // 防御塔信息数据表

    private GameDataManager()
    {
        // 初始化读取数据
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        sceneInfos = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        zombieInfos = JsonMgr.Instance.LoadData<List<ZombieInfo>>("ZombieInfo");
        towerInfos = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
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

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="resName">音效切片文件名</param>
    public void PlaySound(string resName)
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(resName);
        audioSource.volume = musicData.soundValue;
        audioSource.mute = !musicData.soundOpen;
        audioSource.Play();
        
        GameObject.Destroy(soundObj, 2);
    }
}
