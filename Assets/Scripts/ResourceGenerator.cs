using UnityEngine;

public class ResourceGenerator : MonoBehaviour {
    private float timer;
    private float timerMax;
    private BuildingTypesSO buildingType;


    private void Awake() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.GetResourceGenerateTimerData();
    }

    private void Update() {
        collectResources();
    }

    private void collectResources() {
        timer -= Time.deltaTime;
        if (timer < 0f) {
            timer += timerMax;
            Debug.Log("Ding!! " + buildingType.GetResourceGenerateBuildType().GetName());
            ResourceManager.Instance.AddResource(buildingType.GetResourceGenerateBuildType(), 1);
        }
    }
}