using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 相机动画状态机
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
    /// 左转
    /// </summary>
    /// <param name="action">播放结束后执行的委托</param>
    public void TurnLeft(UnityAction action)
    {
        overAction = action;
        animator.SetTrigger("TurnLeft");
    }

    /// <summary>
    /// 右转
    /// </summary>
    /// <param name="action">播放结束后执行的委托</param>
    public void TurnRight(UnityAction action)
    {
        overAction = action;
        animator.SetTrigger("TurnRight");
    }

    /// <summary>
    /// 动画播放结束方法
    /// </summary>
    public void PlayOver()
    {
        overAction?.Invoke();
        overAction = null;
    }
}
