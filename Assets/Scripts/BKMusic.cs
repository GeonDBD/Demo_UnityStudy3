using UnityEngine;

/// <summary>
/// 背景音乐
/// </summary>
public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();

        // 初始化背景音乐的数据
        MusicData data = GameDataManager.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }

    /// <summary>
    /// 设置背景音乐是否开启
    /// </summary>
    /// <param name="isOpen">背景音乐开启标识符</param>
    public void SetIsOpen(bool isOpen)
    {
        audioSource.mute = !isOpen;
    }

    /// <summary>
    /// 改变背景音乐音量
    /// </summary>
    /// <param name="value">音量值</param>
    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }
}
