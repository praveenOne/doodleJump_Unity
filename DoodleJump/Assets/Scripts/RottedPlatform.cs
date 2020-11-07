using System.Collections;

public class RottedPlatform : Platform
{
    public override int OnStep()
    {
        StartCoroutine(DestroyPlatform());
        return base.OnStep();
    }

    IEnumerator DestroyPlatform()
    {
        yield return null;
        Recycle();
    }
}
