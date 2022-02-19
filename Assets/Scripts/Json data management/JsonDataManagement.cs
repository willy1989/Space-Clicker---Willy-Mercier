using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonDataManagement
{
    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + fileName;
    }

    public static void SaveData<T>(string fileName, T data)
    {
        string path = GetPath(fileName);

        string jsonString = JsonUtility.ToJson(data);

        using StreamWriter writer = new StreamWriter(path);

        writer.Write(jsonString);
    }

    public static T LoadData<T>(string fileName)
    {
         string path = GetPath(fileName);

         using StreamReader reader = new StreamReader(path);

         string json = reader.ReadToEnd();

         return JsonUtility.FromJson<T>(json);
    }

    public static bool FileExists(string fileName)
    {
        string path = GetPath(fileName);

        return File.Exists(path);
    }
}
