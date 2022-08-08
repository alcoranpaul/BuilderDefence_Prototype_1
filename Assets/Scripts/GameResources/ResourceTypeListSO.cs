using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeList")]
public class ResourceTypeListSO : ScriptableObject {
    [SerializeField] private List<ResourceTypeSO> resourceTypesList;

    public List<ResourceTypeSO> GetResourceTypeList() {
        return resourceTypesList;
    }
}
