using System.IO;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LoadOptions();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SaveOptions();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            LoadOptions();
        }
    }
    private void SaveOptions() {
        SaveObject saveObject = new(SoundManager.Instance.GetVolume(),
            MusicManager.Instance.GetVolume(),
            CameraHandler.Instance.GetEdgeScrolling());
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText($"{Application.dataPath}/save.txt", json);

        Debug.Log("SAVED");
    }

    public void LoadOptions() {
        if (File.Exists($"{Application.dataPath}/save.txt")) {
            string saveString = File.ReadAllText($"{Application.dataPath}/save.txt");
            SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(saveString);

            SoundManager.Instance.SetVolume(loadedSaveObject.soundVolume);
            MusicManager.Instance.SetVolume(loadedSaveObject.musicVolume);

            CameraHandler.Instance.SetEdgeScrolling(loadedSaveObject.edgeScrolling);
            Debug.Log("Save has Loaded");
        }
        else {
            Debug.Log("No Save");
        }
    }

}
