using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Vector3 playerCoordinates = new Vector3();
    private MoveDirection moveDirection = MoveDirection.None;
    private RotateDirection rotateDirection = RotateDirection.None;
    public float walkAnimationSpeed = 1.0f;
    public float runAnimationSpeed = 1.0f;
    private float speed = 1.2f;
    private float speedup = 1.25f;
    private float rotationSpeed = 500;
    private float rotation_y = 0;
    private float neededRotation_y = 0;
    private bool running = false;
    private float maxEnergy = 0;
    private float energy = 0;
    private bool die = false;
    private float stopTime = 0.0f;

    // Use this for initialization
    private void Start()
    {
        gameObject.transform.position = new Vector3(1.5f, 0, 1.75f);
        playerCoordinates = gameObject.transform.position;
        rotation_y = gameObject.transform.rotation.y;
        GameManager.RunningEvent += ChangeRun;

        GetComponent<Animation>()["walk"].speed = walkAnimationSpeed;
        GetComponent<Animation>()["charge"].speed = runAnimationSpeed;

        GameEngine.PlayerHitPoints = PlayerPrefs.GetInt("PlayerHitPoints");
        maxEnergy = PlayerPrefs.GetInt("PlayerEnergy");
        energy = 0.15f * maxEnergy;
        GameEngine.PlayerGoldMaxInGame = PlayerPrefs.GetInt("PlayerGoldMaxInGame");


        energy = 0.15f * maxEnergy;
        ItemsManager.EnergyEvent += AddEnergy;
    }


    // Update is called once per frame
    private void Update()
    {
        if (GameEngine.PlayerHitPoints < 1)
        {
            if (!die)
            {
                GetComponent<Animation>().CrossFade("die");
                die = true;
            }
            return;
        }
        if (GameEngine.GameIsPaused)
        {
            GetComponent<Animation>().Stop();
            return;
        }
        if (GameEngine.GameIsFinished)
        {
            ItemsManager.EnergyEvent -= AddEnergy;
            GetComponent<Animation>().Stop();
            return;
        }
        if (!running)
        {
            if (energy < maxEnergy)
            {
                energy += Time.deltaTime*speed;
            }
            else
            {
                energy = maxEnergy;
            }
            if (moveDirection != MoveDirection.None && !GetComponent<Animation>().IsPlaying("walk"))
            {
                gameObject.GetComponent<Animation>().CrossFade("walk");
            }
        }
        if (energy < 1 && running)
        {
            ChangeRun();
            if (GetComponent<Animation>().IsPlaying("charge"))
            {
                gameObject.GetComponent<Animation>().CrossFade("walk");
            }
        }
        GameEngine.PlayerCoordinates = gameObject.transform.position;
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x + 1.5f, 5,
            gameObject.transform.position.z + 3);

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
            }
        }

        if (moveDirection != MoveDirection.None)
        {
            stopTime = 0.0f;
            if (!GetComponent<Animation>().IsPlaying("walk") && !GetComponent<Animation>().IsPlaying("charge"))
            {
                gameObject.GetComponent<Animation>().CrossFade("walk");

            }
            if (running && energy > 0)
            {
                energy -= 2*Time.deltaTime*speed;
                if (!GetComponent<Animation>().IsPlaying("charge"))
                {
                    gameObject.GetComponent<Animation>().Play("charge");
                }
            }

            switch (moveDirection)
            {
                case MoveDirection.Left:
                    gameObject.transform.position += new Vector3(Time.deltaTime*speed, 0, 0);
                    if (gameObject.transform.position.x - playerCoordinates.x > 0.99f)
                    {
                        playerCoordinates = playerCoordinates + new Vector3(1, 0, 0);
                        //gameObject.transform.position = playerCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Right:
                    gameObject.transform.position += new Vector3((-1)*Time.deltaTime*speed, 0, 0);
                    if (playerCoordinates.x - gameObject.transform.position.x > 0.99f)
                    {
                        playerCoordinates = playerCoordinates + new Vector3(-1, 0, 0);
                        //gameObject.transform.position = playerCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Down:
                    gameObject.transform.position += new Vector3(0, 0, Time.deltaTime*speed);
                    if (gameObject.transform.position.z - playerCoordinates.z > 0.99f)
                    {
                        playerCoordinates = playerCoordinates + new Vector3(0, 0, 1);
                        //gameObject.transform.position = playerCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;

                case MoveDirection.Up:
                    gameObject.transform.position += new Vector3(0, 0, (-1)*Time.deltaTime*speed);
                    if (playerCoordinates.z - gameObject.transform.position.z > 0.99f)
                    {
                        playerCoordinates = playerCoordinates + new Vector3(0, 0, -1);
                        //gameObject.transform.position = playerCoordinates;
                        moveDirection = MoveDirection.None;
                    }
                    break;
            }
        }

        if (energy < maxEnergy && moveDirection == MoveDirection.None)
        {
            energy += Time.deltaTime*speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (moveDirection == MoveDirection.None && !GetComponent<Animation>().IsPlaying("idlebattle"))
        {
            if (stopTime < 2*Time.deltaTime)
            {
                stopTime += Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<Animation>().CrossFade("idlebattle");
                stopTime = 0.0f;
            }
        }
        if (Input.GetKey(KeyCode.RightControl))
        {
            CollectGold();
            return;
        }

        SetRotationDirection();
    }

    public void CollectGold()
    {
        if (moveDirection == MoveDirection.None)
        {
            GameEngine.CollectGold((sbyte) playerCoordinates.z, (sbyte) playerCoordinates.x);
        }
    }

    public void ChangeRun()
    {
        if (running)
        {
            speed = speed - speedup;
        }
        else
        {
            if (energy < 5)
            {
                return;
            }
            speed = speed + speedup;
        }
        running = !running;
    }

    public void MoveRight()
    {
        if (moveDirection != MoveDirection.None)
        {
            return;
        }
        if (GameEngine.map[(int) playerCoordinates.z, (int) playerCoordinates.x - 1] != 0)
        {
            moveDirection = MoveDirection.Right;
        }
        neededRotation_y = 270;
        GameEngine.GameIsStarted = true;
        SetRotationDirection();
    }

    public void MoveLeft()
    {
        if (moveDirection != MoveDirection.None)
        {
            return;
        }
        if (GameEngine.map[(int) playerCoordinates.z, (int) playerCoordinates.x + 1] != 0)
        {
            moveDirection = MoveDirection.Left;
        }
        neededRotation_y = 90;
        GameEngine.GameIsStarted = true;
        SetRotationDirection();
    }

    public void MoveUp()
    {
        if (moveDirection != MoveDirection.None)
        {
            return;
        }
        if (GameEngine.map[((int) playerCoordinates.z - 1), (int) playerCoordinates.x] != 0)
        {
            moveDirection = MoveDirection.Up;
        }
        neededRotation_y = 180;
        GameEngine.GameIsStarted = true;
        SetRotationDirection();
    }

    public void MoveDown()
    {
        if (moveDirection != MoveDirection.None)
        {
            return;
        }
        if (GameEngine.map[((int) playerCoordinates.z + 1), (int) playerCoordinates.x] != 0)
        {
            moveDirection = MoveDirection.Down;
        }
        if (rotation_y > 180)
        {
            neededRotation_y = 360;
        }
        else
        {
            neededRotation_y = 0;
        }
        GameEngine.GameIsStarted = true;
        SetRotationDirection();
    }

    public void SetRotationDirection()
    {
        if (rotation_y < 5 || rotation_y > 355)
        {
            if (neededRotation_y > 180)
            {
                rotation_y = 360;
            }
            else
            {
                rotation_y = 0;
            }
        }
        if (neededRotation_y > rotation_y)
        {
            rotateDirection = RotateDirection.Сlockwise;
        }
        if (neededRotation_y < rotation_y)
        {
            rotateDirection = RotateDirection.Сounterclockwise;
        }
        if (Math.Abs(rotation_y - neededRotation_y) < 5)
        {
            rotateDirection = RotateDirection.None;
        }
    }

    public void AddEnergy(int _energy)
    {
        energy += _energy;
    }
}
