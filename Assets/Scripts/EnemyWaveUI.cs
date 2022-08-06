using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour {

    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveText, waveMessage;
    private RectTransform enemySpawnIndicator, enemyIndicator;
    private Camera m_camera;
    private void Awake() {
        waveText = transform.Find("waveNumber_Text").GetComponent<TextMeshProUGUI>();
        waveMessage = transform.Find("waveMessage_Text").GetComponent<TextMeshProUGUI>();
        enemySpawnIndicator = transform.Find("enemyWaveSpawnPositionIndicatior").GetComponent<RectTransform>();
        enemyIndicator = transform.Find("enemyIndicator").GetComponent<RectTransform>();

    }

    private void Start() {
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        string message = string.Format("Wave: {0}", enemyWaveManager.GetWaveNumber());
        m_camera = Camera.main;
        SetWaveNumberText(message);
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e) {
        string message = string.Format("Wave: {0}", enemyWaveManager.GetWaveNumber());
        SetWaveNumberText(message);
    }

    private void Update() {
        HandleUI();
        HandleNextWaveIndicator();
        HandleClosestEnemyIndicator();
    }

    private void HandleUI() {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f) {//Currently Spawning
            SetMessageText("");
        }
        else {
            string message = string.Format("Next wave in {0}s", nextWaveSpawnTimer.ToString("F1"));
            SetMessageText(message);
        }
    }

    private void HandleNextWaveIndicator() {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - m_camera.transform.position).normalized;
        enemySpawnIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemySpawnIndicator.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), m_camera.transform.position);
        enemySpawnIndicator.gameObject.SetActive(distanceToNextSpawnPosition > m_camera.orthographicSize * 1.5f);
    }

    private void HandleClosestEnemyIndicator() {
        float targetMaxRadius = 9999f;

        Collider2D[] collider2dArray = Physics2D.OverlapCircleAll(m_camera.transform.position, targetMaxRadius);
        Enemy enemyTarget = null;
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

        if (enemyTarget != null) {
            Vector3 dirToNearestEnemy = (enemyTarget.transform.position - m_camera.transform.position).normalized;
            enemyIndicator.anchoredPosition = dirToNearestEnemy * 250f;
            enemyIndicator.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVector(dirToNearestEnemy));

            float distanceToNearestEnemy = Vector3.Distance(enemyTarget.transform.position, m_camera.transform.position);
            enemyIndicator.gameObject.SetActive(distanceToNearestEnemy > m_camera.orthographicSize * 1.5f);
        }
        else {
            enemyIndicator.gameObject.SetActive(false);
        }
    }
    private void SetMessageText(string text) {
        waveMessage.SetText(text);
    }

    private void SetWaveNumberText(string text) {
        waveText.SetText(text);
    }

}
