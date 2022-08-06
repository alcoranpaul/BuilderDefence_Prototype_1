using System;
using UnityEngine;
public class HealthSystem : MonoBehaviour {

    public event EventHandler OnDamage;
    public event EventHandler OnDied;

    [SerializeField] private int healthMax;
    private int health;

    private void Awake() {
        health = healthMax;
    }

    public void Damage(int damageAmount) {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, healthMax);

        OnDamage?.Invoke(this, EventArgs.Empty);

        if (IsDead()) {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetHealthAmountMax(int healthAmount, bool updateHealthAmount = false) {
        this.healthMax = healthAmount;
        if (updateHealthAmount) {
            this.health = healthAmount;
        }
    }

    public bool IsDead() {
        return health == 0;
    }
    public bool IsFullHealth() {
        return health == healthMax;
    }
    public int GetHealthAmount() {
        return health;
    }


    public float GetHealthNormalized() {
        return (float)health / healthMax;
    }
}
