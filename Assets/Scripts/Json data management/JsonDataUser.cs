using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataUser<T>
{
    public T JsonData;

    private string jsonFileName;

    // We only save data when a wave is spawned
    public void SaveData()
    {
        JsonDataManagement.SaveData<T>(jsonFileName, JsonData);
    }

    public JsonDataUser(T _StartJsonData, string _jsonFileName)
    {
        jsonFileName = _jsonFileName;

        /// <summary>
        /// Currently we only need to load the data once as the game begins
        /// This is why we do the loading here in the constructor and why
        /// we don't have a dedicated load method
        /// </summary>

        if (JsonDataManagement.FileExists(jsonFileName) == true)
        {
            JsonData = JsonDataManagement.LoadData<T>(jsonFileName);
        }

        else
        {
            JsonData = _StartJsonData;
        }
    }
}
