using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region singleton stuff
    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get { return m_Instance; }
    }
    #endregion

    [SerializeField] GameObject m_Player;

    private void Awake()
    {
        m_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform GetPlayerTransform()
    {
        return m_Player.transform;
    }
}
