using System.Collections.Generic;

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

    public List<SceneInfo> sceneInfos;  // ������Ϣ��
    public SceneInfo nowSceneInfo;      // ��ǰ��Ϸѡ��ĳ�����Ϣ

    public List<ZombieInfo> zombieInfos;    // ɥʬ��Ϣ��

    private GameDataManager()
    {
        // ��ʼ����ȡ����
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        sceneInfos = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        zombieInfos = JsonMgr.Instance.LoadData<List<ZombieInfo>>("ZombieInfo");
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
}
