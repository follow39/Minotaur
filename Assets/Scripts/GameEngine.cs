using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class GameEngine
{
    public static sbyte[,] map; //карта лабиринта
    public static int Height = 0; //ширина лабиринта
    public static int Width = 0; //длина лабиринта
    public static bool CreateMap = false;
    public static float GameTime = 0.0f;
    public static bool GameIsStarted = false;
    public static bool GameIsPaused = false;
    public static bool GameIsFinished = false;
    public static bool CollectingGold = false;
    public static float CollectingTime = 0.0f;
    public static float DetectingTime = 0.0f;
    public static int PlayerHitPoints;
    public static int PlayerGold;
    public static int PlayerGoldAll;
    public static int PlayerGoldMaxInGame;
    public static bool MuteAudio = false;
    public static bool MonsterIsStuned = false;
    public static float StunnedTime;
    public static DifficultyLevel GameDifficultyLevel;
    public static Vector3 PlayerCoordinates = new Vector3();
    public static Vector3 MonsterCoordinates = new Vector3();
    public static Vector3 TreasureCoordinates = new Vector3();

    private static bool detecting = false;
    private static int countDetecting = 0;

    public static Action DetectingEvent;
    public static Action<bool> InvisibleEvent; 



    public static Vector3 GetPlayerCoordinates(int i, int j)
    {
        if (GameIsFinished)
        {
            return new Vector3(Width - 2, 0, Height - 2);
        }
        if (GameTime < 1)
        {
            return PlayerCoordinates;
        }
        if (GameDifficultyLevel == DifficultyLevel.Hardcore)
        {
            if (GameTime%30 < 1)
            {
                return PlayerCoordinates;
            }
        }
        if (GameDifficultyLevel == DifficultyLevel.Hard)
        {
            if (GameTime%45 < 1)
            {
                return PlayerCoordinates;
            }
        }

        int k = 0;
        switch (GameDifficultyLevel)
        {
            case DifficultyLevel.Easy:
                k = 4;
                break;

            case DifficultyLevel.Normal:
                k = 4;
                break;

            case DifficultyLevel.Hard:
                k = 5;
                break;

            case DifficultyLevel.Hardcore:
                k = 5;
                break;
        }

        if (Math.Abs(i - (int) PlayerCoordinates.z) < k && Math.Abs(j - (int) PlayerCoordinates.x) < k)
        {
            if(!detecting)
            {
                detecting = true;
                if(DetectingEvent!=null)
                {
                    countDetecting = 0;
                    DetectingEvent();
                }
            }
            return PlayerCoordinates;
        }
        countDetecting++;
        if (countDetecting > 5/Time.deltaTime)
        {
            detecting = false;
        }
        return new Vector3();
    }

    public static Vector3 GetTresureCoordinates(int i, int j)
    {
        if (Math.Abs(i - (int) TreasureCoordinates.z) > 3 && Math.Abs(j - (int) TreasureCoordinates.x) > 3)
        {
            if (map[(int) TreasureCoordinates.z + 1, (int) TreasureCoordinates.x] > 0)
            {
                return TreasureCoordinates + new Vector3(0, 0, 1);
            }
            if (map[(int) TreasureCoordinates.z - 1, (int) TreasureCoordinates.x] > 0)
            {
                return TreasureCoordinates + new Vector3(0, 0, -1);
            }
            if (map[(int) TreasureCoordinates.z, (int) TreasureCoordinates.x + 1] > 0)
            {
                return TreasureCoordinates + new Vector3(1, 0, 0);
            }
            if (map[(int) TreasureCoordinates.z, (int) TreasureCoordinates.x - 1] > 0)
            {
                return TreasureCoordinates + new Vector3(-1, 0, 0);
            }
            return TreasureCoordinates;
        }
        return new Vector3();
    }


    public static void CollectGold(sbyte i, sbyte j)
    {
        if (Math.Abs(i - (sbyte) TreasureCoordinates.z) > 1 && Math.Abs(j - (sbyte) TreasureCoordinates.x) > 1)
        {
            return;
        }

        CollectingGold = true;
        if (GameTime - CollectingTime < 0.1)
        {
            return;
        }
        if (PlayerGoldMaxInGame > PlayerGold)
        {
            PlayerGold += 5;
            CollectingTime = GameTime;
            return;
        }
        else
        {
            CollectingGold = false;
        }
        return;
    }

    public static bool ActivateWallTrap(sbyte i, sbyte j)
    {
        if (Math.Abs(i - (sbyte) PlayerCoordinates.z) < 2 && Math.Abs(j - (sbyte) PlayerCoordinates.x) == 0 ||
            Math.Abs(i - (sbyte) PlayerCoordinates.z) == 0 && Math.Abs(j - (sbyte) PlayerCoordinates.x) < 2)
        {
            return true;
        }
        return false;
    }

    public static bool ActivateFloorTrap(sbyte i, sbyte j)
    {
        return (Math.Abs(i - (sbyte) PlayerCoordinates.z) == 0 && Math.Abs(j - (sbyte) PlayerCoordinates.x) == 0);
    }

    public static void AddGold()
    {
        PlayerGoldAll += PlayerGold;
        PlayerGold = 0;
    }

    public static void ChangeMute()
    {
        MuteAudio = !MuteAudio;
    }

    public static bool IsStuned()
    {
        if (MonsterIsStuned)
        {
            if (GameTime - StunnedTime < 5)
            {
                return true;
            }
        }
        return false;
    }

    public static void Stunned()
    {
        MonsterIsStuned = true;
        StunnedTime = GameTime;
    }

    public static void SetMaxGold(int maxGold)
    {
        switch (GameDifficultyLevel)
        {
            case DifficultyLevel.Easy:
                if(maxGold>350)
                {
                    PlayerGoldMaxInGame = 350;
                }
                else
                {
                    PlayerGoldMaxInGame =  maxGold;
                }
                break;

            case DifficultyLevel.Normal:
                if (maxGold > 650)
                {
                    PlayerGoldMaxInGame = 350;
                }
                else
                {
                    PlayerGoldMaxInGame = maxGold;
                }
                break;

            case DifficultyLevel.Hard:
                if (maxGold > 1150)
                {
                    PlayerGoldMaxInGame = 1150;
                }
                else
                {
                    PlayerGoldMaxInGame = maxGold;
                }
                break;

            case DifficultyLevel.Hardcore:
                if (maxGold > 1750)
                {
                    PlayerGoldMaxInGame = 1750;
                }
                else
                {
                    PlayerGoldMaxInGame = maxGold;
                }
                break;
        }
    }
}

public enum DifficultyLevel//уровни сложности
{
    Easy,
    Normal,
    Hard,
    Hardcore,
    Impossible
}


public enum MoveDirection
{
    Right,
    Left,
    Up,
    Down,
    None
}

public enum RotateDirection
{
    Сlockwise,
    Сounterclockwise,
    None
}