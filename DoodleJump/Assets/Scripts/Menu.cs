using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void OnClickPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
