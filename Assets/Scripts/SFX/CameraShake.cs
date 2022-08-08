using Cinemachine;
using UnityEngine;
public class CameraShake : MonoBehaviour {

    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    private float timer, timerMax, intensity;
    private void Awake() {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlinNoise.m_AmplitudeGain = 0f;
    }

    private void Update() {
        if (timer < timerMax) {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(intensity, 0f, timer / timerMax);
            perlinNoise.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intensity, float timerMax) {
        this.timerMax = timerMax;
        this.timer = 0f;
        this.intensity = intensity;
        perlinNoise.m_AmplitudeGain = intensity;
    }
}
