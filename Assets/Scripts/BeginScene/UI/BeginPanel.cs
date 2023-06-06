using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ¿ªÊ¼Ãæ°å
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
                UIManager.Instance.ShowPanel<ChooseRolePanel>();
            });
        });

        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btnAbout.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<AboutPanel>();
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
