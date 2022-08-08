using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    public static GameOverUI Instance { get; private set; }
    private void Awake() {
        Instance = this;
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.Game);
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
        transform.Find("waveSurvived_Text").GetComponent<TextMeshProUGUI>()
            .SetText($"You survived {EnemyWaveManager.Instance.GetWaveNumber()} waves!");
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
