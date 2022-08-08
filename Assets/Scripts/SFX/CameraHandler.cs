using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public static CameraHandler Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float x, y, orthographicSize, targetOrthographicSize;
    private bool edgeScrolling;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    void Update() {
        MovementHandler();
        LensZoomHandler();

    }

    private void MovementHandler() {
        float moveSpeed = 20f;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (edgeScrolling) {
            float edgeScrollingSize = 30;
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize) {
                x = 1f;
            }
            else if (Input.mousePosition.x < edgeScrollingSize) {
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize) {
                y = 1f;
            }
            else if (Input.mousePosition.y < edgeScrollingSize) {
                y = -1f;
            }
        }

        Vector3 moveDir = new Vector3(x, y).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }

    private void LensZoomHandler() {
        float zoomAmount = 2f;
        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;
        float zoomSpeed = 5f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    public void SetEdgeScrolling(bool edgeScrolling) {
        this.edgeScrolling = edgeScrolling;
    }

    public bool GetEdgeScrolling() {
        return edgeScrolling;
    }
}
