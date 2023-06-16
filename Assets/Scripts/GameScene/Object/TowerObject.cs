using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
public class TowerObject : MonoBehaviour
{
    public Transform head;              // ������ͷ��
    public Transform firePoint;         // �����
    private float rotateSpeed = 20;     // ������ͷ����ת�ٶ�
    private TowerInfo towerInfo;        // ��������Ϣ����

    private ZombieObject zombieTarget;          // ��������Ŀ��
    private List<ZombieObject> zombieTargets;   // �������Ŀ��
    private Vector3 zombieTargetPos;            // ����Ŀ��λ��

    private float frontAtkTime;         // ���㹥�����ʱ��

    private void Update()
    {
        if (towerInfo.type == 1)        // ���幥��
        {
            if (zombieTarget == null || zombieTarget.isDead || Vector3.Distance(transform.position, zombieTarget.transform.position) > towerInfo.atkRange)
            {
                zombieTarget = GameLevelManager.Instance.FindZombieTarget(this.transform.position, towerInfo.atkRange);
            }

            if (zombieTarget == null) return;

            // ͷ����ת
            zombieTargetPos = zombieTarget.transform.position;
            zombieTargetPos.y = head.position.y;
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(zombieTargetPos - head.position), rotateSpeed * Time.deltaTime);

            // �������
            if (Vector3.Angle(head.forward, zombieTargetPos - head.position) < 5 && Time.time - frontAtkTime >= towerInfo.atkOffsetTime)
            {
                GameDataManager.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(towerInfo.eff), firePoint.position, firePoint.rotation);
                Destroy(effObj, 0.5f);

                zombieTarget.Wound(towerInfo.atk);

                frontAtkTime = Time.time;
            }
        }
        else if (towerInfo.type == 2)   // Ⱥ�幥��
        {
            zombieTargets = GameLevelManager.Instance.FindZombieTargets(this.transform.position, towerInfo.atkRange);

            if (zombieTargets.Count > 0 && Time.time - frontAtkTime >= towerInfo.atkOffsetTime)
            {
                GameDataManager.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(towerInfo.eff), this.transform.position, this.transform.rotation);
                Destroy(effObj, 0.5f);

                for (int i = 0; i < zombieTargets.Count; i++)
                {
                    zombieTargets[i].Wound(towerInfo.atk);
                }

                frontAtkTime = Time.time;
            }
        }
    }

    /// <summary>
    /// ��������ʼ������
    /// </summary>
    /// <param name="info">��������Ϣ����</param>
    public void Init(TowerInfo info)
    {
        towerInfo = info;
    }
}
