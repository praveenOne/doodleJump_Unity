using UnityEngine;

namespace praveen.one
{
    public class SpringPlatform : Platform
    {
        #region member_variables
        Animator m_Animator;
        #endregion

        public override void Start()
        {
            base.Start();
            m_Animator = GetComponent<Animator>();
        }

        public override int OnStep(GameObject player)
        {
            m_Animator.SetTrigger("onStep");
            return base.OnStep(player);
        }
    }
}
