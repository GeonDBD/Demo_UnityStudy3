using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ��Ϸ�������
/// </summary>
public class GameOverPanel : BasePanel
{
    [Header("��Ϸ�������")]
    public Text txtTitle;
    public Text txtMoney;
    public Button btnConfirm;

    public override void Init()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            // �������
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();

            // ��չؿ�����
            GameLevelManager.Instance.Clear();

            // �л�����
            SceneManager.LoadSceneAsync("BeginScene");
        });
    }

    /// <summary>
    /// ��ʼ����Ϸ���������Ϣ
    /// </summary>
    /// <param name="money">������Ǯ</param>
    /// <param name="isWin">�Ƿ�ʤ����ʶ��</param>
    public void InitInfo(int money, bool isWin)
    {
        txtTitle.text = isWin ? "��Ϸ�ɹ�" : "��Ϸʧ��";
        txtTitle.color = isWin ? new Color(100/255, 1, 116/255) : new Color(1, 100/255, 100/255);
        txtMoney.text = "$" + money;

        GameDataManager.Instance.playerData.money += money;
        GameDataManager.Instance.SavePlayerData();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        // ��������ʾ���
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
