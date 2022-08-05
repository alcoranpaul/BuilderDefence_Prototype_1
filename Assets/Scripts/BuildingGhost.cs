using UnityEngine;

public class BuildingGhost : MonoBehaviour {
    private GameObject spriteGameObject, circle;
    private SpriteRenderer circleSprite;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    private BuildingTypesSO buildingType;
    private void Awake() {
        spriteGameObject = transform.Find("sprite").gameObject;
        circle = transform.Find("circle").gameObject;
        circleSprite = circle.GetComponent<SpriteRenderer>();

        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        if (e.activeBuildingType == null) {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else {
            buildingType = e.activeBuildingType;
            Show(buildingType.GetSprite());
            resourceNearbyOverlay.Show(buildingType.GetResourceGeneratorData());
            circle.transform.localScale = new Vector3(buildingType.GetResourceGeneratorData().resourceDetectionRadius * 2, buildingType.GetResourceGeneratorData().resourceDetectionRadius * 2);

        }
    }

    private void Update() {
        transform.position = Utilities.GetMousePosition();
        circleSprite.color = (resourceNearbyOverlay.nearbyResource && BuildingManager.Instance.CanSpawnBuilding(buildingType, transform.position, out string errorMessage)) ?
            new Color32(0, 255, 0, 64) : new Color32(255, 0, 0, 64);
    }
    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        circle.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide() {
        spriteGameObject.SetActive(false);
        circle.SetActive(false);
    }

}
