using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region singleton stuff
    private static HUD m_Instance;

    public static HUD Instance
    {
        get { return m_Instance; }
    }
    #endregion

    [SerializeField] LifeMeeter m_LifeMeeter;
    [SerializeField] Text m_Coins;
    [SerializeField] Text m_Score;
    [SerializeField] GameObject m_HighScore;
    [SerializeField] CountdownAnimation m_Countdown;

    private void Awake()
    {
        m_Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InItPaintHUD();
    }

    void InItPaintHUD()
    {
        int lives = GameManager.Instance.GetLifeCount();
        m_LifeMeeter.SetCount(lives);
        m_Coins.text = GameManager.Instance.GetCoinCount().ToString();
        m_Score.text = "0";
        m_HighScore.SetActive(false);
    }

    public void SetCoinCount(int coins)
    {
        m_Coins.text = coins.ToString();
    }

    public void SetLifeCount(int lifes)
    {
        m_LifeMeeter.SetCount(lifes);
    }

    public void SetScore(int score)
    {
        m_Score.text = score.ToString();
    }

    public void StartCountdown(System.Action callback)
    {
        m_Countdown.StartCountdown(callback);
    }
   
}
