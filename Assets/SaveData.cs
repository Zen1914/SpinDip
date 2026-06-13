using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string Path => Application.persistentDataPath + "/save.json";
    public static void Save(RankingsData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path, json);
    }

    public static RankingsData Load()
    {
        if (!File.Exists(Path))
            return new RankingsData();

        string json = File.ReadAllText(Path);
        return JsonUtility.FromJson<RankingsData>(json);
    }
}
