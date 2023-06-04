using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    private Transform canvasTrans;
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();   // ����ֵ䣺���ڴ洢���ɵ����

    private UIManager()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        GameObject.DontDestroyOnLoad(canvas);
        canvasTrans = canvas.transform;
    }

    /// <summary>
    /// ��ʾ����
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <returns>�������ű�</returns>
    public T ShowPanel<T>() where T : BasePanel
    {
        // ��ȡ�������
        string panelName = typeof(T).Name;

        // ����Ƿ��Ѵ洢�����
        if (panelDic.ContainsKey(panelName) )
        {
            return panelDic[panelName] as T;
        }

        // ��ȡ���Ԥ������󣬲�ʵ�������ɸö���
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        
        // ����Canvas��
        panelObj.transform.SetParent(canvasTrans, false);

        // ��ȡ�������ű�
        T panelComponent = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panelComponent);
        panelComponent.ShowPanel();

        return panelComponent;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <param name="isFade">�Ƿ�������Ч��</param>
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        // ��ȡ�������
        string panelName = typeof(T).Name;

        // ����Ƿ��Ѵ洢�����
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
    /// ��ȡ���
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <returns>�������ű�</returns>
    public T GetPanel<T>() where T : BasePanel
    {
        // ��ȡ�������
        string panelName = typeof(T).Name;

        // ����Ƿ��Ѵ洢�����
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[(panelName)] as T;
        }

        return null;
    }
}
