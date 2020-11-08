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
    [SerializeField] Text m_LifeBuyBtnTxt;

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
        Debug.Log(lives);
        m_lifeMeeter.SetCount(lives);

        if (!GameManager.Instance.CanPurchaseLifes())
        {
            m_LifeBuyBtn.SetActive(false);
        }
        else
        {
            m_LifeBuyBtnTxt.text = "Buy Life For " + GameManager.Instance.GetPurchaseCost() + " Coins";
        }

        m_Coins.text = GameManager.Instance.GetCoinCount().ToString();
    }

    public void OnClickPlayButton()
    {
        GameManager.Instance.ChangeScene(GameScenes.Game);
    }
}
