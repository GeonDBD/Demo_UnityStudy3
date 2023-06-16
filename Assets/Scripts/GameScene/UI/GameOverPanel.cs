using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 游戏结束面板
/// </summary>
public class GameOverPanel : BasePanel
{
    [Header("游戏结束面板")]
    public Text txtTitle;
    public Text txtMoney;
    public Button btnConfirm;

    public override void Init()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            // 隐藏面板
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();

            // 清空关卡数据
            GameLevelManager.Instance.Clear();

            // 切换场景
            SceneManager.LoadSceneAsync("BeginScene");
        });
    }

    /// <summary>
    /// 初始化游戏结束面板信息
    /// </summary>
    /// <param name="money">奖励金钱</param>
    /// <param name="isWin">是否胜利标识符</param>
    public void InitInfo(int money, bool isWin)
    {
        txtTitle.text = isWin ? "游戏成功" : "游戏失败";
        txtTitle.color = isWin ? new Color(100/255, 1, 116/255) : new Color(1, 100/255, 100/255);
        txtMoney.text = "$" + money;

        GameDataManager.Instance.playerData.money += money;
        GameDataManager.Instance.SavePlayerData();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        // 解锁并显示鼠标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
