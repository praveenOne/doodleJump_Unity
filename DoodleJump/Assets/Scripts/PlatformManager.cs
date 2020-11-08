using UnityEngine;
using Util;

public enum PlatformSide
{
    left,
    right,
    either
}

public class PlatformManager : MonoBehaviour
{

    [SerializeField] GameObject[] m_Platform;

    float m_RightX;
    float m_LeftX;
    Camera m_Camera;
    ObjectPool[] m_PlatformPool;
    Transform m_PreviousPlatform;

    #region singleton stuff
    private static PlatformManager m_Instance;

    public static PlatformManager Instance
    {
        get { return m_Instance; }
    }
    #endregion

    private void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        m_Camera = Camera.main;
        m_LeftX = m_Camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        m_RightX = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;

        m_PlatformPool = new ObjectPool[m_Platform.Length];

        for (int i = 0; i < m_Platform.Length; i++)
        {
            m_PlatformPool[i] = new ObjectPool(m_Platform[i], 20, true, transform);
        }

        GameObject firstPt = m_PlatformPool[0].Spawn();
        m_PreviousPlatform = firstPt.transform;
        for (int i = 0; i < 50; i++)
        {
            CreatePlatforms();
        }
    }

    public float GetLeftBoundry()
    {
        return m_LeftX;
    }

    public float GetRightBoundry()
    {
        return m_RightX;
    }

    void CreatePlatforms()
    {
        GameObject go = m_PlatformPool[Random.Range(0, m_Platform.Length)].Spawn();
        go.transform.SetParent(transform);
        go.transform.position = GetNextPlatformPos(m_PreviousPlatform, go.transform);
        m_PreviousPlatform = go.transform;
    }

    public bool IsPlatformDissapear(Transform platrom)
    {
        return m_Camera.WorldToScreenPoint(platrom.position).y < -100;
    }

    Vector3 GetNextPlatformPos(Transform prevPlatform, Transform currPlatform)
    {
        Vector3 pos = Vector3.zero;
        PlatformSide sideTiSpawn = GetSideToSpawn(prevPlatform, currPlatform);
        switch (sideTiSpawn)
        {
            case PlatformSide.right:
                pos = new Vector3(Random.Range(prevPlatform.position.x, m_RightX), prevPlatform.position.y + 2, 0);
                break;
            case PlatformSide.left:
                pos = new Vector3(Random.Range(m_LeftX, prevPlatform.position.x), prevPlatform.position.y + 2, 0);
                break;
            case PlatformSide.either:
                pos = new Vector3(Random.Range(m_LeftX, m_RightX), prevPlatform.position.y + 2, 0);
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

    public void DestroyPlatform(PlatformType type, GameObject platrom)
    {
        m_PlatformPool[(int)type].Recycle(platrom);
        CreatePlatforms();
    }
}
