using UnityEngine;

public class Enemy : MonoBehaviour {

    public static Enemy Create(Vector3 position) {
        Transform enemyTransform = Instantiate(GameAssets.Instance.pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;

    }
    private Transform targetTransform;
    private Rigidbody2D enemyRigidBody;
    private float moveSpeed, lookForTargetTimer, lookForTargetTimerMax = .2f;
    private HealthSystem healthSystem;

    private void Start() {
        if (BuildingManager.Instance.GetHQ() != null) {
            targetTransform = BuildingManager.Instance.GetHQ().transform;
        }
        enemyRigidBody = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
        lookForTargetTimer = Random.Range(lookForTargetTimer, lookForTargetTimerMax);
        moveSpeed = 6f;
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamage += HealthSystem_OnDamage;

    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
        CameraShake.Instance.ShakeCamera(3f, 0.1f);
        ChromaticAborationEffect.Instance.SetWeight(0.5f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
        CameraShake.Instance.ShakeCamera(5f, 0.12f);
        ChromaticAborationEffect.Instance.SetWeight(0.5f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
    }

    private void Update() {
        HandleMovement();
        HandleTargetSearch();
    }

    private void HandleMovement() {
        //Get the direction to the target
        if (targetTransform != null) {
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
            enemyRigidBody.velocity = moveDirection * moveSpeed;
        }
        else {
            enemyRigidBody.velocity = Vector3.zero;

        }
    }

    private void HandleTargetSearch() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null) {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            this.healthSystem.Damage(999);
        }

    }

    private void LookForTargets() {
        float targetMaxRadius = 10f;

        Collider2D[] collider2dArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider in collider2dArray) {
            Building building = collider.GetComponent<Building>();
            if (building != null) {
                if (targetTransform == null) { //No target

                }
                else { //Has target
                    //If there are nearby buildings
                    if (Vector3.Distance(transform.position, targetTransform.position) >
                        Vector3.Distance(transform.position, building.transform.position)) {
                        targetTransform = building.transform;
                    }
                }
            }
        }
        if (targetTransform == null) {
            if (BuildingManager.Instance.GetHQ() != null) {
                targetTransform = BuildingManager.Instance.GetHQ().transform;
            }
        }
    }
}
