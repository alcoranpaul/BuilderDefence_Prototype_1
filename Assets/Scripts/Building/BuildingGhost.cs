using UnityEngine;

public class BuildingGhost : MonoBehaviour {
    private Transform spriteGameObject, circle;
    private SpriteRenderer circleSprite;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    private BuildingTypesSO buildingType;
    private void Awake() {
        spriteGameObject = transform.Find("sprite");
        circle = transform.Find("circle");
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
            if (e.activeBuildingType.IsResource()) {
                resourceNearbyOverlay.Show(buildingType.GetResourceGeneratorData());
                circle.transform.localScale = new Vector3(buildingType.GetResourceGeneratorData().resourceDetectionRadius * 2, buildingType.GetResourceGeneratorData().resourceDetectionRadius * 2);
            }
            else {
                resourceNearbyOverlay.Hide();
            }
        }
    }

    private void Update() {
        transform.position = Utilities.GetMousePosition();
        circleSprite.color = (resourceNearbyOverlay.nearbyResource && BuildingManager.Instance.CanSpawnBuilding(buildingType, transform.position, out _)) ?
            new Color32(0, 255, 0, 64) : new Color32(255, 0, 0, 64);
    }
    private void Show(Sprite ghostSprite) {
        spriteGameObject.gameObject.SetActive(true);
        circle.gameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide() {
        spriteGameObject.gameObject.SetActive(false);
        circle.gameObject.SetActive(false);
    }

}
