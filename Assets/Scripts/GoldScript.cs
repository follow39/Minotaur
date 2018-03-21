using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    public Text text;
    public Text textGold;

    // Use this for initialization
    private void Start()
    {
        text.text = LocalizationManager.Localization("Gold");
    }

    // Update is called once per frame
    private void Update()
    {
        textGold.text = GameEngine.PlayerGoldAll.ToString();
    }
}
