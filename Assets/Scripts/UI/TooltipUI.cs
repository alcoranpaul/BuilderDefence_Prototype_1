using TMPro;
using UnityEngine;
public class TooltipUI : MonoBehaviour {
    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;
    private TextMeshProUGUI text;
    private RectTransform background;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;

    private void Awake() {
        Instance = this;
        text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        background = transform.Find("background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        SetText("Hello World!");
        Hide();
    }

    private void Update() {
        GetMousePosition();
        TooltipFade();
    }

    private void SetText(string tooltipText) {
        text.SetText(tooltipText);
        text.ForceMeshUpdate();

        Vector2 textSize = text.GetRenderedValues(false);
        Vector2 padding = new(8, 8); //Double size of text position
        background.sizeDelta = textSize + padding;
    }

    private void TooltipFade() {
        if (tooltipTimer != null) {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0) {
                Hide();
            }
        }
    }

    private void GetMousePosition() {
        // Using the Canva's scale we can calculate the accurate posistion of the mouse
        // local scale is uniformed
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        float background_Width = background.rect.width;
        float background_Height = background.rect.height;
        float canvas_Width = canvasRectTransform.rect.width;
        float canvas_Height = canvasRectTransform.rect.height;


        if (anchoredPosition.x + background_Width > canvas_Width) { // If tooltip goes beyond Width length
            anchoredPosition.x = canvas_Width - background_Width;
        }
        else if (anchoredPosition.y + background_Height > canvas_Height) { // If tooltip goes beyond height length
            anchoredPosition.y = canvas_Height - background_Height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public void Show(string toolTipText, TooltipTimer tooltipTimer = null) {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(toolTipText);
        GetMousePosition();
    }
    public void Hide() {
        gameObject.SetActive(false);
    }

    public class TooltipTimer {
        public float timer;

        public TooltipTimer(float timer) {
            this.timer = timer;
        }
    }
}
