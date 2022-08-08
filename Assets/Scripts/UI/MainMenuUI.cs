using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour {

    private RectTransform transitionImage;
    private void Awake() {
        transitionImage = transform.Find("transitionUI").GetComponent<RectTransform>();

        transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(() => {
            transitionImage.gameObject.SetActive(true);
            LeanTween.scale(transitionImage, Vector3.zero, 0);
            LeanTween.scale(transitionImage, Vector3.one, 2f).setEase(LeanTweenType.easeInExpo).setOnComplete(() => {
                transitionImage.gameObject.SetActive(false);
                LoadGame();
            });

        });
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {
            transitionImage.gameObject.SetActive(true);
            LeanTween.scale(transitionImage, Vector3.zero, 0);
            LeanTween.scale(transitionImage, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutExpo).setOnComplete(() => {
                transitionImage.gameObject.SetActive(false);
                Application.Quit();
            });

        });

    }

    private void Start() {
        transitionImage.gameObject.SetActive(true);
        LeanTween.scale(transitionImage, new Vector3(1, 1, 1), 0);
        LeanTween.scale(transitionImage, Vector3.zero, 1.5f).setEase(LeanTweenType.easeOutCirc).setOnComplete(() => {
            transitionImage.gameObject.SetActive(false);
        });
    }


    private void LoadGame() {
        GameSceneManager.Load(GameSceneManager.Scene.Game);
    }

}
