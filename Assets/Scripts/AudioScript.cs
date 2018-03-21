using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour
{
    public float fadeTime = 5;
    private float volume = 0.0f;
    private float start = 0.0f;
    private float end = 1.0f;
    public FadeType fadeType = FadeType.In;


    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        fadeType = FadeType.In;
        GetComponent<AudioSource>().volume = volume;
    }

    // Update is called once per frame
    private void Update()
    {
        GetComponent<AudioSource>().mute = GameEngine.MuteAudio;

        if (fadeType == FadeType.In)
        {
            volume += (1.0f/fadeTime)*Time.deltaTime;
            GetComponent<AudioSource>().volume = Mathf.Lerp(start, end, volume);
        }
        if (fadeType == FadeType.Out)
        {
            if (start == 0.0f)
            {
                volume = 1 - volume;
                start = 1.0f;
                end = 0.0f;
            }
            volume += (1.0f/fadeTime)*Time.deltaTime;
            GetComponent<AudioSource>().volume = Mathf.Lerp(start, end, volume);
            if (volume > 0.9f)
            {
                Destroy(gameObject);
            }
        }
        if (GetComponent<AudioSource>().time > GetComponent<AudioSource>().clip.length - fadeTime)
        {
            fadeType = FadeType.Out;
        }
    }
}

public enum FadeType
{
    In,
    Out
}
