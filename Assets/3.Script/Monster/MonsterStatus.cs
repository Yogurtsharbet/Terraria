using UnityEngine;

public class MonsterStatus : MonoBehaviour {
    [SerializeField] public float MonsterMaxHP;
    [SerializeField] public float MonsterMaxMP;
    public float MonsterAttack;
    public float MonsterHP;
    public float MonsterMP;

    private void OnEnable() {
        MonsterHP = MonsterMaxHP;
        MonsterMP = MonsterMaxMP;
    }
    public void TakeDamage(float damage) {
        MonsterHP -= damage;
        if (MonsterHP <= 0) Die();
    }

    public void Die() {
        gameObject.SetActive(false);
    }
}
