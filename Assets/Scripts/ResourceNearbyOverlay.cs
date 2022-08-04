using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour {
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake() {
        Hide();
    }
    private void Update() {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
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
