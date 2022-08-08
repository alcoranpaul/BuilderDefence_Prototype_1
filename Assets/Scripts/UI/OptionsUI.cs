using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    private TextMeshProUGUI soundVolumeText, musicVolumeText;

    private void Awake() {
        soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();

        transform.Find("soundInceaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.IncreaseVolume();
            UpdateText();
        });
        transform.Find("soundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.DecreaseVolume();
            UpdateText();
        });
        transform.Find("musicInceaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            MusicManager.Instance.IncreaseVolume();
            UpdateText();
        });
        transform.Find("musicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            MusicManager.Instance.DecreaseVolume();
            UpdateText();
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        transform.Find("edgeScrolling").GetComponent<Toggle>().onValueChanged.AddListener((bool set) => {
            CameraHandler.Instance.SetEdgeScrolling(set);
            Debug.Log($"OnValueChanged: {set}");
        });


    }

    private void Start() {
        UpdateText();
        gameObject.SetActive(false);
        transform.Find("edgeScrolling").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
    }



    private void UpdateText() {
        soundVolumeText.SetText(Mathf.RoundToInt(SoundManager.Instance.GetVolume() * 10).ToString());
        musicVolumeText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10).ToString());
    }

    public void ToggleVisible() {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf) {
            Time.timeScale = 0f; //Pause game
        }
        else {
            Time.timeScale = 1f;
        }
    }

}
