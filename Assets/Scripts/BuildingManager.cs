using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour {

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
                Instantiate(activeBuildingType.GetPrefab(), GetMousePosition(), Quaternion.identity);
            }
        }
    }

    private Vector3 GetMousePosition() {
        Vector3 mousePosition_Pixel = Input.mousePosition;
        Vector3 mousePosition_World = m_Camera.ScreenToWorldPoint(mousePosition_Pixel);
        mousePosition_World.z = 0f; //No need for z-axis
        return mousePosition_World;
    }

    public void SetActiveBuildingType(BuildingTypesSO buildingType) {
        activeBuildingType = buildingType;
    }

    public BuildingTypesSO GetActiveBuildingType() {
        return activeBuildingType;
    }
}
