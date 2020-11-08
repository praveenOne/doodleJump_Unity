using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SaveKeys
{
    highScore,
    lives,
    coins
}

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

    int m_Lives;
    int m_Coins;
    int m_HighScore;
    int m_Score;
    int m_LifeCost;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
        }

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    void Init()
    {
        // get save data
        m_Coins = PlayerPrefs.GetInt(SaveKeys.coins.ToString(), 0);
        m_HighScore = PlayerPrefs.GetInt(SaveKeys.highScore.ToString(), 0);
        m_Lives = PlayerPrefs.GetInt(SaveKeys.lives.ToString(), 0);

        m_LifeCost = 4;
    }

    public void StartGame()
    {
        m_Score = 0;
    }

    public void PurchaseLifes(System.Action callback)
    {
        if (m_Lives > 2)
            return;

        if(m_Coins >= m_LifeCost)
        {
            m_Lives++;
            m_Coins -= m_LifeCost;
            callback.Invoke();
        }
    }

    public int GetLifeCount()
    {
        return m_Lives;
    }

    public int GetCoinCount()
    {
        return m_Coins;
    }

    //public Transform GetPlayerTransform()
    //{
    //    return m_Player.transform;
    //}
}
