using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������
/// </summary>
public class Main : MonoBehaviour
{
    private Scene nowScene;

    private void Start()
    {
        nowScene = SceneManager.GetActiveScene();

        if (nowScene.name == "BeginScene")
        {
            UIManager.Instance.ShowPanel<BeginPanel>();
        }
    }
}
