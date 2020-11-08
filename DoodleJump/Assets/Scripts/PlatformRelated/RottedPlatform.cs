using System.Collections;
using UnityEngine;

public class RottedPlatform : Platform
{
    public override int OnStep(GameObject player)
    {
        StartCoroutine(DestroyPlatform());
        return base.OnStep(player);
    }

    IEnumerator DestroyPlatform()
    {
        yield return null;
        Recycle();
    }
}
