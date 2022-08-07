using UnityEngine;

public class Building : MonoBehaviour {
    private HealthSystem healthSystem;
    private BuildingTypesSO buildingType;
    private Transform buildingDemolishButton;
    private void Awake() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.GetHealthAmountMax(), true);
        buildingDemolishButton = transform.Find("pfBuildingDemoslish");
        HideDemolishButton();
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

    private void OnMouseEnter() {
        ShowDemolishButton();
    }

    private void OnMouseExit() {
        HideDemolishButton();
    }

    private void ShowDemolishButton() {
        if (buildingDemolishButton != null) {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }
    private void HideDemolishButton() {
        if (buildingDemolishButton != null) {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}
