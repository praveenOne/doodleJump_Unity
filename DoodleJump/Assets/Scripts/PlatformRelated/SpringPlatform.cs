using UnityEngine;

public class SpringPlatform : Platform
{
    Animator m_Animator;

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
