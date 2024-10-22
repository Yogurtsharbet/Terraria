using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
    private float ScrollSpeed = -0.6f;
    void Update() {
        transform.Translate(Time.deltaTime * ScrollSpeed, 0, 0);
        if (transform.position.x <= -56) transform.Translate(112, 0, 0);
    }
}
