using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Canvas canvasGame;
    private Canvas canvasPause;
    private Canvas canvasEducation;
    public GameObject loadingScreen;
    List<Button> buttonsList = new List<Button>();
    private MoveDirection specifiedDirection = MoveDirection.None;
    public bool collectingGold = false;

    public GameObject damageObject;
    public GameObject detectedObject;
    
    public static Action RunningEvent;


    // Use this for initialization
    private void Start()
    {
        Monster.DamageDeal += () => Instantiate(damageObject);
        GameEngine.DetectingEvent += () => Instantiate(detectedObject);

        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "CanvasGame")
            {
                canvasGame = canvas;
            }
            if (canvas.name == "CanvasPause")
            {
                canvasPause = canvas;
            }
            if (canvas.name == "CanvasEducation")
            {
                canvasEducation = canvas;
                foreach (Text text in canvas.GetComponentsInChildren<Text>())
                {
                    text.text = LocalizationManager.Localization(text.name);
                }
            }
        }
        foreach (Button button in FindObjectsOfType<Button>())
        {
            buttonsList.Add(button);
            if (button.name == "ButtonRestart")
            {
                if (Application.systemLanguage != SystemLanguage.English)
                {
                    button.GetComponentInChildren<Text>().fontSize = 16;
                }
                button.GetComponentInChildren<Text>().text = LocalizationManager.Localization("Restart");
                button.onClick.AddListener(RestartGame);
            }
            if (button.name == "ButtonExit")
            {
                if (Application.systemLanguage != SystemLanguage.English)
                {
                    button.GetComponentInChildren<Text>().fontSize = 16;
                }
                button.GetComponentInChildren<Text>().text = LocalizationManager.Localization("Exit");
                button.onClick.AddListener(Application.Quit);
            }
            if (button.name == "ButtonPause")
            {
                button.onClick.AddListener(Pause);
            }
            if (button.name == "ButtonResume")
            {
                if (Application.systemLanguage != SystemLanguage.English)
                {
                    button.GetComponentInChildren<Text>().fontSize = 16;
                }
                button.GetComponentInChildren<Text>().text = LocalizationManager.Localization("Resume");
                button.onClick.AddListener(Unpause);
            }
            if (button.name == "ButtonFinish")
            {
                button.onClick.AddListener(EndGameWin);
            }
            if (button.name == "ButtonMenu")
            {
                button.onClick.AddListener(() =>
                {
                    EndGameLose();
                    Unpause();
                    Instantiate(loadingScreen);
                    Application.LoadLevel(0);
                });
                if (Application.systemLanguage != SystemLanguage.English)
                {
                    button.GetComponentInChildren<Text>().fontSize = 16;
                }
                button.GetComponentInChildren<Text>().text = LocalizationManager.Localization("Menu");
            }
        }

        Unpause();

        if (GameEngine.GameDifficultyLevel != DifficultyLevel.Easy)
        {
            canvasEducation.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
        if (GameEngine.GameIsStarted)
        {
            Time.timeScale = 1;
            canvasEducation.enabled = false;
        }
        if (GameEngine.PlayerHitPoints < 1)
        {
            GameEngine.GameIsFinished = true;
            if (Camera.main.fieldOfView > 25)
            {
                Camera.main.fieldOfView -= 7.5f * Time.deltaTime;
                return;
            }
            EndGameLose();
        }

        if (GameEngine.PlayerCoordinates.x < 2.0f && GameEngine.PlayerCoordinates.z < 2.0f)
        {
            buttonsList.Find((button) => button.name == "ButtonFinish").gameObject.SetActive(true);
            if (GameEngine.PlayerGold > 0)
            {
                buttonsList.Find((button) => button.name == "ButtonFinish").interactable = true;
            }
            else
            {
                buttonsList.Find((button) => button.name == "ButtonFinish").interactable = false;
            }
        }
        else
        {
            buttonsList.Find((button) => button.name == "ButtonFinish").gameObject.SetActive(false);
        }

        switch (specifiedDirection)
        {
            case MoveDirection.Up:
                FindObjectOfType<Player>().MoveUp();
                break;
            case MoveDirection.Left:
                FindObjectOfType<Player>().MoveLeft();
                break;
            case MoveDirection.Down:
                FindObjectOfType<Player>().MoveDown();
                break;
            case MoveDirection.Right:
                FindObjectOfType<Player>().MoveRight();
                break;
        }
        if (collectingGold)
        {
            FindObjectOfType<Player>().CollectGold();
        }
        else
        {
            GameEngine.CollectingGold = false;
        }
    }

    public void Pause()
    {
        GameEngine.GameIsPaused = !GameEngine.GameIsPaused;
        if (GameEngine.GameIsPaused)
        {
            Time.timeScale = 0;
            canvasGame.enabled = false;
            canvasPause.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            canvasGame.enabled = true;
            canvasPause.enabled = false;
        }
    }

    public void Unpause()
    {
        GameEngine.GameIsPaused = false;
        Time.timeScale = 1;
        canvasGame.enabled = true;
        canvasPause.enabled = false;
    }

    public void RestartGame()
    {
        Instantiate(loadingScreen);
        GameEngine.CreateMap = false;
        Unpause();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void EndGameLose()
    {
        PlayerPrefs.SetInt("GamesCount", (PlayerPrefs.GetInt("GamesCount") + 1));
        GameEngine.GameIsFinished = true;
        canvasGame.enabled = false;
        canvasPause.enabled = true;
        buttonsList.Find((button) => button.name == "ButtonResume").gameObject.SetActive(false);
    }

    public void EndGameWin()
    {
        PlayerPrefs.SetInt("GamesCount", (PlayerPrefs.GetInt("GamesCount") + 1));
        PlayerPrefs.SetInt("GamesCountWin", (PlayerPrefs.GetInt("GamesCountWin") + 1));
        LoadingParameters.SaveRecord(GameEngine.GameDifficultyLevel,
            new Record() { gold = GameEngine.PlayerGold, time = GameEngine.GameTime });
        GameEngine.AddGold();
        GameEngine.GameIsFinished = true;
        canvasGame.enabled = false;
        canvasPause.enabled = true;
        buttonsList.Find((button) => button.name == "ButtonResume").gameObject.SetActive(false);
        LoadingParameters.Save();
    }

    public void ButtonDown(int x)
    {
        Time.timeScale = 1;
        switch (x)
        {
            case 0:
                specifiedDirection = MoveDirection.Up;
                break;
            case 1:
                specifiedDirection = MoveDirection.Left;
                break;
            case 2:
                specifiedDirection = MoveDirection.Down;
                break;
            case 3:
                specifiedDirection = MoveDirection.Right;
                break;
            case 4:
                collectingGold = true;
                break;
            case 5:
                /*if (RunningEvent != null)
                {
                    RunningEvent();
                }*/

                FindObjectOfType<Player>().ChangeRun();
                break;
        }
    }

    public void ButtonUp()
    {
        specifiedDirection = MoveDirection.None;
        collectingGold = false;
    }

    public void ShowInfo()
    {
        if (GameEngine.GameIsFinished)
        {
            return;
        }
        canvasEducation.enabled = true;
        GameEngine.GameIsStarted = false;
        Unpause();
        Time.timeScale = 0;
    }

    public void ButtonRunning()
    {
        if (RunningEvent != null)
        {
            RunningEvent();
        }
    }
}
