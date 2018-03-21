using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordsManager : MonoBehaviour
{
    private List<Record> records = new List<Record>();
    public List<GameObject> recordPanels = new List<GameObject>();

    public Text textDifficulty;
    public DifficultyLevel selectedDifficultyLevel = DifficultyLevel.Easy;

    // Use this for initialization
    private void Start()
    {
        SelectDifficulty(0);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void SelectDifficulty(int level)
    {
        switch (level)
        {
            case 0:
                selectedDifficultyLevel = DifficultyLevel.Easy;
                break;
            case 1:
                selectedDifficultyLevel = DifficultyLevel.Normal;
                break;
            case 2:
                selectedDifficultyLevel = DifficultyLevel.Hard;
                break;
            case 3:
                selectedDifficultyLevel = DifficultyLevel.Hardcore;
                break;
        }
        textDifficulty.text = LocalizationManager.Localization(LoadingParameters.GetKey(selectedDifficultyLevel));
        records = LoadingParameters.GetRecords(selectedDifficultyLevel);
        for (int i = 0; i < records.Count; i++)
        {
            foreach (Text panelText in recordPanels[i].GetComponentsInChildren<Text>())
            {
                if (panelText.name == "TextGold")
                {
                    panelText.text = string.Format("{0}:  {1}", LocalizationManager.Localization("Gold"),
                        records[i].gold);
                    if (records[i].gold == 0)
                    {
                        panelText.text = string.Format("{0}: -", LocalizationManager.Localization("Gold"));
                    }
                }
                if (panelText.name == "TextTime")
                {
                    panelText.text = string.Format("{0}:  {1:F1}", LocalizationManager.Localization("Time"),
                        records[i].time);
                    if (records[i].time == 0)
                    {
                        panelText.text = string.Format("{0}: -", LocalizationManager.Localization("Time"));
                    }
                }
            }
        }
    }
}
