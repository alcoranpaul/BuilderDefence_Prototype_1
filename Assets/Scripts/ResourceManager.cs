using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    //Singleton
    public static ResourceManager Instance { get; private set; }

    //Event Handle
    public event EventHandler OnResourceAmountChange;

    //Instance variables
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
        OnResourceAmountChange?.Invoke(this, EventArgs.Empty); //Even Trigger and Null check

    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDict[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts) {
        foreach (ResourceAmount resourceAmount in resourceAmounts) {
            if (GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount) {
                //Can not Afford
                return false;
            }
        }
        return true;
    }
    public bool SpendResources(ResourceAmount[] resourceAmounts) {
        foreach (ResourceAmount resourceAmount in resourceAmounts) {
            resourceAmountDict[resourceAmount.resourceType] -= resourceAmount.amount;
        }
        return true;
    }
}