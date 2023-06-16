using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔
/// </summary>
public class TowerObject : MonoBehaviour
{
    public Transform head;              // 防御塔头部
    public Transform firePoint;         // 开火点
    private float rotateSpeed = 20;     // 防御塔头部旋转速度
    private TowerInfo towerInfo;        // 防御塔信息数据

    private ZombieObject zombieTarget;          // 单个攻击目标
    private List<ZombieObject> zombieTargets;   // 多个攻击目标
    private Vector3 zombieTargetPos;            // 攻击目标位置

    private float frontAtkTime;         // 计算攻击间隔时间

    private void Update()
    {
        if (towerInfo.type == 1)        // 单体攻击
        {
            if (zombieTarget == null || zombieTarget.isDead || Vector3.Distance(transform.position, zombieTarget.transform.position) > towerInfo.atkRange)
            {
                zombieTarget = GameLevelManager.Instance.FindZombieTarget(this.transform.position, towerInfo.atkRange);
            }

            if (zombieTarget == null) return;

            // 头部旋转
            zombieTargetPos = zombieTarget.transform.position;
            zombieTargetPos.y = head.position.y;
            head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(zombieTargetPos - head.position), rotateSpeed * Time.deltaTime);

            // 攻击检测
            if (Vector3.Angle(head.forward, zombieTargetPos - head.position) < 5 && Time.time - frontAtkTime >= towerInfo.atkOffsetTime)
            {
                GameDataManager.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(towerInfo.eff), firePoint.position, firePoint.rotation);
                Destroy(effObj, 0.5f);

                zombieTarget.Wound(towerInfo.atk);

                frontAtkTime = Time.time;
            }
        }
        else if (towerInfo.type == 2)   // 群体攻击
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
    /// 防御塔初始化方法
    /// </summary>
    /// <param name="info">防御塔信息数据</param>
    public void Init(TowerInfo info)
    {
        towerInfo = info;
    }
}
