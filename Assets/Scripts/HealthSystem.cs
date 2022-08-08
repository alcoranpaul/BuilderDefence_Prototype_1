using System;
using UnityEngine;
public class HealthSystem : MonoBehaviour {

    public event EventHandler OnHealthAmountMaxChange;
    public event EventHandler OnDamage;
    public event EventHandler OnDied;
    public event EventHandler OnHeal;

    [SerializeField] private int healthMax;
    private int health;

    private void Awake() {
        health = healthMax;
    }

    public void Heal(int healAmount) {
        health += healAmount;
        health = Mathf.Clamp(healAmount, 0, healthMax);
        OnHeal?.Invoke(this, EventArgs.Empty);
    }

    public void FullHeal() {
        health = healthMax;
        OnHeal?.Invoke(this, EventArgs.Empty);
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
        OnHealthAmountMaxChange?.Invoke(this, EventArgs.Empty);
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

    public int GetHealthAmountMax() {
        return healthMax;
    }

    public float GetHealthNormalized() {
        return (float)health / healthMax;
    }
}
