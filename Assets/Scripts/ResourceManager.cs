using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDict;

    private void Awake() {
        resourceAmountDict = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceTypeSO in resourceTypeListSO.GetResourceTypeList()) {
            resourceAmountDict[resourceTypeSO] = 0;
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDict[resourceType] += amount;
    }
}
