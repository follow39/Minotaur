using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Canvas canvasShop;
    public Canvas canvasRecords;
    public GameObject buttonsPanel;

    // Use this for initialization
    private void Start()
    {
        ShowShop();
        ShowRecords();
        LoadingParameters.Load();
        foreach (Text text in buttonsPanel.GetComponentsInChildren<Text>())
        {
            text.text = LocalizationManager.Localization(text.name);
        }

        HideRecords();
        HideShop();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(canvasShop.enabled)
            {
                canvasShop.enabled = false;
                return;
            }
            if (canvasRecords.enabled)
            {
                canvasRecords.enabled = false;
                return;
            }
            Exit();
        }
    }

    public void StartEasyLevel()
    {
        GameEngine.GameDifficultyLevel = DifficultyLevel.Easy;

        Instantiate(loadingScreen);
        Application.LoadLevel(1);
    }

    public void StartNormalLevel()
    {
        GameEngine.GameDifficultyLevel = DifficultyLevel.Normal;

        Instantiate(loadingScreen);
        Application.LoadLevel(1);
    }

    public void StartHardLevel()
    {
        GameEngine.GameDifficultyLevel = DifficultyLevel.Hard;

        Instantiate(loadingScreen);
        Application.LoadLevel(1);
    }

    public void StartHardcoreLevel()
    {
        GameEngine.GameDifficultyLevel = DifficultyLevel.Hardcore;

        Instantiate(loadingScreen);
        Application.LoadLevel(1);
    }

    public void Exit()
    {
        LoadingParameters.Save();
        Application.Quit();
    }

    public void ShowShop()
    {
        canvasShop.enabled = true;
        canvasRecords.enabled = false;
    }

    public void HideShop()
    {
        canvasShop.enabled = false;
    }

    public void ShowRecords()
    {
        canvasRecords.enabled = true;
        canvasShop.enabled = false;
    }

    public void HideRecords()
    {
        canvasRecords.enabled = false;
    }
}
