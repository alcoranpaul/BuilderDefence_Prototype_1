using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypesList")]
public class BuildingTypesListSO : ScriptableObject {
    [SerializeField] private List<BuildingTypesSO> buildingTypesList;

    public BuildingTypesSO GetBuildingType(int index) {
        return buildingTypesList[index];
    }
}
