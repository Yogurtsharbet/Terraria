using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightControl : MonoBehaviour {
    [SerializeField] private Light2D AmbientLight;
    [SerializeField] private Camera CameraBackground;
    [SerializeField] GameObject Sun, Moon;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AnimationCurve Intensity;
    [SerializeField] private float timeSpeed = 24f, ClampY = 130f;


    private bool isNight = false, isToggled = true;
    float time, timeRate;
    private void Awake() {
        AmbientLight = GetComponent<Light2D>();
    }

    private void Start() {
        timeRate = 1.0f / timeSpeed;
        time = 0;
    }

    private void Update() {
        time = (time + timeRate * Time.deltaTime) % 1.0f;
        float yPos = Mathf.PingPong(time, 1);
        yPos = 2 * yPos * (1 - yPos);
        
        float intensity = Intensity.Evaluate(time);
        AmbientLight.color = Color.HSVToRGB(0, 0, 
            Mathf.Clamp(intensity * (isNight ? 0.2f : 1f), 0.1f, 1));
        CameraBackground.backgroundColor = Color.HSVToRGB(220f / 360f, 70f / 100f,
                        intensity * (isNight ? -1f : 1f));

        GameObject planet = Sun;
        if (isNight) planet = Moon;
        planet.SetActive(true);

        planet.transform.position = new Vector3(
            playerTransform.position.x + 120f * time - 65f,
            Mathf.Clamp(playerTransform.position.y + 60f * yPos + 5f, ClampY, float.MaxValue), 0);

        if (time > 0.5 && time < 0.6) isToggled = false;

        if (time > 0.99 && !isToggled) {
            isToggled = true;
            planet.SetActive(false);
            isNight = !isNight;
        }
    }
}
