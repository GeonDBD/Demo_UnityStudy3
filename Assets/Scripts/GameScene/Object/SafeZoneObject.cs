using UnityEngine;

/// <summary>
/// 安全区对象
/// </summary>
public class SafeZoneObject : MonoBehaviour
{
    private static SafeZoneObject instance;
    public static SafeZoneObject Instance => instance;

    private int hp;
    private int maxHP;

    private bool isDead;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 安全区血量更新方法
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="maxHP"></param>
    public void UpdateHp(int hp, int maxHP)
    {
        this.hp = hp;
        this.maxHP = maxHP;
        UIManager.Instance.GetPanel<GamePanel>().UpdateSafeZoneHP(hp, maxHP);
    }

    /// <summary>
    /// 安全区受伤方法
    /// </summary>
    /// <param name="dmg"></param>
    public void Wound(int dmg)
    {
        if (isDead) return;

        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;

            // 游戏结束
        }

        UpdateHp(hp, maxHP);
    }

    private void OnDestroy()
    {
        if (instance != null)
        {
            instance = null;
        }
    }
}
