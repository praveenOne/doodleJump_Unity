using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    normal = 0,
    rotted = 1,
    spring = 2,
    moving = 3
}

public class Platform : MonoBehaviour
{
    public int m_BounceForce;
    public PlatformType m_Type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual int OnStep()
    {
        return m_BounceForce;
    }

    public virtual void Update()
    {
        if (PlatformManager.Instance.IsPlatformDissapear(gameObject.transform))
        {
            Recycle();
        }
    }

    public void Recycle()
    {
        PlatformManager.Instance.DestroyPlatform(m_Type, gameObject);
    }
}
