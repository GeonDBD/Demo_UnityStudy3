using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : BasePanel
{
    [Header("������Ϣ�ı�����")]
    public Text txtContent;
    public string content;

    [Header("�رհ�ť")]
    public Button btnClose;

    public override void Init()
    {
        if (content != "")
            txtContent.text = content;

        btnClose.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<AboutPanel>();
        });
    }
}
