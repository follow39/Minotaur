using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    public GameObject floor;
    public GameObject player;
    public GameObject monster;
    public GameObject treasure;
    public GameObject skeletonRunner;

    private GameObject _gameObject;
    private int height = 0;
    private int width = 0;
    private sbyte[,] Array;
    private int minCount = 0;
    private int count = 0;
    private int floorTrapCount = 0;
    private int wallTrapCount = 0;
    private float timer;


    public GameObject exit;
    public GameObject wall0;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public GameObject wall5;
    public GameObject wall6;
    


    public GameObject pillar1;
    public GameObject pillar2;
    public GameObject pillar3;
    public GameObject pillar4;

    public GameObject trapWall1;

    public GameObject trapFloor1;


    // Use this for initialization
    private void Start()
    {
        int maxCountSkeletons = 0;

        GameEngine.GameIsFinished = false;
        GameEngine.GameIsPaused = false;
        GameEngine.GameIsStarted = false;
        GameEngine.PlayerGold = 0;
        GameEngine.GameTime = 0;
        switch (GameEngine.GameDifficultyLevel)
        {
            case DifficultyLevel.Easy:
                height = Random.Range(23, 39);
                height = height % 2 == 0 ? height + 1 : height;
                width = Random.Range(23, 39);
                width = width % 2 == 0 ? width + 1 : width;
                minCount = 3;
                floorTrapCount = height / 4;
                wallTrapCount = width / 3;
                maxCountSkeletons = 1;
                break;

            case DifficultyLevel.Normal:
                height = Random.Range(38, 50);
                height = height % 2 == 0 ? height + 1 : height;
                width = Random.Range(38, 45);
                width = width % 2 == 0 ? width + 1 : width;
                minCount = 3;
                floorTrapCount = height * 2 / 3;
                wallTrapCount = width / 2;
                maxCountSkeletons = 2;
                break;

            case DifficultyLevel.Hard:
                height = Random.Range(51, 71);
                height = height % 2 == 0 ? height + 1 : height;
                width = Random.Range(44, 61);
                width = width % 2 == 0 ? width + 1 : width;
                minCount = 3;
                floorTrapCount = height / 2;
                wallTrapCount = width;
                maxCountSkeletons = 5;
                break;

            case DifficultyLevel.Hardcore:
                height = Random.Range(71, 99);
                height = height % 2 == 0 ? height + 1 : height;
                width = Random.Range(60, 80);
                width = width % 2 == 0 ? width + 1 : width;
                minCount = 3;
                floorTrapCount = height;
                wallTrapCount = width * 2;
                maxCountSkeletons = 3;
                break;
        }
        Array = GenerateMap();
        GameEngine.Height = height;
        GameEngine.Width = width;
        GameEngine.map = Array;
        Mapping();
        GameEngine.map = Array;
        Instantiate(player);
        Instantiate(treasure);
        GameEngine.PlayerGoldMaxInGame = 500;
        GameEngine.PlayerGold = 0;
        _gameObject = monster;
        _gameObject.transform.position = new Vector3(GameEngine.Width - 1.5f, 0, GameEngine.Height - 1.5f);
        Instantiate(_gameObject);
        _gameObject = monster;

        int countSkeletons = 0;
        while (countSkeletons < maxCountSkeletons)
        {
            _gameObject = skeletonRunner;
            _gameObject.transform.position = new Vector3(Random.Range(3, GameEngine.Width - 3) + 0.5f, 0.4f, Random.Range(3, GameEngine.Height - 3) + 0.5f);
            if (GameEngine.map[(int)_gameObject.transform.position.z, (int)_gameObject.transform.position.x] == 1)
            {
                Instantiate(_gameObject);
                countSkeletons++;
            }
        }

        Destroy(gameObject);
    }

    public void Mapping()
    {
        //генерация краёв карты
        for (int i = 1; i < height-1; i++)
        {
            _gameObject = wall0;
            if (Array[i,1]==0)
            {
                _gameObject = wall1;
            }
            if (i == 1)
            {
                _gameObject = exit;
            }
            _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            _gameObject.transform.position = new Vector3(0.5f, 0, /*height-1 -*/ i + 0.5f);
            Instantiate(_gameObject);

            _gameObject = wall0;
            if (Array[i, width-2] == 0)
            {
                _gameObject = wall1;
            }
            _gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
            _gameObject.transform.position = new Vector3(width - 1 + 0.5f, 0, /*height-1 -*/ i + 0.5f);
            Instantiate(_gameObject);
            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        for (int j = 1; j < width - 1; j++)
        {
            _gameObject = wall0;
            if (Array[1, j] == 0)
            {
                _gameObject = wall1;
            }
            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ 0 + 0.5f);
            Instantiate(_gameObject);

            if (Array[height-2, j] == 0)
            {
                _gameObject = wall1;
            }
            _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ height - 1 + 0.5f);
            Instantiate(_gameObject);
            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }



        //генерация ловушек
        bool sectorClear = true;
        for (int k = 0; k < floorTrapCount;)
        {
            for (int i = 5; i < height - 5; i++)
            {
                for (int j = 5; j < width - 5; j++)
                {
                    for (int l = i - 3; l < i + 3; l++)
                    {
                        for (int m = j - 3; m < j + 3; m++)
                        {
                            if (Array[l, m] == -3)
                            {
                                sectorClear = false;
                            }
                        }
                    }
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] == 0 && Array[i, j + 1] > 0 && Array[i, j - 1] > 0 && Array[i, j] != -3 && sectorClear)
                    {
                        if (Random.Range(0, 25) > 22)
                        {
                            _gameObject = trapFloor1;
                            Array[i, j] = -3;
                            k++;
                            _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                            _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ i + 0.5f);
                            Instantiate(_gameObject);
                            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    if (Array[i + 1, j] > 0 && Array[i - 1, j] > 0 && Array[i, j + 1] == 0 && Array[i, j - 1] == 0 && Array[i, j] != -3 && sectorClear)
                    {
                        if (Random.Range(0, 25) > 22)
                        {
                            _gameObject = trapFloor1;
                            Array[i, j] = -3;
                            k++;
                            _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                            _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ i + 0.5f);
                            Instantiate(_gameObject);
                            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    if (k > floorTrapCount-1)
                    {
                        break;
                    }
                    sectorClear = true;
                }
                if (k > floorTrapCount-1)
                {
                    break;
                }
            }
        } 
        for (int k = 0; k < wallTrapCount; )
        {
            for (int i = 5; i < height - 3; i++)
            {
                for (int j = 5; j < width - 3; j++)
                {
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] == 0 && Array[i, j + 1] > 0 && Array[i, j - 1] > 0 && Array[i,j] != -3)
                    {
                        if (Random.Range(0, 25) > 22)
                        {
                            _gameObject = trapWall1;
                            Array[i, j] = -3;
                            k++;
                            _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                            _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ i + 0.5f);
                            Instantiate(_gameObject);
                            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    if (Array[i + 1, j] > 0 && Array[i - 1, j] > 0 && Array[i, j + 1] == 0 && Array[i, j - 1] == 0 && Array[i, j] != -3)
                    {
                        if (Random.Range(0, 25) > 22)
                        {
                            _gameObject = trapWall1;
                            Array[i, j] = -3;
                            k++;
                            _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                            _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ i + 0.5f);
                            Instantiate(_gameObject);
                            _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    if (k > wallTrapCount-1)
                    {
                        break;
                    }
                }
                if (k > wallTrapCount-1)
                {
                    break;
                }
            }
        }



        //генерация стен
        for (int i = 1; i < height - 1; i++)
        {
            for (int j = 1; j < width - 1; j++)
            {
                if (Array[i, j] == 0)
                {
                    count = 0;
                    _gameObject = pillar1;
                    for (int k = i - 1; k < i + 2; k++)
                    {
                        for (int l = j - 1; l < j + 2; l++)
                        {
                            if (Array[k, l] == 0)
                            {
                                count++;
                            }
                        }
                    }
                    if (count == 1)
                    {
                        switch (Random.Range(0, 15))
                        {
                            case 0:
                                _gameObject = pillar2;
                                break;
                            case 1:
                                _gameObject = pillar3;
                                break;
                            default:
                                if (Random.Range(0,11)>3)
                                {
                                    _gameObject = pillar1;
                                }
                                else
                                {
                                    _gameObject = pillar4;
                                }
                                break;
                        }
                        _gameObject.transform.rotation = Quaternion.Euler(0, 90*Random.Range(0,4), 0);
                    }

                    if (Array[i + 1, j] == 0 && Array[i - 1, j] == 0 && Array[i, j + 1] != 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall2;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    if (Array[i + 1, j] != 0 && Array[i - 1, j] != 0 && Array[i, j + 1] == 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall2;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }



                    if (Array[i + 1, j] == 0 && Array[i - 1, j] != 0 && Array[i, j + 1] != 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall6;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (Array[i + 1, j] != 0 && Array[i - 1, j] == 0 && Array[i, j + 1] != 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall6;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (Array[i + 1, j] != 0 && Array[i - 1, j] != 0 && Array[i, j + 1] == 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall6;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    if (Array[i + 1, j] != 0 && Array[i - 1, j] != 0 && Array[i, j + 1] != 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall6;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                    }



                    if (Array[i + 1, j] != 0 && Array[i - 1, j] == 0 && Array[i, j + 1] == 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall5;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] != 0 && Array[i, j + 1] == 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall5;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                    }
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] != 0 && Array[i, j + 1] != 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall5;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (Array[i + 1, j] != 0 && Array[i - 1, j] == 0 && Array[i, j + 1] != 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall5;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }



                    if (Array[i + 1, j] != 0 && Array[i - 1, j] == 0 && Array[i, j + 1] == 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall3;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] != 0 && Array[i, j + 1] == 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall3;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] == 0 && Array[i, j + 1] != 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall3;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                    }
                    if (Array[i + 1, j] == 0 && Array[i - 1, j] == 0 && Array[i, j + 1] == 0 && Array[i, j - 1] != 0)
                    {
                        _gameObject = wall3;
                        _gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }



                    if (Array[i + 1, j] == 0 && Array[i - 1, j] == 0 && Array[i, j + 1] == 0 && Array[i, j - 1] == 0)
                    {
                        _gameObject = wall4;
                    }
                }
                else
                {
                    _gameObject = floor;
                }
                if (Array[i, j] != -3)
                {
                    _gameObject.transform.position = new Vector3(j + 0.5f, 0, /*height-1 -*/ i + 0.5f);
                    Instantiate(_gameObject);
                    _gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    //strings[i] += Array[i, j].ToString();
                }
            }
            //Debug.Log(strings[i]);
        }
    }

    public void Update()
    {
    }



    public sbyte[,] GenerateMap()
    {
        sbyte[,] array = new sbyte[height, width];
        bool haveExit = false;
        int count = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                array[i, j] = 0;
            }
        }
        for (int i = 1; i < height - 1; i += 2)
        {
            count = 0;
            for (int j = 1; j < width - 1; j++)
            {
                if (Random.Range(0, 2) == 0 && array[i - 1, j] == 0 && count > minCount && (array[i, j] == array[i, j + 1]))
                {
                    if (array[i, j - 1] == 0 || j + 2 >= width)
                    {
                        array[i, j] = 1;
                        count++;
                    }
                    else
                    {
                        array[i, j] = 0;
                        count = 0;
                    }
                }
                else
                {
                    array[i, j] = 1;
                    count++;
                }
            }
            for (int j = 1; j < width - 1; j++)
            {
                if (array[i, j] == 0)
                {
                    if (!haveExit)
                    {
                        if (i + 2 < height)
                        {
                            array[i + 1, j - (2+Random.Range(-1,1))] = 1;
                        }
                    }
                    j++;
                    haveExit = false;
                }
                array[i, j] = 1;
                if (Random.Range(0, 2) == 1)
                {
                    if (i + 2 < height && j - 2 >= 0 && array[i + 1, j - 1] == 0 && array[i + 1, j - 2] == 0)
                    {
                        array[i + 1, j] = 1;
                        haveExit = true;
                    }
                }
                if (j == width - 2)
                {
                    if (i + 2 < height && array[i + 1, j - 1] == 0)
                    {
                        array[i + 1, j] = 1;
                    }
                }
            }
        }
        return array;
    }
}
