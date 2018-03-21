using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{
    public Sprite sound;
    public Sprite soundMute;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.MuteAudio)
        {
            GetComponent<Image>().sprite = soundMute;
        }
        else
        {
            GetComponent<Image>().sprite = sound;
        }
    }

    public void ChangeMute()
    {
        GameEngine.MuteAudio = !GameEngine.MuteAudio;   
    }
}
