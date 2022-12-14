using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour {
    //Instance Variables
    [SerializeField] private Building HQBuilding;
    private BuildingTypesSO activeBuildingType;
    private BuildingTypesListSO buildingTypeList;
    private Camera m_Camera;


    public static BuildingManager Instance { get; private set; } //Singleton

    //Instance Events
    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;
    public class OnActiveBuildingTypeChangeEventArgs : EventArgs {
        public BuildingTypesSO activeBuildingType;
    }


    private void Awake() {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);

    }
    private void Start() {
        m_Camera = Camera.main;
        HQBuilding.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    private void HQ_OnDied(object sender, EventArgs e) {
        GameOverUI.Instance.Show();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (activeBuildingType != null) {
                if (CanSpawnBuilding(activeBuildingType, Utilities.GetMousePosition(), out string errorMessaage)) {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.GetResourceAmounts())) {
                        ResourceManager.Instance.SpendResources(activeBuildingType.GetResourceAmounts());
                        //Instantiate(activeBuildingType.GetPrefab(), Utilities.GetMousePosition(), Quaternion.identity);
                        BuildingConstruction.Create(Utilities.GetMousePosition(), activeBuildingType);
                        SetActiveBuildingType(null);
                        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                    }
                    else {
                        TooltipUI.Instance.Show("Cannot afford " + activeBuildingType.GetResourceCostString(), new TooltipUI.TooltipTimer(2f));
                    }
                }
                else {
                    TooltipUI.Instance.Show(errorMessaage, new TooltipUI.TooltipTimer(2f));
                }
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

    public bool CanSpawnBuilding(BuildingTypesSO buildingType, Vector3 position, out string errorMessage) {
        BoxCollider2D boxCollider2D = buildingType.GetPrefab().GetComponent<BoxCollider2D>();

        // Is Area clear?
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2D.Length == 0;
        if (!isAreaClear) {
            errorMessage = "Area is not clear!!";
            return false;
        }

        // Is Building same type as spawned?
        collider2D = Physics2D.OverlapCircleAll(position, buildingType.GetMinBuildingRadius());
        foreach (Collider2D collider in collider2D) {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            // Am I spawning a building?
            if (buildingTypeHolder != null) {
                if (buildingTypeHolder.buildingType == buildingType) {
                    errorMessage = "Cannot spawn near another " + buildingType.GetName() + "!";
                    return false;
                }
            }
        }
        if (buildingType.IsResource()) {
            ResourceGeneratorData resourceGeneratorData = buildingType.GetResourceGeneratorData();
            int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, position);

            if (nearbyResourceAmount == 0) {
                errorMessage = "There are no resources nearby";
                return false;
            }
        }


        //Am I too far from other buildings?
        float maxConstructionRadius = 25f;
        collider2D = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider in collider2D) {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            // Am I spawning a building?
            if (buildingTypeHolder != null) {
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "Cannot spawn far from other buildings!!";
        return false;

    }

    public Building GetHQ() {
        return HQBuilding;
    }
}
