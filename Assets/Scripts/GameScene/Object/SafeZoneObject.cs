using UnityEngine;

/// <summary>
/// ��ȫ������
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
    /// ��ȫ��Ѫ�����·���
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
    /// ��ȫ�����˷���
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

            // ��Ϸ����
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
