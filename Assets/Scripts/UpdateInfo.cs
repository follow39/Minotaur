using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateInfo : MonoBehaviour
{
    public Text textTime;
    public Text textGold;
    public Text textTimeCount;
    public Text textGoldCount;

    // Use this for initialization
    private void Start()
    {
        textTime.text = LocalizationManager.Localization("Time") + ":";
        textGold.text = LocalizationManager.Localization("Gold") + ":";
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameEngine.GameIsStarted || GameEngine.GameIsPaused || GameEngine.GameIsFinished)
        {
            return;
        }
        GameEngine.GameTime += Time.deltaTime;

        textTimeCount.text = string.Format("{0:F1}", GameEngine.GameTime);
        textGoldCount.text = string.Format("{0}", GameEngine.PlayerGold);
    }
}
