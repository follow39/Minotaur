using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private float loadingTime = 0.0f;
    private bool tap = false;

    public Text tapText;
    public Text missionText;
    public GameObject missionPanel;

    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        tapText.text = LocalizationManager.Localization("Tap");
        missionText.text = LocalizationManager.Localization("Mission");
    }

    // Update is called once per frame
    private void Update()
    {
        loadingTime += Time.deltaTime;
        if (GameEngine.CreateMap || Application.loadedLevel == 0)
        {
            if (Input.touchCount>0 || Input.anyKeyDown)
            {
                tap = true;
            }
            if ((loadingTime > 0.5f && tap) || Application.loadedLevel == 0)
            {
                gameObject.GetComponentInChildren<RawImage>().color += new Color(0, 0, 0, -1*Time.deltaTime);

                tapText.color += new Color(0, 0, 0, -5 * Time.deltaTime);
                missionText.color += new Color(0, 0, 0, -2 * Time.deltaTime);
                missionPanel.GetComponent<Image>().color += new Color(0, 0, 0, -2 * Time.deltaTime);

                if (gameObject.GetComponentInChildren<RawImage>().color.a < 0.01)
                {
                    GameEngine.GameIsPaused = false;
                    Destroy(gameObject);
                }
                return;
            }
            tapText.color += new Color(0, 0, 0, 1 * Time.deltaTime);
        }
    }
}
