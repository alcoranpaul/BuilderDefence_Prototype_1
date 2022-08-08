using UnityEngine;

public class BuildingConstruction : MonoBehaviour {
    private BuildingTypesSO buildingType;
    private BoxCollider2D boxCollider2D;
    private float constructionTimer, constructionTimerMax;
    private SpriteRenderer buildingSprite;
    private BuildingTypeHolder buildingTypeHolder;
    private Material buildingTypeMaterial;

    public static BuildingConstruction Create(Vector3 position, BuildingTypesSO buildingType) {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;

    }



    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        buildingSprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        buildingTypeMaterial = buildingSprite.material;
    }

    private void Update() {
        constructionTimer -= Time.deltaTime;
        buildingTypeMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (constructionTimer <= 0) {
            Instantiate(buildingType.GetPrefab(), transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypesSO buildingType) {
        this.constructionTimerMax = buildingType.GetConstructionTimerMax();
        this.constructionTimer = buildingType.GetConstructionTimerMax();
        this.buildingType = buildingType;
        buildingSprite.sprite = buildingType.GetSprite();
        buildingTypeHolder.buildingType = buildingType;
        boxCollider2D.offset = buildingType.GetPrefab().GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.GetPrefab().GetComponent<BoxCollider2D>().size;
    }

    public float GetConstructionTimerNormalized() {
        return 1 - constructionTimer / constructionTimerMax;
    }
}
