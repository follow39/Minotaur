using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Treasure : MonoBehaviour
{
    public bool used = false;

    // Use this for initialization
    private void Start()
    {
        gameObject.transform.position = new Vector3(GameEngine.Width - 1.5f, 0, GameEngine.Height - 1.5f);
        for (int i = GameEngine.Height/4; i < GameEngine.Height/2 + GameEngine.Height/4; i++)
        {
            for (int j = GameEngine.Width/4; j < GameEngine.Width/2 + GameEngine.Width/4; j++)
            {
                if (GameEngine.map[i, j] > 0 && Random.Range(0, 39) > 23)
                {
                    gameObject.transform.position = new Vector3(j + 0.5f, 0, i + 0.5f);
                }
            }
        }
        //gameObject.transform.position = new Vector3(3.5f, 0, 1.5f);
        GameEngine.map[(int) gameObject.transform.position.z, (int) gameObject.transform.position.x] = 0;
        GameEngine.TreasureCoordinates = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.CollectingGold && !used)
        {
            used = true;
            gameObject.GetComponent<Animation>().CrossFade("box_open");
        }
        if (!GameEngine.CollectingGold && used)
        {
            used = false;
            gameObject.GetComponent<Animation>().CrossFade("box_close");
        }
    }
}