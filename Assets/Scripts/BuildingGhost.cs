using UnityEngine;

public class BuildingGhost : MonoBehaviour {
    private GameObject spriteGameObject;
    private void Awake() {
        spriteGameObject = transform.Find("sprite").gameObject;
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        if (e.activeBuildingType == null) {
            Hide();
        }
        else {
            Show(e.activeBuildingType.GetSprite());
        }
    }

    private void Update() {
        transform.position = Utilities.GetMousePosition();
    }
    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide() {
        spriteGameObject.SetActive(false);
    }
}
