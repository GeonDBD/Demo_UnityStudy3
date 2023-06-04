using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    private Transform canvasTrans;
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();   // 面板字典：用于存储生成的面板

    private UIManager()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        GameObject.DontDestroyOnLoad(canvas);
        canvasTrans = canvas.transform;
    }

    /// <summary>
    /// 显示方法
    /// </summary>
    /// <typeparam name="T">面板类名</typeparam>
    /// <returns>面板组件脚本</returns>
    public T ShowPanel<T>() where T : BasePanel
    {
        // 获取面板名字
        string panelName = typeof(T).Name;

        // 检查是否已存储该面板
        if (panelDic.ContainsKey(panelName) )
        {
            return panelDic[panelName] as T;
        }

        // 获取面板预制体对象，并实例化生成该对象
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        
        // 放入Canvas中
        panelObj.transform.SetParent(canvasTrans, false);

        // 获取面板组件脚本
        T panelComponent = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panelComponent);
        panelComponent.ShowPanel();

        return panelComponent;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T">面板类名</typeparam>
    /// <param name="isFade">是否开启淡出效果</param>
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        // 获取面板名字
        string panelName = typeof(T).Name;

        // 检查是否已存储该面板
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HidePanel(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }

    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T">面板类名</typeparam>
    /// <returns>面板组件脚本</returns>
    public T GetPanel<T>() where T : BasePanel
    {
        // 获取面板名字
        string panelName = typeof(T).Name;

        // 检查是否已存储该面板
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[(panelName)] as T;
        }

        return null;
    }
}
