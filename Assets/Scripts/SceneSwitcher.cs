using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GoToWar()
    {
        SceneManager.LoadScene("War");
    }

    public void GoToSlapJack()
    {
        SceneManager.LoadScene("SlapJack");
    }

    public void GoToGOPS()
    {
        SceneManager.LoadScene("GOPS");
    }
    public void GoToBlackJack()
    {
        SceneManager.LoadScene("BlackJack");
    }
}
