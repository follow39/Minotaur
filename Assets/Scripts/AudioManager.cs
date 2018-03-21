using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public List<GameObject> TrackList = new List<GameObject>();
    private GameObject currentTrack;
    private float fadeTime = 0.0f;

    private float playTime = 0.0f;

    void Awake()
    {
        foreach (GameObject _gameObject in FindObjectsOfType<GameObject>())
        {
            if (_gameObject.GetComponent<AudioManager>() && _gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        currentTrack = TrackList[Random.Range(0, TrackList.Count)];
        Instantiate(currentTrack);
    }

    // Update is called once per frame
    private void Update()
    {
        playTime += Time.deltaTime;
        if (playTime > currentTrack.GetComponent<AudioSource>().clip.length - fadeTime)
        {
            currentTrack = TrackList[Random.Range(0, TrackList.Count)];
            playTime = 0.0f;
            fadeTime = currentTrack.GetComponent<AudioScript>().fadeTime;
            Instantiate(currentTrack);
        }
    }
}
