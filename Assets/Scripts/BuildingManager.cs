using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour {

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;
    public class OnActiveBuildingTypeChangeEventArgs : EventArgs {
        public BuildingTypesSO activeBuildingType;
    }
    public static BuildingManager Instance { get; private set; } //Singleton
    private BuildingTypesSO activeBuildingType;
    private BuildingTypesListSO buildingTypeList;
    private Camera m_Camera;

    private void Awake() {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);
    }
    private void Start() {
        m_Camera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (activeBuildingType != null) {
                Instantiate(activeBuildingType.GetPrefab(), Utilities.GetMousePosition(), Quaternion.identity);
            }
        }
    }



    public void SetActiveBuildingType(BuildingTypesSO buildingType) {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this,
            new OnActiveBuildingTypeChangeEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypesSO GetActiveBuildingType() {
        return activeBuildingType;
    }
}
