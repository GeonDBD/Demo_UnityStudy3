using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetPos;
    public float lookAtPosHeight;
    public float moveSpeed;
    public float rotateSpeed;

    private Vector3 cameraOffsetPos;
    private Quaternion LookAtTargetRotation;

    private void Update()
    {
        if (target == null) return;

        // 摄像机位置
        cameraOffsetPos = target.position;
        cameraOffsetPos += target.forward * offsetPos.z;
        cameraOffsetPos += Vector3.up * offsetPos.y;
        cameraOffsetPos += target.right * offsetPos.x;
        transform.position = Vector3.Lerp(transform.position, cameraOffsetPos, moveSpeed * Time.deltaTime);

        // 摄像机看向目标旋转
        LookAtTargetRotation = Quaternion.LookRotation((target.position + Vector3.up * lookAtPosHeight) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookAtTargetRotation, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 设置摄像机看向目标
    /// </summary>
    /// <param name="player">玩家的Transform</param>
    public void SetTarget(Transform player)
    {
        target = player;
    }
}
