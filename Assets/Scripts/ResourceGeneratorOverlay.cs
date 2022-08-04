using TMPro;
using UnityEngine;
public class ResourceGeneratorOverlay : MonoBehaviour {
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform bar;
    private void Start() {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        bar = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.GetSprite();
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update() {
        bar.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
