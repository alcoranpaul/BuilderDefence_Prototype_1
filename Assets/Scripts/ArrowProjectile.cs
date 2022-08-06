using UnityEngine;

public class ArrowProjectile : MonoBehaviour {
    private Enemy enemy;
    private float moveSpeed = 20f;
    private float timeToDie = 2f;
    private Vector3 lastMoveDirection;

    public static ArrowProjectile Create(Vector3 position, Enemy enemy) {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
        Transform enemyTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrow = enemyTransform.GetComponent<ArrowProjectile>();
        arrow.SetTarget(enemy);
        return arrow;

    }
    private void Update() {
        HandleMovement();
        HandleSpawnTime();
    }

    private void HandleMovement() {
        Vector3 moveDirection;

        if (enemy != null) {
            moveDirection = (enemy.transform.position - transform.position).normalized;
            lastMoveDirection = moveDirection;
        }
        else {
            moveDirection = lastMoveDirection;
        }
        transform.position += moveSpeed * Time.deltaTime * moveDirection;
        transform.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVector(moveDirection));
    }

    private void HandleSpawnTime() {
        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0f) {
            Destroy(gameObject);
        }
    }
    private void SetTarget(Enemy enemy) {
        this.enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
