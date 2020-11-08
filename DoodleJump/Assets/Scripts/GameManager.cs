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

    int m_Lives;
    int m_Coins;
    int m_HighScore;
    int m_Score;
    int m_LifeCost;
    Player m_Player;

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

        m_LifeCost = 4;
        m_Lives = 3;
        m_HighScore = 2;
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

        if(m_Coins >= m_LifeCost)
        {
            m_Lives++;
            m_Coins -= m_LifeCost;
            callback.Invoke();
        }
    }

    private void SpendLife()
    {
        m_Lives -= 1;
        HUD.Instance.SetLifeCount(m_Lives);
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
                    ChangeScene(GameScenes.Menu);
                }
            });
        }
        else
        {
            ChangeScene(GameScenes.Menu);
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

    public void OnStepPlatform()
    {
        m_Score += 1;
        HUD.Instance.SetScore(m_Score);
        if(m_Score > m_HighScore)
        {
            StartCoroutine(HUD.Instance.DisplayHighScore());
        }
    }

    public void ChangeScene(GameScenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
