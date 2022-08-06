using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;

    private void Awake() {
        barTransform = transform.Find("bar");
    }

    private void Start() {
        healthSystem.OnDamage += HealthSystem_OnDamage;
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

    private void UpdateBarVisibile() {
        if (healthSystem.IsFullHealth()) {
            Hide();
        }
        else {
            Show();
        }

    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
