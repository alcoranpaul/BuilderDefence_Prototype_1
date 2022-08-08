using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour {
    private ResourceGeneratorData resourceGeneratorData;
    public bool nearbyResource;

    private void Awake() {
        Hide();
    }
    private void Update() {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.parent.position);
        nearbyResource = (nearbyResourceAmount > 0);

        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }
    public void Show(ResourceGeneratorData resourceGenerator) {
        this.resourceGeneratorData = resourceGenerator;
        gameObject.SetActive(true);
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.GetSprite();


    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
