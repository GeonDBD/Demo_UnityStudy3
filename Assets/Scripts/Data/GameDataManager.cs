using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ���ݹ�����
/// </summary>
public class GameDataManager : MonoBehaviour
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance => instance;

    public PlayerData playerData;       // �������
    public MusicData musicData;         // ��������
    public List<RoleInfo> roleInfos;    // ��ɫ��Ϣ����

    private GameDataManager()
    {
        // ��ʼ������
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
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
