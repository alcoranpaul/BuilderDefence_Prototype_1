using UnityEngine;
public class GameHandler : MonoBehaviour {


    private void Awake() {
        SaveObject saveObject = new SaveObject(5f, 1f);
        string json = JsonUtility.ToJson(saveObject);

        Debug.Log($"JsonFile: {json}");

        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log($"From JsonFile: {loadedSaveObject.soundVolume}\n{loadedSaveObject.musicVolume}");
    }



}
