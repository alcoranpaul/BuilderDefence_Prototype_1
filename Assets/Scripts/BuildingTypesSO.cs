using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypes", fileName = "NewBuildingType")]
public class BuildingTypesSO : ScriptableObject
{
    [SerializeField] private string nameString;
    [SerializeField] private Transform prefab;

    public string GetName() {
        return nameString;
    }
    public void SetName(string name) {
        nameString = name;
    }

    public Transform GetPrefab() {
        return prefab;
    }

}
