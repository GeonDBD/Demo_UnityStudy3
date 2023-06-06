using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : BasePanel
{
    [Header("关于信息文本内容")]
    public Text txtContent;
    public string content;

    [Header("关闭按钮")]
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
