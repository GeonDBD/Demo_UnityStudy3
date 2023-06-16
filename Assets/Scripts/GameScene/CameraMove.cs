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

        // �����λ��
        cameraOffsetPos = target.position;
        cameraOffsetPos += target.forward * offsetPos.z;
        cameraOffsetPos += Vector3.up * offsetPos.y;
        cameraOffsetPos += target.right * offsetPos.x;
        transform.position = Vector3.Lerp(transform.position, cameraOffsetPos, moveSpeed * Time.deltaTime);

        // ���������Ŀ����ת
        LookAtTargetRotation = Quaternion.LookRotation((target.position + Vector3.up * lookAtPosHeight) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookAtTargetRotation, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �������������Ŀ��
    /// </summary>
    /// <param name="player">��ҵ�Transform</param>
    public void SetTarget(Transform player)
    {
        target = player;
    }
}
