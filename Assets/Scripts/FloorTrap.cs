using UnityEngine;
using System.Collections;

public class FloorTrap : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        GameEngine.map[(int) gameObject.transform.position.z, (int) gameObject.transform.position.x] = -3;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.ActivateFloorTrap((sbyte) gameObject.transform.position.z,
            (sbyte) gameObject.transform.position.x) && GameEngine.PlayerHitPoints > 0)
        {
            GameEngine.PlayerHitPoints -= 1;
            GetComponent<Animation>().Play("Up");
        }
    }
}
