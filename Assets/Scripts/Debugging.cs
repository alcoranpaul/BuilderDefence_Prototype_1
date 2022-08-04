using UnityEngine;

public class Debugging : MonoBehaviour {
    private BuildingTypesSO buildingType;
    private void Awake() {
        buildingType = GetComponent
    }
    public static void Debug_AddCircle() {
        Transform circleRadius = transform.Find("radius");
        Debug.Log(new Vector3(buildingType.GetMinBuildingRadius(), buildingType.GetMinBuildingRadius(), 1));
        circleRadius.localScale = new Vector3(buildingType.GetMinBuildingRadius(), buildingType.GetMinBuildingRadius(), 1);
    }
}
