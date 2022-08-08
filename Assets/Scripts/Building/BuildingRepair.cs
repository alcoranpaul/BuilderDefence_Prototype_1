using UnityEngine;
using UnityEngine.UI;

public class BuildingRepair : MonoBehaviour {
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;
    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceAmount = new ResourceAmount[] { new ResourceAmount { resourceType = goldResourceType, amount = repairCost } };
            if (ResourceManager.Instance.CanAfford(resourceAmount)) {
                ResourceManager.Instance.SpendResources(resourceAmount);
                healthSystem.FullHeal();
            }
            else {
                TooltipUI.Instance.Show($"Cannot afford heal", new TooltipUI.TooltipTimer(2f));
            }

        });
    }

}
