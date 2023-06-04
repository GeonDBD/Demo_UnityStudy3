using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;        // 控制面板透明度组件
    private float alphaSpeed = 10;          // 淡入淡出速度
    public bool isShow = false;             // 是否显示标识符
    private UnityAction hidePanelCallBack = null;  // 隐藏面板时的回调委托函数

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
        // 淡入淡出
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
    /// 初始化方法
    /// 用于注册控件事件，所有的子类面板，都需要注册一些控件事件
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示面板
    /// </summary>
    public virtual void ShowPanel()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public virtual void HidePanel(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hidePanelCallBack = callBack;
    }
}
