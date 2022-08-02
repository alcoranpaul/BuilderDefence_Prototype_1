using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypes", fileName = "NewBuildingType")]
public class BuildingTypesSO : ScriptableObject {
    [SerializeField] private string nameString;
    [SerializeField] private Transform prefab;
    [SerializeField] private ResourceGeneratorData resourceGeneratorData;
    [SerializeField] private Sprite sprite;

    public string GetName() {
        return nameString;
    }
    public void SetName(string name) {
        nameString = name;
    }

    public Transform GetPrefab() {
        return prefab;
    }

    public float GetResourceGenerateTimerData() {
        return resourceGeneratorData.timerMax;
    }

    public ResourceTypeSO GetResourceGenerateBuildType() {
        return resourceGeneratorData.resourceType;
    }

    public Sprite GetSprite() {
        return sprite;
    }


}
