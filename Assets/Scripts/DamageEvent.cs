using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageEvent : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren<RawImage>().color += new Color(0, 0, 0, -10 * Time.deltaTime);
        if (gameObject.GetComponentInChildren<RawImage>().color.a < 0.01)
        {
            Destroy(gameObject);
        }
    }
}
