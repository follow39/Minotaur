using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvertisingPanelScript : MonoBehaviour
{
    public Text text;
    public Text textGold;
    public Button buttonBuy;
    public string valueName;
    public int gold;
    public int factor;

    // Use this for initialization
    private void Start()
    {
        buttonBuy.onClick.AddListener(BuyFactor);

        if (PlayerPrefs.GetInt(valueName) < 2)
        {
            PlayerPrefs.SetInt(valueName, 2);
        }

        text.text = LocalizationManager.Localization(gameObject.name);

        textGold.text = string.Format("{0}: {1}",LocalizationManager.Localization("Gold"), gold);
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

        if (PlayerPrefs.GetInt(valueName) >= factor)
        {
            buttonBuy.interactable = false;
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.GetComponent<AdvertisingPanelScript>().enabled = false;
        }
    }

    public void BuyFactor()
    {
        GameEngine.PlayerGoldAll -= gold;
        PlayerPrefs.SetInt(valueName, factor);
        LoadingParameters.Save();
    }
}
