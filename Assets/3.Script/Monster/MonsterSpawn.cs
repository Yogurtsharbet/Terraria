using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterType {
    public string typeName;
    public GameObject[] typeMonster;
}

public class MonsterSpawn : MonoBehaviour {
    [SerializeField] private MonsterType[] MonsterPrefeb;
    public Transform playerTransform;


    private void Start() {
        StartCoroutine("Spawn_Slime", 30f);
    }

    IEnumerator Spawn_Slime(float spawnTime) {
        List<GameObject> SpawnedMonster = new List<GameObject>();

        while (true) {
            GameObject[] targetPrefebs = GetMonsterType("Slime");
            int direction = (Random.Range(0, 2) * 2 - 1);
            Vector3 spawnPosition = playerTransform.position +
                (Vector3.right * direction * 70) +
                (Vector3.right * direction * Random.Range(0, 20));

            bool isSpawned = false;
            foreach (GameObject spawned in SpawnedMonster) {
                if (!spawned.activeSelf) {
                    spawned.transform.position = spawnPosition;
                    spawned.SetActive(true);
                    isSpawned = true;
                    break;
                }
            }
            if (!isSpawned && SpawnedMonster.Count < 10) {
                SpawnedMonster.Add(
                    Instantiate(
                    targetPrefebs[Random.Range(0, targetPrefebs.Length)],
                    spawnPosition, Quaternion.identity)
                );
                SpawnedMonster[SpawnedMonster.Count - 1].transform.SetParent(transform);
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public GameObject[] GetMonsterType(string typeName) {
        foreach(MonsterType target in MonsterPrefeb) 
            if (target.typeName.Equals(typeName)) 
                return target.typeMonster;
        
        return null;
    }
}
