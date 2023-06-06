using UnityEngine;

/// <summary>
/// ��������
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

        // ��ʼ���������ֵ�����
        MusicData data = GameDataManager.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }

    /// <summary>
    /// ���ñ��������Ƿ���
    /// </summary>
    /// <param name="isOpen">�������ֿ�����ʶ��</param>
    public void SetIsOpen(bool isOpen)
    {
        audioSource.mute = !isOpen;
    }

    /// <summary>
    /// �ı䱳����������
    /// </summary>
    /// <param name="value">����ֵ</param>
    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }
}
