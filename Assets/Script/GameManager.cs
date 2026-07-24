using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

/*
 * Absolute Rule:
 * When setting the path for playerData, the index of the scene is used.
 * i.e, if the scene has index 0, the playerData path will be Application.persistentPath + "/playerData_scene0.json"
 * if the scene has index 1, the playerData path will be Application.persistentPath + "/playerData_scene1.json"
 * and so on.
 */

public enum GameMode
{
    Easy,
    Normal,
    Hard
}
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance)
            {
                return instance;
            }
            else
            {
                instance = new GameObject("Game Manager").AddComponent<GameManager>();
                return instance;
            }
        }
    }



    private void Awake()
    {

        //setting  up  application's  persistant  path  on a  string yeyeyeye
        //_applicationPersistentPath = Application.persistentDataPath;

        //singleton boilerplate code
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }

        //actually  hardcoding the path of the save  files along with file names, be careful or else u will lose hair over stress caused by something so simple and  stupid
        //playerDataPath = Application.persistentDataPath + "/playerData.json";
        //checkpointDataPaths = new string[]
        //{
        //    _applicationPersistentPath + "/checkpointData_scene0.json",
        //    _applicationPersistentPath + "/checkpointData_scene1.json",
        //    _applicationPersistentPath + "/checkpointData_scene2.json",
        //    _applicationPersistentPath + "/checkpointData_scene3.json"
        //};

        //rn windows is making my life worse, idk why but likely  due to  driver issues with windows and my ancient rx 550 2gb gpu with some scheduling stuff, making the render loop very long like 17 ms or more, so like just capping the frames at 60 fps for  now  
        // Application.targetFrameRate = 60;
    }
    #endregion

    //#region Player References
    ////Player Reference
    //private PlayerController _pc;
    //public PlayerController pc
    //{
    //    get
    //    {
    //        if (_pc)
    //            return _pc;
    //        else
    //            return null;
    //    }
    //}
    //private CameraController _cc;
    //public CameraController cc
    //{
    //    get
    //    {
    //        if (_cc)
    //            return _cc;
    //        else
    //            return null;
    //    }
    //}
    //private GameObject _player;
    //public GameObject player
    //{
    //    get
    //    {
    //        if (_player)
    //            return _player;
    //        else
    //        {
    //            GameObject go = GameObject.FindGameObjectWithTag("Player");
    //            if (go)
    //            {
    //                return go;
    //            }
    //            else
    //                return null;
    //        }
    //    }
    //}

    //private checkpoint_manager _checkpoint_Manager;
    //public checkpoint_manager checkpoint_Manager
    //{
    //    get
    //    {
    //        if (_checkpoint_Manager)
    //            return _checkpoint_Manager;
    //        else
    //        {
    //            _checkpoint_Manager = GameObject.FindFirstObjectByType<checkpoint_manager>();
    //            return _checkpoint_Manager;
    //        }
    //    }

    //    set
    //    {
    //        _checkpoint_Manager = value;
    //    }
    //}

    //#endregion

    #region Data Management

//    #region Player Data
//    //data path in device directory
////    private static string playerDataPath;
////    private PlayerData _playerData = null;
////    public PlayerData playerData
////    {
////        get
////        {
////            if (_playerData != null)
////                return _playerData;
////            else
////            {
////                _playerData = new PlayerData();
////                LoadPlayerData();
////                return _playerData;
////            }
////        }

////        set
////        {
////            _playerData = value;
////        }
////    }

////    public void LoadPlayerData()
////    {
////        if (File.Exists(playerDataPath))
////        {
////            //reading json data
////            string json = File.ReadAllText(playerDataPath);
////            //converting it to player data
////            _playerData = JsonUtility.FromJson<PlayerData>(json);

////#if UNITY_EDITOR
////            Debug.Log("Player Data Loaded");
////#endif
////        }
////        else
////        {
////            _playerData = new PlayerData();
////            SavePlayerData(new PlayerData());

////#if UNITY_EDITOR
////            Debug.Log("Player Data path not found, creating new player data");
////#endif
////        }
////    }

////    public void SavePlayerData(PlayerData data)
////    {
////        //converting player data in json format
////        string jsonData = JsonUtility.ToJson(data, true);
////        //saving it to desired path
////        File.WriteAllText(playerDataPath, jsonData);

////#if UNITY_EDITOR
////        Debug.Log("Player Data Saved");
////#endif
////    }
//    #endregion
//    //old cp code used to be here
//    #region Checkpoint Data

//    [HideInInspector] public int latestCheckPointIndex = 0;
//    [HideInInspector] public int checkPointPlayerWantsToSpawnIn = 0;

//    ////keeping track of the lastest checkpoint position for quick respawn
//    //private Vector3 _lastestcheckpointPosition = Vector3.zero;
//    //public Vector3 lastestcheckpointPosition
//    //{
//    //    get
//    //    {
//    //        if (_lastestcheckpointPosition == Vector3.zero)
//    //        {
//    //            int i = localCheckpointData.checkPointPositions.Count;
//    //            SerializableVector3 _pos = localCheckpointData.checkPointPositions[i];
//    //            _lastestcheckpointPosition = _pos.ToVector3();
//    //        }
//    //        return _lastestcheckpointPosition;
//    //    }

//    //    set
//    //    {
//    //        _lastestcheckpointPosition = value;
//    //    }
//    //}

//    /*this is where we finally make separation between the player data and checkpoint data, so that we can save them separately and load them separately as well.*/

//    /*we bake in the checkpoint data path for each scene, so that we can save and load them separately. further follow the strict rule that the path is determined by the scene index, and the index of the array points to the scene index, thus when using in a particular scene, we use its buildscene index to find the correct path, and must ensure it is consistent to avoid errors*/

//    /*since we cannot run application.persistantpath from data types, and only run it at start/awake so creating a simple private persistentpath variables, initiliazed in the awake function*/

//    //private string _applicationPersistentPath;

//    ////keeping all the required player data paths for different data store needs
//    //private static string[] checkpointDataPaths = new string[]
//    //{
//    //    // _applicationPersistentPath + "/checkpointData_scene0.json",
//    //    // _applicationPersistentPath + "/checkpointData_scene1.json",
//    //    // _applicationPersistentPath + "/checkpointData_scene2.json",
//    //    // _applicationPersistentPath + "/checkpointData_scene3.json"

//    //    //the above data is rewritten from the awake function since u cannot use a string inside a static variable
//    //};

//    //private save_manager _saveManager; //local reference to the save manager
//    //public save_manager saveManager
//    //{
//    //    get
//    //    {
//    //        if (_saveManager)
//    //            return _saveManager;
//    //        else
//    //        {
//    //            _saveManager = save_manager.Instance;
//    //            return _saveManager;
//    //        }
//    //    }

//    //}

//    //public void SaveCheckpointData(CheckpointData data)
//    //{
//    //    string path = checkpointDataPaths[SceneManager.GetActiveScene().buildIndex];
//    //    saveManager.SavePlayerData(data, path);
//    //}

//    // public CheckpointData LoadCheckpointData()
//    // {
//    //     string path = checkpointDataPath[SceneManager.GetActiveScene().buildIndex];

//    // }

//    //private CheckpointData _localCheckpointData;
//    //public CheckpointData localCheckpointData
//    //{
//    //    get
//    //    {
//    //        if (_localCheckpointData == null)
//    //        {
//    //            //load check point data
//    //            string _path = checkpointDataPaths[SceneManager.GetActiveScene().buildIndex];

//    //            _localCheckpointData = saveManager.LoadPlayerData<CheckpointData>(_path);
//    //        }
//    //        return _localCheckpointData;
//    //    }
//    //}
//    //#endregion
//    #endregion

//    #region Unity logic

//    private void OnEnable()
//    {
//        SceneManager.activeSceneChanged += OnSceneChanged; ;
//    }

//    private void OnDisable()
//    {
//        SceneManager.activeSceneChanged -= OnSceneChanged;
//    }

//    private void OnApplicationQuit()
//    {
//        //Saving player data and checkpoint data
//        // SavePlayerData(playerData);
//        // SaveCPData(checkpointData);

//        //Clearing Notification Manager list
//        NotificationManager.Instance.RemoveAll();
//    }

//    private void OnSceneChanged(Scene previousScene, Scene newScene)
//    {
//        //SavePlayerData(playerData);
//        // SaveCPData(checkpointData);

//        //Clearing Notification Manager list
//        NotificationManager.Instance.RemoveAll();
//    }
//    #endregion

}

    #endregion