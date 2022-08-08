using UnityEngine;

public class GameAssets : MonoBehaviour {
    public static GameAssets instance;
    public static GameAssets Instance {
        get {
            if (instance == null) {
                instance = Resources.Load<GameAssets>("GameAssets");
            }
            return instance;
        }
    }

    public Transform pfEnemy,
        pfEnemyDieParticles,
        pfArrowProjectile,
        pfBuildingDestroyedParticles,
        pfBuildingConstruction,
        pfBuildingPlacedParticles;
}
