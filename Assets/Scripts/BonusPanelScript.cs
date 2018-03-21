using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BonusPanelScript : MonoBehaviour
{
    public Text text;
    public Text textCurrent;
    public Text textNext;
    public Text textGold;
    public Button buttonBuy;
    public int minValue;
    public int maxValue;
    public string valueName;
    public int valueFactor;
    public int startGold;
    public int gold;
    public int goldFactor;

    // Use this for initialization
    private void Awake()
    {
        buttonBuy.onClick.AddListener(IncreaseValue);

        if (PlayerPrefs.GetInt(valueName) < minValue)
        {
            PlayerPrefs.SetInt(valueName, minValue);
        }

        text.text = LocalizationManager.Localization(gameObject.name);

        gold = startGold + (PlayerPrefs.GetInt(valueName) - minValue)*goldFactor;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.PlayerGoldAll < gold)
        {
            buttonBuy.interactable = false;
        }
        else
        {
            buttonBuy.interactable = true;
        }
        gold = startGold + (PlayerPrefs.GetInt(valueName) - minValue)/valueFactor * goldFactor;
        if (PlayerPrefs.GetInt(valueName) == maxValue)
        {
            buttonBuy.interactable = false;
            textNext.text = "-";
            textGold.text = "-";
        }
        else
        {
            textNext.text = string.Format("{0}: {1}",LocalizationManager.Localization("Next"), PlayerPrefs.GetInt(valueName) + valueFactor);
            textGold.text = string.Format("{0}: {1}",LocalizationManager.Localization("Gold"), gold);
        }
        textCurrent.text = string.Format("{0}: {1}",LocalizationManager.Localization("Current"), PlayerPrefs.GetInt(valueName));
    }

    public void IncreaseValue()
    {
        if (PlayerPrefs.GetInt(valueName) < maxValue)
        {
            PlayerPrefs.SetInt(valueName, PlayerPrefs.GetInt(valueName) + valueFactor);
            GameEngine.PlayerGoldAll -= gold;
            gold += goldFactor;
            LoadingParameters.Save();
        }
    }
}
