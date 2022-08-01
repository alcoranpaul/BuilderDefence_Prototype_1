using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    private BuildingTypesSO buildingType;
    private BuildingTypesListSO buildingTypeList;
    private Camera m_Camera;
    private void Start() {
        m_Camera = Camera.main;
        buildingTypeList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);
        buildingType = buildingTypeList.GetBuildingType(0);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) { 
            Instantiate(buildingType.GetPrefab(), GetMousePosition(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            buildingType = buildingTypeList.GetBuildingType(0);
            Debug.Log(buildingType.GetName());
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            buildingType = buildingTypeList.GetBuildingType(1);
            Debug.Log(buildingType.GetName());
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            buildingType = buildingTypeList.GetBuildingType(2);
            Debug.Log(buildingType.GetName());
        }
    }

    private Vector3 GetMousePosition() {
        Vector3 mousePosition_Pixel = Input.mousePosition;
        Vector3 mousePosition_World = m_Camera.ScreenToWorldPoint(mousePosition_Pixel);
        mousePosition_World.z = 0f; //No need for z-axis
        return mousePosition_World;
    }
}
