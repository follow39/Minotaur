using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        GetComponent<Light>().range = PlayerPrefs.GetInt("PlayerIllumination");
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
