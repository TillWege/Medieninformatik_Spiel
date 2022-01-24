using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController _instance;
    public Text ScoreText, TimerText, MainMenuText, MainMenuTextHeader, MainMenuCountdown, MainMenuCredits;
    public GameObject UIPanel;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void SetGameOverText(bool pWin)
    {
        MainMenuTextHeader.text = string.Format("Du hast {0}!", pWin? "Gewonnen": "Verloren");
        MainMenuText.text = "<Enter zum neustarten>";
    }

    public void SetScoreText(int pRemainingHouses)
    {
        ScoreText.text = string.Format("{0} Häuser Verbleibend", pRemainingHouses);
    }

    public void SetTimeRemainingText(int pRemainingTime)
    {
        TimerText.text = string.Format("{0} Sekunden Verbleibend", pRemainingTime);
    }

    public void SetCreditVisible(bool pVisible)
    {
        MainMenuCredits.gameObject.SetActive(pVisible);
    }

    public void SetMenuBackgroundVisible(bool pVisible)
    {
        UIPanel.SetActive(pVisible);
    }

    public void SetMainMenuVisible(bool pVisible)
    {
        MainMenuText.gameObject.SetActive(pVisible);
        MainMenuTextHeader.gameObject.SetActive(pVisible);
    }

    public void SetGameUIVisible(bool pVisible)
    {
        ScoreText.gameObject.SetActive(pVisible);
        TimerText.gameObject.SetActive(pVisible);
    }

    public void SetCountdownVisible(bool pVisible)
    {
        MainMenuCountdown.gameObject.SetActive(pVisible);    
    }

    public void SetCountdownTime(int pTime)
    {
        MainMenuCountdown.text = pTime.ToString();
    }
}
