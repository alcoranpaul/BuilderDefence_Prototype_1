using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    //Singleton
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> resourceAmountDict;

    private void Awake() {
        Instance = this;
        resourceAmountDict = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceTypeSO in resourceTypeListSO.GetResourceTypeList()) {
            resourceAmountDict[resourceTypeSO] = 0;
        }
    }

    private void TestLog() {
        foreach (ResourceTypeSO resource in resourceAmountDict.Keys) {
            Debug.Log(resource.GetName() + ": " + resourceAmountDict[resource]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDict[resourceType] += amount;
        TestLog();
    }
}