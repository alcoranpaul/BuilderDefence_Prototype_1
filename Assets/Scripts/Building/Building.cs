using UnityEngine;

public class Building : MonoBehaviour {
    private HealthSystem healthSystem;
    private BuildingTypesSO buildingType;
    private Transform circle;
    private Transform buildingDemolishButton, buildingRepairButton;

    private void Awake() {
        buildingDemolishButton = transform.Find("pfBuildingDemoslish");
        buildingRepairButton = transform.Find("pfBuildingRepair");
        circle = transform.Find("radius");
        circle.gameObject.SetActive(false);
        HideDemolishButton();
        HideRepairButton();
    }
    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.GetHealthAmountMax(), true);
        //if (circle != null) {
        //    circle.transform.localScale = new Vector3(buildingType.GetResourceGeneratorData().resourceDetectionRadius * 2, buildingType.GetResourceGeneratorData().resourceDetectionRadius * 2);
        //}
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e) {
        if (healthSystem.IsFullHealth()) HideRepairButton();
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e) {
        ShowRepairButton();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CameraShake.Instance.ShakeCamera(5f, 0.1f);
        ChromaticAborationEffect.Instance.SetWeight(1f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
        CameraShake.Instance.ShakeCamera(10f, 0.15f);
        ChromaticAborationEffect.Instance.SetWeight(1f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        Instantiate(GameAssets.Instance.pfBuildingDestroyedParticles, transform.position, Quaternion.identity);
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
    private void ShowRepairButton() {
        if (buildingRepairButton != null) {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }
    private void HideRepairButton() {
        if (buildingRepairButton != null) {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
