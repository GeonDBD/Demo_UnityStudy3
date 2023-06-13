using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    [Header("按钮")]
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;

    [Header("场景描述")]
    public Image imgScene;
    public Text txtSceneName;
    public Text txtSceneTips;

    private int nowIndex;
    private SceneInfo nowSceneInfo;

    public override void Init()
    {
        nowIndex = 0;
        UpdateScene();

        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
            {
                nowIndex = GameDataManager.Instance.sceneInfos.Count - 1;
            }

            UpdateScene();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataManager.Instance.sceneInfos.Count)
            {
                nowIndex = 0;
            }

            UpdateScene();
        });

        btnStart.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            SceneManager.LoadScene(nowSceneInfo.sceneName);
        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            UIManager.Instance.ShowPanel<ChooseRolePanel>();
        });
    }

    /// <summary>
    /// 更新面板上的场景信息
    /// </summary>
    private void UpdateScene()
    {
        nowSceneInfo = GameDataManager.Instance.sceneInfos[nowIndex];
        imgScene.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
        txtSceneName.text = nowSceneInfo.name;
        txtSceneTips.text = nowSceneInfo.tips;
    }
}
