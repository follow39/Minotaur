using System;
using UnityEngine;
using System.Collections;

public class PlayerTrap : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Math.Abs(GameEngine.MonsterCoordinates.x - gameObject.transform.position.x) < 0.5f &&
            Math.Abs(GameEngine.MonsterCoordinates.z - gameObject.transform.position.z) < 0.5f)
        {
            GameEngine.Stunned();
            Destroy(gameObject);
        }
    }
}
