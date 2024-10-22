using UnityEngine;

public class BackgroundUnderControl : MonoBehaviour {
    [SerializeField] private BackgroundUnderControl backA, backB;
    [SerializeField] private Transform playerTransform;

    public int positionIndex;               // 2 3
    private int direction = -1;             // 0 1 SORT INDEX
    private float offsetX, offsetY;
    private void Awake() {
        offsetX = transform.position.x;
        offsetY = transform.position.y;
    }

    private void Update() {
        if (playerTransform.position.y > 120) {
            transform.position = new Vector3(playerTransform.position.x + offsetX, offsetY, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.position.x > transform.position.x)
            backA.transform.position = new Vector3(transform.position.x + 400, transform.position.y, 0);
        else
            backA.transform.position = new Vector3(transform.position.x - 400, transform.position.y, 0);

        if (collision.transform.position.y > transform.position.y && transform.position.y + 120 < 0)
            backB.transform.position = new Vector3(transform.position.x, transform.position.y + 240, 0);
        else
            backB.transform.position = new Vector3(transform.position.x, transform.position.y - 240, 0);
    }
}
