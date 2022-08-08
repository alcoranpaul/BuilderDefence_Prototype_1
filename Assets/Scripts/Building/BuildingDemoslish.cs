using UnityEngine;
using UnityEngine.UI;

public class BuildingDemoslish : MonoBehaviour {
    [SerializeField] private Building building;
    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            BuildingTypesSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.GetResourceAmounts()) {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * 0.6f));
            }
            Destroy(building.gameObject);
        });
    }

}
