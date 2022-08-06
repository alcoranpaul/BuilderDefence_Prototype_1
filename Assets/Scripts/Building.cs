using UnityEngine;

public class Building : MonoBehaviour {
    private HealthSystem healthSystem;
    private BuildingTypesSO buildingType;
    private void Awake() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.GetHealthAmountMax(), true);
    }
    private void Start() {

        healthSystem.OnDied += HealthSystem_OnDied;
    }


    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }

    public string GetBuildingName() {
        return buildingType.GetName();
    }
}
