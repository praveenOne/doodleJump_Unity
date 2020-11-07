using System.Collections;

public class BreakingPlatform : Platform
{
    public override int OnStep()
    {
        StartCoroutine(DestroyPlatform());
        return base.OnStep();
    }

    IEnumerator DestroyPlatform()
    {
        yield return null;
        PlatformManager.Instance.DestroyPlatform(m_Type, gameObject);
    }
}
