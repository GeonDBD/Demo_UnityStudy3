using UnityEngine;

/// <summary>
/// ������
/// </summary>
public class Main : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.ShowPanel<BeginPanel>();
    }
}
