using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SkeletonRunner : MonoBehaviour
{
    private Vector3 skeletonCoordinates = new Vector3();
    private Vector3 targetCoordinates = new Vector3();
    private MoveDirection moveDirection = MoveDirection.None;
    private RotateDirection rotateDirection = RotateDirection.None;
    private float speed = 3.25f;
    private float rotationSpeed = 750;
    public float rotation_y = 0;
    private float neededRotation_y = 0;

    private float timeCount = 0;
    private float timePause = 0;

    private int[,] map;
    private Stack<MoveDirection> stackMoves = new Stack<MoveDirection>();

    // Use this for initialization
    void Start()
    {
        map = new int[GameEngine.Height, GameEngine.Width];
        gameObject.GetComponent<Animation>().Play("Idle");
        rotation_y = gameObject.transform.rotation.y;
        ResetMap();

        skeletonCoordinates = gameObject.transform.position;
        GetComponent<Animation>()["Run"].speed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameEngine.GameIsStarted || GameEngine.GameIsPaused)
        {
            GetComponent<Animation>().Stop();
            return;
        }
        if (stackMoves.Count == 0)
        {
            gameObject.GetComponent<Animation>().CrossFade("Idle");
            if (timePause == 0)
            {
                timePause = Random.Range(1.5f, 4.0f);
            }
            timeCount += Time.deltaTime;

            if (timeCount > timePause)
            {
                ResetMap();
                UpdateMap((int)skeletonCoordinates.z, (int)skeletonCoordinates.x);
                targetCoordinates = new Vector3(Random.Range(3, GameEngine.Width - 3), 0, Random.Range(3, GameEngine.Height - 3));

                if (GameEngine.map[(int)targetCoordinates.z, (int)targetCoordinates.x] != 0)
                {
                    FindPath((int)targetCoordinates.z, (int)targetCoordinates.x);
                }
            }
        }
        else
        {
            timePause = 0;
            timeCount = 0;
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

        if (moveDirection != MoveDirection.None)
        {
            if (rotateDirection != RotateDirection.None)
            {
                if (rotateDirection == RotateDirection.Сlockwise)
                {
                    rotation_y += Time.deltaTime * rotationSpeed % 360;
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
                    rotation_y -= Time.deltaTime * rotationSpeed % 360;
                    if (rotation_y <= neededRotation_y)
                    {
                        rotation_y = neededRotation_y;
                        rotateDirection = RotateDirection.None;
                    }
                    gameObject.transform.rotation = Quaternion.Euler(0, rotation_y, 0);
                    return;
                }
            }
            gameObject.GetComponent<Animation>().Play("Run");
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    if (gameObject.transform.position.x - skeletonCoordinates.x < 1)
                    {
                        gameObject.transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
                    }
                    else
                    {
                        skeletonCoordinates = skeletonCoordinates + new Vector3(1, 0, 0);
                        gameObject.transform.position = skeletonCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Right:
                    if (skeletonCoordinates.x - gameObject.transform.position.x < 1)
                    {
                        gameObject.transform.position += new Vector3((-1) * Time.deltaTime * speed, 0, 0);
                    }
                    else
                    {
                        skeletonCoordinates = skeletonCoordinates + new Vector3(-1, 0, 0);
                        gameObject.transform.position = skeletonCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Down:
                    if (gameObject.transform.position.z - skeletonCoordinates.z < 1)
                    {
                        gameObject.transform.position += new Vector3(0, 0, Time.deltaTime * speed);
                    }
                    else
                    {
                        skeletonCoordinates = skeletonCoordinates + new Vector3(0, 0, 1);
                        gameObject.transform.position = skeletonCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Up:
                    if (skeletonCoordinates.z - gameObject.transform.position.z < 1)
                    {
                        gameObject.transform.position += new Vector3(0, 0, (-1) * Time.deltaTime * speed);
                    }
                    else
                    {
                        skeletonCoordinates = skeletonCoordinates + new Vector3(0, 0, -1);
                        gameObject.transform.position = skeletonCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;
            }
            return;
        }        
    }


    public void MoveRight()
    {
        if (GameEngine.map[(int)skeletonCoordinates.z, (int)skeletonCoordinates.x - 1] != 0)
        {
            moveDirection = MoveDirection.Right;
            neededRotation_y = 270;
        }
        SetRotationDirection();
    }

    public void MoveLeft()
    {
        if (GameEngine.map[(int)skeletonCoordinates.z, (int)skeletonCoordinates.x + 1] != 0)
        {
            moveDirection = MoveDirection.Left;
            neededRotation_y = 90;
        }
        SetRotationDirection();
    }

    public void MoveUp()
    {
        if (GameEngine.map[((int)skeletonCoordinates.z - 1), (int)skeletonCoordinates.x] != 0)
        {
            moveDirection = MoveDirection.Up;
            neededRotation_y = 180;
        }
        SetRotationDirection();
    }

    public void MoveDown()
    {
        if (GameEngine.map[((int)skeletonCoordinates.z + 1), (int)skeletonCoordinates.x] != 0)
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
                if (GameEngine.map[i, j] == 1)
                {
                    map[i, j] = -1;
                }
                if (i == (int)skeletonCoordinates.z && j == (int)skeletonCoordinates.x)
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
