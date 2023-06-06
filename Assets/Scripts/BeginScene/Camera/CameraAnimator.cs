using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������״̬��
/// </summary>
public class CameraAnimator : MonoBehaviour
{
    private Animator animator;
    private UnityAction overAction;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// ��ת
    /// </summary>
    /// <param name="action">���Ž�����ִ�е�ί��</param>
    public void TurnLeft(UnityAction action)
    {
        overAction = action;
        animator.SetTrigger("TurnLeft");
    }

    /// <summary>
    /// ��ת
    /// </summary>
    /// <param name="action">���Ž�����ִ�е�ί��</param>
    public void TurnRight(UnityAction action)
    {
        overAction = action;
        animator.SetTrigger("TurnRight");
    }

    /// <summary>
    /// �������Ž�������
    /// </summary>
    public void PlayOver()
    {
        overAction?.Invoke();
        overAction = null;
    }
}
