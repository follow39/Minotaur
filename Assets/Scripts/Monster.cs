using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    private Vector3 monsterCoordinates = new Vector3();
    public Vector3 playerCoordinates = new Vector3();
    private MoveDirection moveDirection = MoveDirection.None;
    private RotateDirection rotateDirection = RotateDirection.None;
    private float speed = 1.4f;
    private float rotationSpeed = 550;
    public float rotation_y = 0;
    private float neededRotation_y = 0;
    private bool isAttack = false;

    private int[,] map;
    private Stack<MoveDirection> stackMoves = new Stack<MoveDirection>();

    public static Action DamageDeal;

    // Use this for initialization
    private void Start()
    {
        bool treasurePath = false;
        bool playerPath = false;
        GameEngine.CreateMap = false;

        rotation_y = gameObject.transform.rotation.y;

        monsterCoordinates = gameObject.transform.position;
        map = new int[GameEngine.Height, GameEngine.Width];
        playerCoordinates = GameEngine.GetPlayerCoordinates((int)monsterCoordinates.z, (int)monsterCoordinates.x);
        ResetMap();
        UpdateMap((int) monsterCoordinates.z, (int) monsterCoordinates.x);

        FindPath((int) GameEngine.TreasureCoordinates.z + 1, (int) GameEngine.TreasureCoordinates.x);
        FindPath((int) GameEngine.TreasureCoordinates.z - 1, (int) GameEngine.TreasureCoordinates.x);
        FindPath((int) GameEngine.TreasureCoordinates.z, (int) GameEngine.TreasureCoordinates.x + 1);
        FindPath((int) GameEngine.TreasureCoordinates.z, (int) GameEngine.TreasureCoordinates.x - 1);
        if (stackMoves.Count == 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            treasurePath = true;
        }
        stackMoves.Clear();

        FindPath((int) playerCoordinates.z, (int) playerCoordinates.x);
        if (stackMoves.Count == 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
            playerPath = true;
        }
        stackMoves.Clear();

        GetComponent<Animation>()["attack01"].speed = 2.0f;
        GetComponent<Animation>()["walk"].speed = 1.75f;

        if (playerPath && treasurePath)
        {
            GameEngine.CreateMap = true;
        }


        if (GameEngine.GameDifficultyLevel == DifficultyLevel.Easy)
        {
            speed = 1.25f;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameEngine.GameIsStarted || GameEngine.GameIsPaused)
        {
            GetComponent<Animation>().Stop();
            return;
        }
        if (GameEngine.IsStuned())
        {
            GetComponent<Animation>().Play("cry");
            return;
        }

        GameEngine.MonsterCoordinates = gameObject.transform.position;
        playerCoordinates = GameEngine.GetPlayerCoordinates((int)monsterCoordinates.z, (int)monsterCoordinates.x);

        if (GetComponent<Animation>().IsPlaying("attack01"))
        {
            isAttack = true;
            if ((Math.Abs(gameObject.transform.position.z - playerCoordinates.z) < 1.25f &&
                 (int)gameObject.transform.position.x == (int)playerCoordinates.x) ||
                (Math.Abs(monsterCoordinates.x - playerCoordinates.x) < 1.25f &&
                 (int)gameObject.transform.position.z == (int)playerCoordinates.z))
            {
                return;
            }
            else
            {
                isAttack = false;
                return;
            }
        }
        if (isAttack)
        {
            if(DamageDeal!=null)
            {
                DamageDeal();
            }
            isAttack = false;
            GameEngine.PlayerHitPoints--;
        }
        if ((Math.Abs(gameObject.transform.position.z - playerCoordinates.z) < 0.25f && (int)gameObject.transform.position.x == (int)playerCoordinates.x) ||
             (Math.Abs(gameObject.transform.position.x - playerCoordinates.x) < 0.25f && (int)gameObject.transform.position.z == (int)playerCoordinates.z))
        {
            if (GameEngine.PlayerHitPoints > 0)
            {
                GetComponent<Animation>().CrossFade("attack01");
            }
            else
            {
                GetComponent<Animation>().Play("idle");
                return;
            }
            return;
        }

        if (moveDirection != MoveDirection.None)
        {
            if (rotateDirection != RotateDirection.None)
            {
                if (rotateDirection == RotateDirection.Сlockwise)
                {
                    rotation_y += Time.deltaTime*rotationSpeed%360;
                    if (rotation_y >= neededRotation_y)
                    {
                        rotation_y = neededRotation_y;
                        rotateDirection = RotateDirection.None;
                    }
                    gameObject.transform.rotation = Quaternion.Euler(0, rotation_y, 0);
                    return;
                }

                if (rotateDirection == RotateDirection.Сounterclockwise)
                {
                    rotation_y -= Time.deltaTime*rotationSpeed%360;
                    if (rotation_y <= neededRotation_y)
                    {
                        rotation_y = neededRotation_y;
                        rotateDirection = RotateDirection.None;
                    }
                    gameObject.transform.rotation = Quaternion.Euler(0, rotation_y, 0);
                    return;
                }
            }
            gameObject.GetComponent<Animation>().Play("walk");
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    if (gameObject.transform.position.x - monsterCoordinates.x < 1)
                    {
                        gameObject.transform.position += new Vector3(Time.deltaTime*speed, 0, 0);
                    }
                    else
                    {
                        monsterCoordinates = monsterCoordinates + new Vector3(1, 0, 0);
                        gameObject.transform.position = monsterCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Right:
                    if (monsterCoordinates.x - gameObject.transform.position.x < 1)
                    {
                        gameObject.transform.position += new Vector3((-1)*Time.deltaTime*speed, 0, 0);
                    }
                    else
                    {
                        monsterCoordinates = monsterCoordinates + new Vector3(-1, 0, 0);
                        gameObject.transform.position = monsterCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Down:
                    if (gameObject.transform.position.z - monsterCoordinates.z < 1)
                    {
                        gameObject.transform.position += new Vector3(0, 0, Time.deltaTime*speed);
                    }
                    else
                    {
                        monsterCoordinates = monsterCoordinates + new Vector3(0, 0, 1);
                        gameObject.transform.position = monsterCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Up:
                    if (monsterCoordinates.z - gameObject.transform.position.z < 1)
                    {
                        gameObject.transform.position += new Vector3(0, 0, (-1)*Time.deltaTime*speed);
                    }
                    else
                    {
                        monsterCoordinates = monsterCoordinates + new Vector3(0, 0, -1);
                        gameObject.transform.position = monsterCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;
            }
            return;
        }

        if (playerCoordinates != new Vector3())
        {
            ResetMap();
            UpdateMap((int)monsterCoordinates.z, (int)monsterCoordinates.x);
            FindPath((int) playerCoordinates.z, (int) playerCoordinates.x);
        }

        if (stackMoves.Count == 0)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
            }
        }
        else
        {
            switch (stackMoves.Pop())
            {
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
                case MoveDirection.Left:
                    MoveLeft();
                    break;
            }
        }

        SetRotationDirection();
    }

    public void MoveRight()
    {
        if (GameEngine.map[(int) monsterCoordinates.z, (int) monsterCoordinates.x - 1] != 0)
        {
            moveDirection = MoveDirection.Right;
            neededRotation_y = 270;
        }
        SetRotationDirection();
    }

    public void MoveLeft()
    {
        if (GameEngine.map[(int) monsterCoordinates.z, (int) monsterCoordinates.x + 1] != 0)
        {
            moveDirection = MoveDirection.Left;
            neededRotation_y = 90;
        }
        SetRotationDirection();
    }

    public void MoveUp()
    {
        if (GameEngine.map[((int) monsterCoordinates.z - 1), (int) monsterCoordinates.x] != 0)
        {
            moveDirection = MoveDirection.Up;
            neededRotation_y = 180;
        }
        SetRotationDirection();
    }

    public void MoveDown()
    {
        if (GameEngine.map[((int) monsterCoordinates.z + 1), (int) monsterCoordinates.x] != 0)
        {
            moveDirection = MoveDirection.Down;
            if (rotation_y > 180)
            {
                neededRotation_y = 360;
                if (rotation_y == 0)
                {
                    rotation_y = 360;
                }
            }
            else
            {
                neededRotation_y = 0;
            }
        }
        SetRotationDirection();
    }

    public void SetRotationDirection()
    {
        if (rotation_y == 0 && neededRotation_y > 180)
        {
            rotation_y = 360;
        }
        if (rotation_y == 360 && neededRotation_y < 180)
        {
            rotation_y = 0;
        }
        if (neededRotation_y > rotation_y)
        {
            rotateDirection = RotateDirection.Сlockwise;
        }
        if (neededRotation_y < rotation_y)
        {
            rotateDirection = RotateDirection.Сounterclockwise;
            ;
        }
        if (neededRotation_y == rotation_y)
        {
            rotateDirection = RotateDirection.None;
        }
    }

    public void UpdateMap(int i, int j)
    {
        if (i + 1 < GameEngine.Height && (map[i + 1, j] == -1 || map[i + 1, j] > map[i, j] + 1))
        {
            map[i + 1, j] = map[i, j] + 1;
            UpdateMap(i + 1, j);
        }
        if (j + 1 < GameEngine.Width && (map[i, j + 1] == -1 || map[i, j + 1] > map[i, j] + 1))
        {
            map[i, j + 1] = map[i, j] + 1;
            UpdateMap(i, j + 1);
        }
        if (i - 1 > 0 && (map[i - 1, j] == -1 || map[i - 1, j] > map[i, j] + 1))
        {
            map[i - 1, j] = map[i, j] + 1;
            UpdateMap(i - 1, j);
        }
        if (j - 1 > 0 && (map[i, j - 1] == -1 || map[i, j - 1] > map[i, j] + 1))
        {
            map[i, j - 1] = map[i, j] + 1;
            UpdateMap(i, j - 1);
        }
    }

    public void ResetMap()
    {
        for (int i = 0; i < GameEngine.Height; i++)
        {
            for (int j = 0; j < GameEngine.Width; j++)
            {
                if (GameEngine.map[i, j] <= 0)
                {
                    map[i, j] = -2;
                }
                if (GameEngine.map[i, j] == 1 )
                {
                    map[i, j] = -1;
                }
                if (i == (int) monsterCoordinates.z && j == (int) monsterCoordinates.x)
                {
                    map[i, j] = 0;
                }
            }
        }
    }

    public void FindPath(int i, int j)
    {
        stackMoves.Clear();
        while (map[i, j] > 0)
        {
            if (i + 1 < GameEngine.Height && map[i + 1, j] == map[i, j] - 1)
            {
                stackMoves.Push(MoveDirection.Up);
                i++;
            }
            if (j + 1 < GameEngine.Width && map[i, j + 1] == map[i, j] - 1)
            {
                stackMoves.Push(MoveDirection.Right);
                j++;
            }
            if (i - 1 > 0 && map[i - 1, j] == map[i, j] - 1)
            {
                stackMoves.Push(MoveDirection.Down);
                i--;
            }
            if (j - 1 > 0 && map[i, j - 1] == map[i, j] - 1)
            {
                stackMoves.Push(MoveDirection.Left);
                j--;
            }
        }
    }
}