using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerEquipmentAnimation : MonoBehaviour {
    [SerializeField] private Animator playerAnimator;
    
    private SpriteRenderer sprite;
    AnimatorStateInfo currentAnimation;
    private void Awake() {
        transform.parent.TryGetComponent(out playerAnimator);
        sprite = transform.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable() {
    }

    private void Update() {
        currentAnimation = playerAnimator.GetCurrentAnimatorStateInfo(1);
        if (currentAnimation.IsName("PlayerDigging")) {
            sprite.transform.gameObject.SetActive(true);
            AnimationClip clip = playerAnimator.runtimeAnimatorController.animationClips[3];
            int totalFrame = Mathf.RoundToInt(clip.frameRate * currentAnimation.length);
            int currentFrame = Mathf.RoundToInt(totalFrame * (currentAnimation.normalizedTime % 1f));

            sprite.transform.localRotation = Quaternion.Euler(0, 0, sprite.flipX ? 310 : 70);
            transform.GetComponent<RectTransform>().localPosition = new Vector2(sprite.flipX ? 0.55f : -0.34f, sprite.flipX ? -0.13f : -0.23f);
            transform.localRotation = Quaternion.Euler(0, 0, (sprite.flipX ? 300 : 0) + 180f / totalFrame * currentFrame * (sprite.flipX ? 1 : -1));

        }
        else {
            sprite.transform.gameObject.SetActive(false);
        }

    }
}