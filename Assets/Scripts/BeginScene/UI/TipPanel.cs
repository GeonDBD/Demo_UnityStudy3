using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    [Header("��ʾ���")]
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
    /// �ı���ʾ����
    /// </summary>
    /// <param name="str">�����ַ���</param>
    public void ChangeTipContent(string str)
    {
        txtContent.text = str;
    }
}
