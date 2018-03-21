using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemsPanelScript : MonoBehaviour
{
    public Text text;
    public Text textCount;
    public Text textCooldown;
    public Text textGold;
    public Button buttonBuy;
    public string valueName;
    public int gold;
    public int cooldown;

    // Use this for initialization
    private void Start()
    {
        buttonBuy.onClick.AddListener(BuyItem);

        text.text = LocalizationManager.Localization(gameObject.name);

        textCooldown.text = string.Format("{0}: {1}",LocalizationManager.Localization("Cooldown"), cooldown);
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
        textCount.text = string.Format("{0}: {1}",LocalizationManager.Localization("Count"), PlayerPrefs.GetInt(valueName));
        textGold.text = string.Format("{0}: {1}",LocalizationManager.Localization("Gold"), gold);
    }

    public void BuyItem()
    {
        PlayerPrefs.SetInt(valueName, PlayerPrefs.GetInt(valueName)+1);
        GameEngine.PlayerGoldAll -= gold;
        LoadingParameters.Save();
    }
}
