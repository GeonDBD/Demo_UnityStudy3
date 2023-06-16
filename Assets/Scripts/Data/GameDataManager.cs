using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ���ݹ�����
/// </summary>
public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance => instance;

    public PlayerData playerData;       // �������
    public MusicData musicData;         // ��������

    public List<RoleInfo> roleInfos;    // ��ɫ��Ϣ���ݱ�
    public RoleInfo nowRoleInfo;        // ��ǰ��Ϸѡ��Ľ�ɫ��Ϣ

    public List<SceneInfo> sceneInfos;  // ������Ϣ���ݱ�
    public SceneInfo nowSceneInfo;      // ��ǰ��Ϸѡ��ĳ�����Ϣ

    public List<ZombieInfo> zombieInfos;    // ɥʬ��Ϣ���ݱ�

    public List<TowerInfo> towerInfos;      // ��������Ϣ���ݱ�

    private GameDataManager()
    {
        // ��ʼ����ȡ����
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        sceneInfos = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        zombieInfos = JsonMgr.Instance.LoadData<List<ZombieInfo>>("ZombieInfo");
        towerInfos = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
    }

    #region ���ݴ���
    /// <summary>
    /// �����������
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }

    /// <summary>
    /// ������������
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    #endregion

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="resName">��Ч��Ƭ�ļ���</param>
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
