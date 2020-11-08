using UnityEngine;

public class CountdownAnimation : MonoBehaviour
{
    System.Action m_Callback;
    [SerializeField] Animator m_Animator;

    public void StartCountdown(System.Action callback)
    {
        m_Animator.SetTrigger("OnStart");
        m_Callback = callback;
    }

    public void OnFinishCountdown()
    {
        if (m_Callback != null)
        {
            m_Callback.Invoke();
        }
    }
}
