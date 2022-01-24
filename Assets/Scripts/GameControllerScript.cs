using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript _instance;
    public Collider Haus1, Haus2, Haus3, Haus4, Haus5;
    public PlayerMovement MovementScript;
    public GameObject SpawnpointFolder;
    public bool MouseUnlocked = false;
    public AudioSource Music, CountdownSound;
    private int TimeRemaining, StartTimer;
    private bool GameStarted, InCredits, Starting;
    private bool[] HouseHits;

    void Start()
    {   
        GameStarted = false;
        InCredits   = false;
        InitPlayerPosition();
    }
    void Update()
    {
        if(!GameStarted && !InCredits && !Starting && Input.GetKeyDown(KeyCode.Return))
        {   
            StartTimer = GlobalConfig.StartTime;
            UIController._instance.SetCountdownTime(StartTimer); 

            InitTimer();
            InitScore();

            Starting = true;
            Cursor.lockState = CursorLockMode.Locked;
            
            UIController._instance.SetMenuBackgroundVisible(false);
            UIController._instance.SetMainMenuVisible(false);
            UIController._instance.SetGameUIVisible(true);
            UIController._instance.SetCountdownVisible(true);
            InvokeRepeating("CountdownFunction", 1f, 1f);
        }

        if (Input.GetKey("escape"))
            Application.Quit();
            
        if(!GameStarted && !Starting &&  Input.GetKeyDown(KeyCode.M))
            ToggleCredits();        

        if(Application.isEditor&&Input.GetKeyDown(KeyCode.L))
            ToggleCursor();
    }

    void CountdownFunction()
    {
        CountdownSound.Play();
        StartTimer--;
        if(StartTimer == 0)
        {
            CancelInvoke();
            UIController._instance.SetCountdownVisible(false);
            StartGame();   
        }else{
            UIController._instance.SetCountdownTime(StartTimer);
        }
    }

    public void StartGame()
    {   
        InvokeRepeating("TimerFunction", 1f, 1f);
        Starting = false;
        GameStarted = true;
        Music.Play();
    }

    public void HouseHit(Collider pHouse)
    {
        if(pHouse == Haus1)
        {
            HouseHits[0] = true;
        }
        else if(pHouse == Haus2)
        {
            HouseHits[1] = true;
        }
        else if(pHouse == Haus3)
        {
            HouseHits[2] = true;
        }
        else if(pHouse == Haus4)
        {
            HouseHits[3] = true;
        }
        else if(pHouse == Haus5)
        {
            HouseHits[4] = true;
        }
        UpdateScore();
    }

    void TimerFunction()
    {
        TimeRemaining--;
        UIController._instance.SetTimeRemainingText(TimeRemaining);
        if(TimeRemaining <= 0){
            GameOver(false);
        }
    }

    void ToggleCredits()
    {
        UIController._instance.SetMainMenuVisible(InCredits);
        UIController._instance.SetCreditVisible(!InCredits);
        InCredits = !InCredits;
    }

    void InitTimer()
    {
        TimeRemaining = GlobalConfig.GameTime;
        UIController._instance.SetTimeRemainingText(TimeRemaining);
    }

    void InitScore()
    {
        HouseHits = new bool[5];
        UpdateScore();
    }
    void UpdateScore()
    {
        int HousesRemaining = 5;
        foreach(bool isHit in HouseHits)
        {
            if(isHit)
            {
                HousesRemaining--;
            }
        }
        if(HousesRemaining > 0){
            UIController._instance.SetScoreText(HousesRemaining);
        }else{
            GameOver(true);
        }
        
    }

    public void GameOver(bool Win)
    {   
        GameStarted = false;
        CancelInvoke();
        InitPlayerPosition();
        UIController._instance.SetMenuBackgroundVisible(true);
        UIController._instance.SetGameUIVisible(false);
        UIController._instance.SetMainMenuVisible(true);
        UIController._instance.SetGameOverText(Win);
        Music.Stop();
    }

    void InitPlayerPosition()
    {   
        int SpawnPointID     = Random.Range(0, SpawnpointFolder.transform.childCount - 1);   
        Transform Spawnpoint = SpawnpointFolder.transform.GetChild(SpawnPointID);

        MovementScript.resetPos(Spawnpoint.position, Spawnpoint.rotation);
    }

    public bool GetGameStarted(){
        return GameStarted;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void ToggleCursor()
    {
        Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked)? CursorLockMode.None : CursorLockMode.Locked;   
        MouseUnlocked = (Cursor.lockState != CursorLockMode.Locked);
    }
}
