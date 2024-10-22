using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBehavior : MonoBehaviour {
    [SerializeField] private LayerMask TargetPlatform;
    [SerializeField] AudioClip[] audioClips;

    private PlayerStatus playerStatus;
    private Rigidbody2D playerRigid;
    private Animator playerAnimator;
    private AudioSource playerAudio;

    private float CoolTimeDig = 0.3f;
    private bool isCoolTime = false;
    Vector3 MousePosition;

    private void Awake() {
        TryGetComponent(out playerAnimator);
        TryGetComponent(out playerStatus);
        TryGetComponent(out playerRigid);
        TryGetComponent(out playerAudio);
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            if (!isCoolTime) {
                StartCoroutine("CalculateCoolTime", CoolTimeDig);

                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // if (Auto Target) MousePosition = ±Ÿ¡¢«— ≈∏¿œ

                // if (¿Â¬¯ ¿Â∫Ò == ∞Ó±™¿Ã)

                if (!playerAnimator.GetBool("isDig"))
                    playerAnimator.SetTrigger("isDigTrigger");
                playerAnimator.SetBool("isDig", true);
                playerAudio.clip = audioClips[3];
                DigTile(MousePosition);
                playerAudio.Play();
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            playerAnimator.SetBool("isDig", false);
            playerAnimator.ResetTrigger("isDigTrigger");

        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Monster")) {
            playerStatus.TakeDamage(collision.gameObject.GetComponent<MonsterStatus>().MonsterAttack);
            Vector2 direction = collision.gameObject.GetComponent<SpriteRenderer>().flipX ?
                Vector2.left : Vector2.right;
            playerRigid.velocity = Vector2.zero;
            playerRigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            playerRigid.AddForce(direction * 7, ForceMode2D.Impulse);

        }
    }


    private IEnumerator CalculateCoolTime (float cooltime) {
        isCoolTime = true;
        yield return new WaitForSeconds(cooltime) ;
        isCoolTime = false;
    }


    public void DigTile(Vector3 TargetPosition) {
        TargetPosition.z = 0;
        if (Vector3.Distance(transform.position, TargetPosition) > 10) return;

        Collider2D collider = Physics2D.OverlapCircle(TargetPosition, 0.01f, TargetPlatform);

        if (collider != null) {
            Tilemap tilemap = collider.transform.GetComponent<Tilemap>();
            Vector3Int cellPosition = tilemap.WorldToCell(TargetPosition);

            if (tilemap.GetTile(cellPosition).name[6].Equals('2'))
                playerAudio.clip = audioClips[4];
            else playerAudio.clip = audioClips[Random.Range(0, 3)];
            tilemap.SetTile(cellPosition, null);
        }
    }
}