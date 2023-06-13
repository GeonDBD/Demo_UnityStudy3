using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Ö÷º¯Êý
/// </summary>
public class Main : MonoBehaviour
{
    private Scene nowScene;
    private int zoneCurrentHP = 100;
    private int zoneMaxHP = 100;

    private void Start()
    {
        nowScene = SceneManager.GetActiveScene();

        if (nowScene.name == "BeginScene")
        {
            UIManager.Instance.ShowPanel<BeginPanel>();
        }
        else if (nowScene.name == "GameScene1" || nowScene.name == "GameScene2" || nowScene.name == "GameScene3")
        {
            UIManager.Instance.ShowPanel<GamePanel>();
            UIManager.Instance.GetPanel<GamePanel>().UpdateSafeZoneHP(zoneCurrentHP, zoneMaxHP);
        }
    }
}
