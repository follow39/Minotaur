using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetectedEvent : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponentInChildren<RawImage>().transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren<RawImage>().color += new Color(0, 0, 0, -5 * Time.deltaTime);
        gameObject.GetComponentInChildren<RawImage>().transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (gameObject.GetComponentInChildren<RawImage>().color.a < 0.01)
        {
            Destroy(gameObject);
        }

    }
}
