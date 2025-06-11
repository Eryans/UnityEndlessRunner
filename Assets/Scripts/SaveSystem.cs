using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string savePath = Application.dataPath + "/save.json";

    public static void Save(string jsonString)
    {
        File.WriteAllText(savePath, jsonString);
    }
    public static string Load()
    {
        if (File.Exists(savePath))
        {
            return File.ReadAllText(savePath);
        }
        return null;
    }
}