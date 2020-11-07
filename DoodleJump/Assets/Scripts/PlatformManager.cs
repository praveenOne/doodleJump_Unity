using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformSide
{
    left,
    right,
    either
}

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_Platform;
    [SerializeField] Transform m_FirstPlatform;
    float m_RightX;
    float m_LeftX;
    Camera m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlatforms();

        m_Camera = Camera.main;
        m_LeftX = m_Camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        m_RightX = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        Debug.Log(m_LeftX);
        Debug.Log(m_RightX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePlatforms()
    {
        Transform prevPlatform = m_FirstPlatform;
        for (int i = 0; i < 100; i++)
        {
            GameObject go = Instantiate(m_Platform[(int)Random.Range(0,2)],this.transform);
            go.transform.position = GetNextPlatformPos(prevPlatform,go.transform);
            prevPlatform = go.transform;
        }
    }

    //Vector3 GetNextPosition(Vector3 prevPos)
    //{
    //    Vector3 pos = new Vector3(Random.Range(-2.8f,2.8f), prevPos.y + 3, prevPos.z);
    //    return pos;
    //}

    Vector3 GetNextPlatformPos(Transform prevPlatform, Transform currPlatform)
    {
        Vector3 pos = Vector3.zero;
        PlatformSide sideTiSpawn = GetSideToSpawn(prevPlatform, currPlatform);
        switch (sideTiSpawn)
        {
            case PlatformSide.right:
                pos = new Vector3(Random.Range(prevPlatform.position.x, 2.8f), prevPlatform.position.y + 2, 0);
                break;
            case PlatformSide.left:
                pos = new Vector3(Random.Range( -2.8f, prevPlatform.position.x), prevPlatform.position.y + 2, 0);
                break;
            case PlatformSide.either:
                pos = new Vector3(Random.Range(-2.8f, 2.8f), prevPlatform.position.y + 2, 0);
                break;
        }
        return pos;
    }

    PlatformSide GetSideToSpawn(Transform prevPlatform, Transform currPlatform)
    {
        SpriteRenderer prePlatform = prevPlatform.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer curPlatform = currPlatform.gameObject.GetComponent<SpriteRenderer>();
        if ((prevPlatform.position.x + prePlatform.bounds.size.x/2 + curPlatform.bounds.size.x  < m_RightX)
            && (prevPlatform.position.x - (prePlatform.bounds.size.x / 2 + curPlatform.bounds.size.x) > m_LeftX))
        {
            return PlatformSide.either;
        }
        else if(prevPlatform.position.x + prePlatform.bounds.size.x / 2  + curPlatform.bounds.size.x < m_RightX)
        {
            return PlatformSide.right;
        }
        else
        {
            return PlatformSide.left;
        }
        
    }
}
