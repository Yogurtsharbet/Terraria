using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartsColor : MonoBehaviour {
    public SpriteRenderer[] parts;
    public int SelectedPart;
    private void Awake() {
    }
    public void ChangePartColor(playerPart part, Color color) {
        parts[(int)part].color = color;
    }
}

public enum playerPart {
    partHead,
    partEyeball,
    partEye,
    partHair,
    partArmor,
    partHandLeft,
    partHandRight,
    partLeg,
    partShoes
}