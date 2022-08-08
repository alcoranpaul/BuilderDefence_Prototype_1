using UnityEngine.SceneManagement;
public static class GameSceneManager {

    public enum Scene {
        Game,
        MainMenuScene
    }


    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}
