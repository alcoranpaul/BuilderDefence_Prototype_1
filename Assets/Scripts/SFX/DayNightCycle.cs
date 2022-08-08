using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DayNightCycle : MonoBehaviour {
    [SerializeField] private Gradient gradient;
    private Light2D light2D;
    private float dayTime, dayTimeSpeed, secondsPerDay = 50f;
    private void Awake() {
        light2D = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondsPerDay;
    }

    private void Update() {
        dayTime += Time.deltaTime * dayTimeSpeed;
        light2D.color = gradient.Evaluate(dayTime % 1f);
    }

}
