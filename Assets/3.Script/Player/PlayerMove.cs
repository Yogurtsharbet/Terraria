using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [SerializeField] private float PlayerMoveSpeed = 1f, playerJumpSpeed = 20f;
    public int backgroundPosition = 0;

    private Rigidbody2D playerRigid;
    private Animator playerAnimator;
    [SerializeField] private SpriteRenderer[] playerPartsSprite;
    private bool allowJump = true;

    private void Awake() {
        TryGetComponent(out playerRigid);
        TryGetComponent(out playerAnimator);

    }
    private void Update() {
        if (!allowJump) playerAnimator.ResetTrigger("isLandTrigger");
        float posX = Input.GetAxis("Horizontal");
        if (posX != 0 && !playerAnimator.GetBool("isMove")) {
            playerAnimator.SetBool("isMove", true);
            if (posX < 0) 
                foreach (SpriteRenderer partSprite in playerPartsSprite)
                    partSprite.flipX = true;
            
            else 
                foreach (SpriteRenderer partSprite in playerPartsSprite)
                    partSprite.flipX = false;
        }
        else if (posX == 0) playerAnimator.SetBool("isMove", false);
         
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && allowJump) {
            playerRigid.AddForce(Vector2.up * playerJumpSpeed, ForceMode2D.Impulse);
            playerAnimator.SetTrigger("isJump");
        }
        if((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) && playerRigid.velocity.y > 0) {
            playerRigid.velocity *= 0.6f;
        }

        transform.Translate(posX * Time.deltaTime * PlayerMoveSpeed * 8f, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            playerAnimator.SetBool("isLand", true);
            playerAnimator.SetTrigger("isLandTrigger");
            allowJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            playerAnimator.SetBool("isLand", false);
            allowJump = false;

        }
    }
}
