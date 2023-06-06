using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始面板
/// </summary>
public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnQuit;

    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<BeginPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                print("显示选择角色面板");
            });
        });

        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btnAbout.onClick.AddListener(() =>
        {
            print("显示关于游戏面板");
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
