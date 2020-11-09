using UnityEngine;

namespace praveen.one
{
    public class CountdownAnimation : MonoBehaviour
    {
        #region member_variables
        System.Action m_Callback;
        [SerializeField] Animator m_Animator;
        #endregion

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
}
