using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SaveKeys
{
    highScore,
    lives,
    coins
}

public enum GameScenes
{
    Menu,
    Game
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

    [SerializeField] GameObject m_PlayerPrefab;
    [SerializeField] int[] m_LifeCost;

    int m_Lives;
    int m_Coins;
    int m_HighScore;
    int m_Score;
    Player m_Player;

    private void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Enum.TryParse(scene.name, out GameScenes thisScene);

        switch (thisScene)
        {
            case GameScenes.Game:
                // start countdown and game
                HUD.Instance.StartCountdown(StartGame);
                break;
            case GameScenes.Menu:
                break;
        }

    }


    void Init()
    {
        // get save data
        m_Coins = PlayerPrefs.GetInt(SaveKeys.coins.ToString(), 0);
        m_HighScore = PlayerPrefs.GetInt(SaveKeys.highScore.ToString(), 0);
        m_Lives = PlayerPrefs.GetInt(SaveKeys.lives.ToString(), 0);

        //m_Lives = 1;
        //m_HighScore = 2;
        //m_Coins = 100;
    }

    void SaveData()
    {
        PlayerPrefs.SetInt(SaveKeys.coins.ToString(), m_Coins);
        PlayerPrefs.SetInt(SaveKeys.highScore.ToString(), m_HighScore);
        PlayerPrefs.SetInt(SaveKeys.lives.ToString(), m_Lives);
    }

    public void StartGame()
    {
        m_Score = 0;
        GameObject player = Instantiate(m_PlayerPrefab);
        m_Player = player.GetComponent<Player>();
        HUD.Instance.SetCamTarget(player.transform);
    }

    public void PurchaseLifes(System.Action callback)
    {
        if (m_Lives > 2)
            return;

        if(m_Coins >= m_LifeCost[m_Lives])
        {
            m_Coins -= m_LifeCost[m_Lives];
            m_Lives++;
            callback.Invoke();
        }
        SaveData();
    }

    public void GetCoin()
    {
        m_Coins += 1;
        HUD.Instance.SetCoinCount(m_Coins);
    }

    private void SpendLife()
    {
        m_Lives -= 1;
        HUD.Instance.SetLifeCount(m_Lives);
        SaveData();
    }

    public void OnPlayerDie()
    {
        if(m_Lives > 0)
        {
            HUD.Instance.PopupMessage("You Died", "Use 1 Heart And Retry!", (bool feedback) =>
            {
                if (feedback)
                {
                    SpendLife();
                    StartCoroutine(m_Player.ActivateShield());
                }
                else
                {
                    GameOver();
                }
            });
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SaveData();
        ChangeScene(GameScenes.Menu);
    }

    public int GetLifeCount()
    {
        return m_Lives;
    }

    public int GetCoinCount()
    {
        return m_Coins;
    }

    public int GetHighScore()
    {
        return m_HighScore;
    }

    public bool CanPurchaseLifes()
    {
        if (m_Lives > 2)
            return false;

        return m_Coins > m_LifeCost[m_Lives];
    }

    public int GetPurchaseCost()
    {
        if (m_Lives > 2)
            return -1;

        return m_LifeCost[m_Lives];
    }

    public void OnStepPlatform()
    {
        m_Score += 1;
        HUD.Instance.SetScore(m_Score);
        if(m_Score > m_HighScore)
        {
            StartCoroutine(HUD.Instance.DisplayHighScore());
            m_HighScore = m_Score;
        }
    }

    public void ChangeScene(GameScenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
