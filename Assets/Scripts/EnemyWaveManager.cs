using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {

    public event EventHandler OnWaveNumberChanged;

    private enum State {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform nextWaveSpawnPositionTransform;
    private State state;
    private float nextWaveSpawnTimer, nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount, waveNumber;
    private Vector3 spawnPosition;

    private void Start() {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer <= 0) {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0) {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer <= 0) {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.5f);
                        Enemy.Create(spawnPosition + Utilities.GetRandomDirection() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0f) {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                }
                break;
        }
    }


    private void SpawnWave() {
        remainingEnemySpawnAmount = 5 + ((int)UnityEngine.Random.Range(0f, 5f) * waveNumber);
        Debug.Log(string.Format("Enemies Spawned: {0}", remainingEnemySpawnAmount));
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetWaveNumber() {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer() {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition() {
        return spawnPosition;
    }
}
