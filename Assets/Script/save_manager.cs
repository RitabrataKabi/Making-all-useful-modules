using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class DataObject
{
    // This class can be extended to include any data you want to save.
    // For example, player stats, inventory, settings, etc.
}

public class save_manager : MonoBehaviour
{

    #region Singleton
    private static save_manager instance;
    public static save_manager Instance
    {
        get
        {
            if (instance)
            {
                return instance;
            }
            else
            {
                instance = new GameObject("Save Manager").AddComponent<save_manager>();
                return instance;
            }
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    #endregion
    //what i want this to do
    // - have a simple save() and load() function that can be called from anywhere in the game
    // - need to feed in only the data i want to save, and the save manager will handle the rest
    //write data to a file
    //planning to use json to write the data
    //save data
    private string _defaultPlayerDataPath;
    public string defaultPlayerDataPath
    {
        get
        {
            if (_defaultPlayerDataPath == null)
            {
                _defaultPlayerDataPath = Application.persistentDataPath + "/playerData_scene" + SceneManager.GetActiveScene().buildIndex + ".json";
            }
            return _defaultPlayerDataPath;
        }

        set
        {
            _defaultPlayerDataPath = value;
        }
    }

    public void SavePlayerData<T>(T dataobjectreceived, string path = null) where T : DataObject
    {
        if (path == null)
        {
            path = defaultPlayerDataPath;
        }
        string json = JsonUtility.ToJson(dataobjectreceived);
        File.WriteAllText(path, json);
    }

    //load data
    //read from the json file and load the correctly formatted data into the current game scene
    public T LoadPlayerData<T>(string path = null) where T : DataObject, new()
    {
        if (path == null)
        {
            path = defaultPlayerDataPath;
        }

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        return new T();
    }
}
