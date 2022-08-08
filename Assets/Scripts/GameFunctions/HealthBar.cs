using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    private Transform seperatorContianer;

    private void Awake() {
        barTransform = transform.Find("bar");

    }

    private void Start() {
        seperatorContianer = transform.Find("separatorContianer");
        ConstructHealthBarSeperator();

        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        healthSystem.OnHealthAmountMaxChange += HealthSystem_OnHealthAmountMaxChange;
        UpdateBar();
        UpdateBarVisibile();
    }

    private void HealthSystem_OnHealthAmountMaxChange(object sender, System.EventArgs e) {
        ConstructHealthBarSeperator();
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateBarVisibile();
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateBarVisibile();
    }

    private void UpdateBar() {
        barTransform.localScale = new Vector3(healthSystem.GetHealthNormalized(), 1, 1);
    }
    private void ConstructHealthBarSeperator() {

        Transform seperatorTemplate = seperatorContianer.Find("seperatorTemplate");
        seperatorTemplate.gameObject.SetActive(false);

        foreach (Transform seperatorTransform in seperatorContianer) {
            if (seperatorTransform == seperatorTemplate) continue;
            Destroy(seperatorTransform.gameObject);
        }

        int healthAmountPerSeparator = 10;
        float barSize = 3f;
        float barOneHealthamountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeperator = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);
        for (int i = 0; i < healthSeperator; i++) {
            Transform seperatorTransform = Instantiate(seperatorTemplate, seperatorContianer);
            seperatorTransform.gameObject.SetActive(true);
            seperatorTransform.localPosition = new Vector3(barOneHealthamountSize * i * healthAmountPerSeparator, 0, 0);
        }
    }

    private void UpdateBarVisibile() {
        if (healthSystem.IsFullHealth()) {
            Hide();
        }
        else {
            Show();
        }
        Show();

    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
