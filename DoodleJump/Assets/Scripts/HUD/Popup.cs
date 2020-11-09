using UnityEngine;
using UnityEngine.UI;


namespace praveen.one
{
    public class Popup : MonoBehaviour
    {
        #region member_variables
        System.Action<bool> m_Callback;
        [SerializeField] Text m_Title;
        [SerializeField] Text m_Message;
        #endregion

        public void Show(string title, string message, System.Action<bool> callback)
        {
            m_Callback = callback;
            m_Title.text = title;
            m_Message.text = message;
            gameObject.SetActive(true);
        }

        public void OnClickOKBtn()
        {
            if (m_Callback != null)
            {
                m_Callback.Invoke(true);
            }
            gameObject.SetActive(false);
        }

        public void OnClickNOBtn()
        {
            if (m_Callback != null)
            {
                m_Callback.Invoke(false);
            }
            gameObject.SetActive(false);
        }
    }
}
