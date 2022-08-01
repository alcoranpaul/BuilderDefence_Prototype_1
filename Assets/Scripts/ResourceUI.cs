using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourcesTypeTransformDict;
    private void Awake() {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourcesTypeTransformDict = new Dictionary<ResourceTypeSO, Transform>();
        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false); //Hide Template

        int index = 0;
        float offSetAmount = -100f;
        foreach (ResourceTypeSO resourceType in resourceTypeList.GetResourceTypeList()) {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetAmount * index, 0);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.GetSprite();

            resourcesTypeTransformDict[resourceType] = resourceTransform;
            index++;
        }
    }

    private void Start() {
        //Subscribe to Even handler
        ResourceManager.Instance.OnResourceAmountChange += ResourceManager_OnResourceAmountChange;
        UpdateResouceAmount();
    }

    private void ResourceManager_OnResourceAmountChange(object sender, System.EventArgs e) {
        UpdateResouceAmount();
    }

    private void UpdateResouceAmount() {
        foreach (ResourceTypeSO resourceType in resourceTypeList.GetResourceTypeList()) {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            Transform resourceTransform = resourcesTypeTransformDict[resourceType];
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }

    }
}
