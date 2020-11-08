using UnityEngine;

public class SpringPlatform : Platform
{
    Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public override int OnStep(GameObject player)
    {
        m_Animator.SetTrigger("onStep");
        return base.OnStep(player);
    }
}
