using UnityEngine;

public class SpringPlatform : Platform
{
    Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public override int OnStep()
    {
        m_Animator.SetTrigger("onStep");
        return base.OnStep();
    }
}
