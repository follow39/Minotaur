using UnityEngine;
using System.Collections;

public class PathItemScript : MonoBehaviour
{
    public GameObject path;
    private float time = 0.0f;
    private float delay = 0.5f;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.GameIsPaused || !GameEngine.GameIsStarted)
        {
            return;
        }
        time += Time.deltaTime;
        if (time%delay < Time.deltaTime)
        {
            path.transform.position = GameEngine.PlayerCoordinates +
                                      new Vector3(Random.Range(-0.35f, 0.35f), 0.001f, Random.Range(-0.35f, 0.35f));
            Instantiate(path);
        }
        if (time > 25.0f)
        {
            Destroy(gameObject);
        }
    }
}
