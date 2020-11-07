using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int m_BounceForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual int OnStep()
    {
        return m_BounceForce;
    }
}
