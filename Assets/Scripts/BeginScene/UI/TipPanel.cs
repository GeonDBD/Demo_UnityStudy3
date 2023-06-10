using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    [Header("提示面板")]
    public Text txtContent;
    public Button btnConfirm;

    public override void Init()
    {
        btnConfirm.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    /// <summary>
    /// 改变提示内容
    /// </summary>
    /// <param name="str">内容字符串</param>
    public void ChangeTipContent(string str)
    {
        txtContent.text = str;
    }
}
