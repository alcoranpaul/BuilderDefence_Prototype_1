using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypes", fileName = "NewBuildingType")]
public class BuildingTypesSO : ScriptableObject {
    [SerializeField] private string nameString;
    [SerializeField] private Transform prefab;
    [SerializeField] private ResourceGeneratorData resourceGeneratorData;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float minBuildingRadius;
    [SerializeField] private ResourceAmount[] constructionResourceCostArray;
    [SerializeField] private int healthAmountMax;
    [SerializeField] private bool isResource;
    [SerializeField] private float constructionTimerMax;

    public string GetName() {
        return nameString;
    }

    public bool HasResourceGeneratorData() {
        return resourceGeneratorData != null;
    }

    public float GetConstructionTimerMax() {
        return constructionTimerMax;
    }

    public bool IsResource() {
        return isResource;
    }

    public int GetHealthAmountMax() {
        return healthAmountMax;
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

    public ResourceGeneratorData GetResourceGeneratorData() {
        return resourceGeneratorData;
    }

    public Sprite GetSprite() {
        return sprite;
    }

    public float GetMinBuildingRadius() {
        return minBuildingRadius;
    }

    public ResourceAmount[] GetResourceAmounts() {
        return constructionResourceCostArray;
    }

    public string GetResourceCostString() {
        string str = "";
        foreach (ResourceAmount resource in constructionResourceCostArray) {
            str += "<color=#" + resource.resourceType.GetColorHex() + ">" + resource.resourceType.GetShortName() + resource.amount + "</color> ";
        }
        return str;
    }
}
