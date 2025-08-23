using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    public static List<T> ReadFromJson<T>(string data)
    {
        return FromJson<T>(data).ToList();
    }

    private class Wrapper<T>
    {
        public T[] Items;
    }

}

[Serializable]
public class MusicData
{
    public string musicId;
    public string musicName;
    public string musicDiffType;
    public string musicDiff;
    public string accuracy;
    public string score;

    public void print()
    {
        Debug.Log(musicId + " : " + musicName);
    }
}


public class DataManager : MonoBehaviour
{
    public MusicListManager musicListManager;
    public bool isTest;
    public MusicData[] musicList;
    private string fileName = "./Assets/SaveData/" + "sampleMusicData.txt";

    private void Start()
    {
        if(isTest)
        {
            LoadMusicTrack();
        }
    }

    //First Init Music Data (Test)
    public void SaveMusicTrack()
    {
        musicList = new MusicData[3];

        musicList[0] = new MusicData();
        musicList[0].musicId = "0";
        musicList[0].musicName = "lemonmeloncookie";
        musicList[0].musicDiffType = "0";
        musicList[0].musicDiff = "3";
        musicList[0].accuracy = "0.00%";
        musicList[0].score = "0";

        musicList[1] = new MusicData();
        musicList[1].musicId = "1";
        musicList[1].musicName = "duipalam_head_lether";
        musicList[1].musicDiffType = "0";
        musicList[1].musicDiff = "5";
        musicList[1].accuracy = "0.00%";
        musicList[1].score = "0";

        musicList[2] = new MusicData();
        musicList[2].musicId = "2";
        musicList[2].musicName = "gamegemothe";
        musicList[2].musicDiffType = "0";
        musicList[2].musicDiff = "1";
        musicList[2].accuracy = "0.00%";
        musicList[2].score = "0";

        string saveData = JsonHelper.ToJson(musicList, true);
        SaveFileData(saveData);
    }

    public void LoadMusicTrack()
    {
        if(File.Exists(fileName))
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string content = reader.ReadToEnd();
                List<MusicData> loadData = JsonHelper.ReadFromJson<MusicData>(content);
                musicList = new MusicData[loadData.Count];
                for (int i = 0; i < loadData.Count; i++)
                {
                    musicListManager.MusicListAdd(loadData[i]);
                    musicList[i] = loadData[i];
                }
            }
        }
        musicListManager.ListStart();
    }

    public void FixScore(int id, int diffType, int score)
    {
        Debug.Log(musicList[id]);
    }

    public void SaveFileData(string data)
    {
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            outputFile.WriteLine(data);
        }
    }
}
