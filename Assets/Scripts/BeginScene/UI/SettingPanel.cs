using UnityEngine.UI;

/// <summary>
/// �������
/// </summary>
public class SettingPanel : BasePanel
{
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    public Button btnClose;

    public override void Init()
    {
        // ��ʼ������
        MusicData data = GameDataManager.Instance.musicData;
        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;

        togMusic.onValueChanged.AddListener((flag) =>
        {
            BKMusic.Instance.SetIsOpen(flag);
            GameDataManager.Instance.musicData.musicOpen = flag;
        });

        togSound.onValueChanged.AddListener((flag) =>
        {
            GameDataManager.Instance.musicData.soundOpen = flag;
        });

        sliderMusic.onValueChanged.AddListener((value) =>
        {
            BKMusic.Instance.ChangeValue(value);
            GameDataManager.Instance.musicData.musicValue = value;
        });

        sliderSound.onValueChanged.AddListener((value) =>
        {
            GameDataManager.Instance.musicData.soundValue = value;
        });

        btnClose.onClick.AddListener(() =>
        {
            GameDataManager.Instance.SaveMusicData();       // �ر����ʱ��������
            UIManager.Instance.HidePanel<SettingPanel>();
        });
    }
}
