using UnityEngine;

public class ResourceGenerator : MonoBehaviour {
    private float timer;
    private float timerMax;
    private BuildingTypesSO buildingType;
    private ResourceGeneratorData resourceGeneratorData;
    private int resourcesAmount;

    private void Awake() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.GetResourceGeneratorData();


        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start() {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        resourcesAmount = nearbyResourceAmount;
        if (nearbyResourceAmount == 0) {
            //No resources nearby
            enabled = false;
        }
        else {
            timerMax = (resourceGeneratorData.timerMax / 2f) +
                resourceGeneratorData.timerMax *
                (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
    }

    private void Update() {
        //if (GetNearbyResourceAmount(resourceGeneratorData, transform.position) != 0) {
        collectResources();
        //}

    }

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position) {

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D collider in collider2DArray) {
            ResourceNode node = collider.GetComponent<ResourceNode>();
            if (node != null) {
                //Its a node
                if (node.resourceType == resourceGeneratorData.resourceType) {
                    nearbyResourceAmount++;
                }

            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
    public static int ConsumeResources(ResourceGeneratorData resourceGeneratorData, Vector3 position) {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;
        foreach (Collider2D collider in collider2DArray) {
            ResourceNode node = collider.GetComponent<ResourceNode>();
            if (node != null) {
                //Its a node
                if (node.resourceType == resourceGeneratorData.resourceType && !node.isEmpty) {
                    node.RemoveResourceAmount();
                }

            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }


    private void collectResources() {
        timer -= Time.deltaTime;
        if (timer < 0f) {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
            //ConsumeResources(resourceGeneratorData, transform.position);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData() {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized() {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond() {
        return 1 / timerMax;
    }
}