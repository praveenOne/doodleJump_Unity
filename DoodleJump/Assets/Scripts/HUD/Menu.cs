using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] LifeMeeter m_lifeMeeter;
    [SerializeField] GameObject m_LifeBuyBtn;
    [SerializeField] Text m_HighScore;
    [SerializeField] Text m_Coins;

    private void Start()
    {
        PaintMenu();
    }

    public void OnClickLifeBuy()
    {
        GameManager.Instance.PurchaseLifes(PaintMenu);
    }


    void PaintMenu()
    {
        int lives = GameManager.Instance.GetLifeCount();
        m_lifeMeeter.SetCount(lives);

        if (lives > 2)
        {
            m_LifeBuyBtn.SetActive(false);
        }

        m_Coins.text = GameManager.Instance.GetCoinCount().ToString();
    }

    public void OnClickPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
