using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������
/// </summary>
public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;                // �������͸�������
    private float alphaSpeed = 10;                  // ���뵭���ٶ�
    private UnityAction hidePanelCallBack = null;   // �������ʱ�Ļص�ί�к���

    [Header("�����ʾ��ʶ��")]
    public bool isShow = false;

    protected virtual void Awake()
    {
        if (canvasGroup != null) canvasGroup = GetComponent<CanvasGroup>();
        else canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        // ���뵭��
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        else if (!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hidePanelCallBack?.Invoke();
            }
        }
    }

    /// <summary>
    /// ��ʼ������
    /// ����ע��ؼ��¼������е�������壬����Ҫע��һЩ�ؼ��¼�
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// ��ʾ���
    /// </summary>
    public virtual void ShowPanel()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    /// <summary>
    /// �������
    /// </summary>
    public virtual void HidePanel(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hidePanelCallBack = callBack;
    }
}
