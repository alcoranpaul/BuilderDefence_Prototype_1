using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeUI : MonoBehaviour {
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypesSO> ignoredBuildingTypes;
    private Dictionary<BuildingTypesSO, Transform> btnBuildingTypeDict;
    private Transform arrowBtn;
    private void Awake() {
        btnBuildingTypeDict = new Dictionary<BuildingTypesSO, Transform>();
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        int index = 0;
        float offSetAmount = 120f;

        SetArrowBtn(btnTemplate, offSetAmount, index);
        index++;
        SetBuildingBtn(btnTemplate, offSetAmount, index);


    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypesSO buildingType in btnBuildingTypeDict.Keys) {
            Transform btnTranform = btnBuildingTypeDict[buildingType];
            btnTranform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypesSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null) {
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else {
            btnBuildingTypeDict[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }

    private void SetArrowBtn(Transform btnTemplate, float offSetAmount, int index) {
        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetAmount * index, 0);
        arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowBtn.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        MouseEnterExitEvents mouseEnterExitEvents = arrowBtn.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
            TooltipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
            TooltipUI.Instance.Hide();
        };
    }
    private void SetBuildingBtn(Transform btnTemplate, float offSetAmount, int index) {
        BuildingTypesListSO buildingTypeList = Resources.Load<BuildingTypesListSO>(typeof(BuildingTypesListSO).Name);
        foreach (BuildingTypesSO buildingType in buildingTypeList.GetBuildingType()) {
            if (ignoredBuildingTypes.Contains(buildingType)) continue;
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offSetAmount * index, 0);
            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.GetSprite();

            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            MouseEnterExitEvents mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
                TooltipUI.Instance.Show(buildingType.GetName() + "\n" + buildingType.GetResourceCostString());
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
                TooltipUI.Instance.Hide();
            };

            btnBuildingTypeDict[buildingType] = btnTransform;
            index++;
        }
    }
}
