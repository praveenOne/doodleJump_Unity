using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    normal,
    rotted
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
}
