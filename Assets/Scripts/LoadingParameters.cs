using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public static class LoadingParameters
{
    private static string easyKey = "Easy";
    private static string normalKey = "Normal";
    private static string hardKey = "Hard";
    private static string hardcoreKey = "Hardcore";

    public static void Load()
    {
        GameEngine.MuteAudio = PlayerPrefs.GetInt("MuteAudio") != 0;
        GameEngine.PlayerGoldAll = PlayerPrefs.GetInt("Gold");
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("MuteAudio", GameEngine.MuteAudio?1:0);
        PlayerPrefs.SetInt("Gold", GameEngine.PlayerGoldAll);
    }

    public static List<Record> GetRecords(DifficultyLevel difficultyLevel)
    {
        List<Record> records = new List<Record>();
        string key = GetKey(difficultyLevel);
        
        for (int i = 0; i < 5; i++)
        {
            records.Add(new Record()
            {
                gold = PlayerPrefs.GetInt(string.Format(key + "Gold" + i.ToString()), 0),
                time = PlayerPrefs.GetFloat(string.Format(key + "Time" + i.ToString()), 0)
            });
        }

        return records;
    }

    public static void SaveRecord(DifficultyLevel difficultyLevel, Record record)
    {
        List<Record> records = GetRecords(difficultyLevel);
        string key = GetKey(difficultyLevel);

        records.Add(record);
        records = SortRecords(records);

        for (int i = 0; i < records.Count; i++)
        {
            PlayerPrefs.SetInt(string.Format(key + "Gold" + i.ToString()), records[i].gold);
            PlayerPrefs.SetFloat(string.Format(key + "Time" + i.ToString()), records[i].time);
        }
    }

    public static string GetKey(DifficultyLevel difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                return easyKey;
            case DifficultyLevel.Normal:
                return normalKey;
            case DifficultyLevel.Hard:
                return hardKey;
            case DifficultyLevel.Hardcore:
                return hardcoreKey;
        }
        return easyKey;
    }

    public static List<Record> SortRecords(List<Record> records)
    {
        Record[] recordsArray = records.ToArray();
        Record buffer = new Record();

        for (int i = 0; i < recordsArray.Count(); i++)
        {
            for (int j = i + 1; j < recordsArray.Count(); j++)
            {
                if (recordsArray[i].gold - recordsArray[i].time < recordsArray[j].gold - recordsArray[j].time)
                {
                    buffer = recordsArray[i];
                    recordsArray[i] = recordsArray[j];
                    recordsArray[j] = buffer;
                }
            }
        }
        records = new List<Record>();
        for (int i = 0; i < 5 && i < recordsArray.Count(); i++)
        {
            records.Add(recordsArray[i]);
        }

        return records;
    }
}

public struct Record
{
    public int gold;
    public float time;
}
