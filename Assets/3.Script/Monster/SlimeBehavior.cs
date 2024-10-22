using System.Collections;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] AudioClip[] audioClips;

    private SpriteRenderer monsterSprite;
    private MonsterStatus monsterStatus;
    private Rigidbody2D monsterRigid;
    private AudioSource monsterAudio;

    private float JumpDelay = 5f, SightDistance = 50f;
    Coroutine coroutine;

    private void OnEnable() {
        TryGetComponent(out monsterRigid);
        TryGetComponent(out monsterStatus);
        TryGetComponent(out monsterSprite);
        TryGetComponent(out monsterAudio);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coroutine = StartCoroutine("Jump_Slime", JumpDelay);

        monsterSprite.color = Color.white;
    }

    private void OnDisable() {
        
        StopCoroutine(coroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Weapon")) {
            if (monsterStatus.MonsterHP <= 2f) monsterAudio.clip = audioClips[2];
            else monsterAudio.clip = audioClips[1];
            monsterAudio.Play();
            monsterStatus.TakeDamage(2f);

            Vector2 direction =
                collision.GetComponent<SpriteRenderer>().flipX ? 
                Vector2.left : Vector2.right;

            SetFilpByDirection(direction);
            monsterRigid.velocity = Vector2.zero;
            StartCoroutine("Hit_Slime");
            monsterRigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            monsterRigid.AddForce(direction * 15, ForceMode2D.Impulse);
        }
    }

    public void SetFilpByDirection (Vector2 direction) {
        if (direction.Equals(Vector2.right)) monsterSprite.flipX = false;
        else if (direction.Equals(Vector2.left)) monsterSprite.flipX = true;
    }

    IEnumerator Jump_Slime(float jumpdelay) {
        while (true) {
            float diffDistance = transform.position.y - playerTransform.position.y;
            if (Mathf.Abs(diffDistance) > 50) gameObject.SetActive(false);
            diffDistance = transform.position.x - playerTransform.position.x;
            if (Mathf.Abs(diffDistance) > 250) gameObject.SetActive(false);

            Vector2 direction = Vector2.right;
            if (Mathf.Abs(diffDistance) < SightDistance) {
                if (diffDistance > 0)
                    direction = Vector2.left;
                monsterAudio.clip = audioClips[0];
                monsterAudio.Play();
            }
            else
                direction = Vector2.right * (Random.Range(0, 2) * 2 - 1);

            SetFilpByDirection(direction);
            monsterRigid.velocity = Vector2.zero;
            monsterRigid.AddForce(Vector2.up * Random.Range(10, 20), ForceMode2D.Impulse);
            monsterRigid.AddForce(direction * Random.Range(5, 15), ForceMode2D.Impulse);

            

            yield return new WaitForSeconds(jumpdelay);
        }
    }

    IEnumerator Hit_Slime() {
        monsterSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        monsterSprite.color = Color.white;
    }
}
