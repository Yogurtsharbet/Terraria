using System.Linq.Expressions;
using UnityEngine;

public class TitleLogoAnimation : MonoBehaviour {
    private float MinSize = 1.2f, MaxSize = 1.9f;
    private float MaxRotate = 10f;
    private float scaleDirection, rotateDirection;

    private void Awake() {      // set scale speed
        scaleDirection = 0.05f;
        rotateDirection = 1.2f;
    }
    private void Update() {
        transform.localScale = new Vector3(
            transform.localScale.x + scaleDirection * Time.deltaTime,
            transform.localScale.y + scaleDirection * Time.deltaTime,
            0);
        float newZRotation = transform.eulerAngles.z + rotateDirection * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, newZRotation);

        if ((transform.localScale.x < MinSize && scaleDirection < 0) || 
            (transform.localScale.x > MaxSize && scaleDirection > 0))
            scaleDirection = -scaleDirection;
        if ((transform.eulerAngles.z < 360 - MaxRotate && transform.eulerAngles.z > 180 && rotateDirection < 0) ||
            (transform.eulerAngles.z > MaxRotate && transform.eulerAngles.z < 180 && rotateDirection > 0))
            rotateDirection = -rotateDirection;
    }
}

