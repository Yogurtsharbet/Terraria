using UnityEngine;

public class BackgroundControl : MonoBehaviour {
    [SerializeField] private BackgroundManager bg;
    private PlayerMove playerMove;

    private void Awake() {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out playerMove);
    }

    public void moveBackground(Collider2D collision, bool isEnter) {
        if (collision.gameObject.CompareTag("Player")) {
            if (transform.position.x > collision.transform.position.x) {    // ÁÂÃø

                if (isEnter && bg.backgrounds[2].Equals(gameObject)) {  
                    playerMove.backgroundPosition++;
                    bg.backgrounds[0].transform.position = bg.backgrounds[2].transform.position + new Vector3(92f, 0, 0);
                }
                else if(!isEnter && !bg.backgrounds[2].Equals(gameObject)) { 
                    playerMove.backgroundPosition--;
                    bg.backgrounds[2].transform.position = bg.backgrounds[0].transform.position - new Vector3(92f, 0, 0);
                }
            }

            else if (transform.position.x < collision.transform.position.x) {  // ¿ìÃø
                if (isEnter && bg.backgrounds[0].Equals(gameObject)) {
                    playerMove.backgroundPosition--;
                    bg.backgrounds[2].transform.position = bg.backgrounds[0].transform.position - new Vector3(92f, 0, 0);
                }
                else if (!isEnter && !bg.backgrounds[0].Equals(gameObject)) {  
                    playerMove.backgroundPosition++;
                    bg.backgrounds[0].transform.position = bg.backgrounds[2].transform.position + new Vector3(92f, 0, 0);
                }
            }

            bg.sortBackgrounds();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        moveBackground(collision, true);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        moveBackground(collision, false);
    }
}