using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;        // �������͸�������
    private float alphaSpeed = 10;          // ���뵭���ٶ�
    public bool isShow = false;             // �Ƿ���ʾ��ʶ��
    private UnityAction hidePanelCallBack = null;  // �������ʱ�Ļص�ί�к���

    protected virtual void Awake()
    {
        if (canvasGroup != null) canvasGroup = GetComponent<CanvasGroup>();
        else canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    private void Update()
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
