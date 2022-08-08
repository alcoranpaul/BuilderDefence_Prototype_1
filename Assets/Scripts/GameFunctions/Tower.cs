using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private float shootTimerMax;

    private Enemy enemyTarget;
    private float lookForTargetTimer, shootTimer, lookForTargetTimerMax = .2f;
    private Vector3 projectileSpawnPosition;
    private void Awake() {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
        shootTimer = shootTimerMax;
    }

    private void Update() {
        HandleTargetSearch();
        HandleShooting();
    }

    private void HandleShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f) {
            shootTimer += shootTimerMax;
            if (enemyTarget != null) {
                ArrowProjectile.Create(projectileSpawnPosition, enemyTarget);
            }
        }

    }

    private void LookForTargets() {
        float targetMaxRadius = 20f;

        Collider2D[] collider2dArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider in collider2dArray) {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null) {
                if (enemyTarget == null) { //No target
                    enemyTarget = enemy;
                }
                else { //Has target
                    //If there are nearby buildings
                    if (Vector3.Distance(transform.position, enemyTarget.transform.position) >
                        Vector3.Distance(transform.position, enemy.transform.position)) {
                        enemyTarget = enemy;
                    }
                }
            }
        }
    }

    private void HandleTargetSearch() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }
}
