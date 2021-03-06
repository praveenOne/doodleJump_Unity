﻿using UnityEngine;
using UnityEngine.UI;

namespace praveen.one
{
    public class Menu : MonoBehaviour
    {
        #region member_variables
        [SerializeField] LifeMeeter m_lifeMeeter;
        [SerializeField] GameObject m_LifeBuyBtn;
        [SerializeField] Text m_HighScore;
        [SerializeField] Text m_Coins;
        [SerializeField] Text m_LifeBuyBtnTxt;
        #endregion

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
            m_HighScore.text = GameManager.Instance.GetHighScore().ToString();
            m_lifeMeeter.SetCount(GameManager.Instance.GetLifeCount());

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
}
