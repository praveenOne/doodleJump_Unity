using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] LifeMeeter m_LifeMeeter;
    [SerializeField] Text m_Coins;
    [SerializeField] Text m_Score;
    [SerializeField] GameObject m_HighScore;
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
    }
}
