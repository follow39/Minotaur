using UnityEngine;
using System.Collections;

public class WallTrap : MonoBehaviour
{
    private bool activate = false;
    private bool reuse = false;
    public GameObject wall;

    // Use this for initialization
    private void Start()
    {
        reuse = Random.Range(0, 2) == 0;
        GameEngine.map[(int) gameObject.transform.position.z, (int) gameObject.transform.position.x] = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!reuse && wall.transform.position.y == 0) return;
        if (!activate || reuse)
        {
            activate = GameEngine.ActivateWallTrap((sbyte)gameObject.transform.position.z,
                (sbyte)gameObject.transform.position.x);
        }
        if (activate)
        {
            if (wall.transform.position.y < 0)
            {
                wall.transform.position += new Vector3(0, 1.5f*Time.deltaTime, 0);
            }
            if (wall.transform.position.y > 0)
            {
                wall.transform.position = new Vector3(wall.transform.position.x, 0, wall.transform.position.z);
            }
        }
        else
        {
            if (wall.transform.position.y > -1.2751f)
            {
                wall.transform.position -= new Vector3(0, 1.5f*Time.deltaTime, 0);
            }
            if (wall.transform.position.y < -1.2751f)
            {
                wall.transform.position = new Vector3(wall.transform.position.x, -1.2751f, wall.transform.position.z);
            }
        }
    }
}