using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMPbarControl : MonoBehaviour {
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private Transform[] barFill;
    [SerializeField] private bool isHP;

    private void Awake() {
        List<Transform> childObjects = new List<Transform>();

        for(int i = 0; i < transform.childCount; i++) 
            childObjects.Add(transform.GetChild(i).GetChild(0).transform);
        barFill = childObjects.ToArray();
    }

    private void Update() {
        barFillControl(isHP);
    }

    public void barFillControl(bool isHP) {
        float targetFillMax = 0, targetFill = 0;
        if (isHP) {
            targetFillMax = playerStatus.PlayerMaxHP;
            targetFill = playerStatus.PlayerHP;
        }
        else {
            targetFillMax = playerStatus.PlayerMaxMP;
            targetFill = playerStatus.PlayerMP;
        }

        float cellLevel = targetFillMax / barFill.Length;
        for (int i = 0; i < barFill.Length; i++) {
            float cellOffset = i * cellLevel;
            float cellHP_normalize = Mathf.InverseLerp(cellOffset, cellOffset + cellLevel, targetFill);
            float cellFillScale = Mathf.Floor(cellHP_normalize * 5) / 5.0f;

            barFill[i].localScale = new Vector3(cellFillScale, cellFillScale, 1);
        }
    }
}
