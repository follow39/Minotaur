using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldPanelScript : MonoBehaviour
{
    public Text text;
    public Button buttonBuy;
    public int gold;
    public string sku;
    public int price;
    public GameObject failPanel;

    // Use this for initialization
    private void Start()
    {
        buttonBuy.onClick.AddListener(BuyGold);

        text.text = string.Format(gold + " "    + LocalizationManager.Localization(gameObject.name));
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void BuyGold()
    {
        if (price == 4)
        {
            GameEngine.PlayerGoldAll += gold;
            LoadingParameters.Save();
            
        }
    }
}
