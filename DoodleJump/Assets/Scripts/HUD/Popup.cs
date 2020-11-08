using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    System.Action<bool> m_Callback;

    [SerializeField] Text m_Title;
    [SerializeField] Text m_Message;

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
