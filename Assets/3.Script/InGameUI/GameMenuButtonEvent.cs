using UnityEngine;

public class GameMenuButtonEvent : MonoBehaviour {
    [SerializeField] private ButtonLabel buttonLabel;
    private void OnEnable() {
    }
}

public enum ButtonLabel {
    GameMain,
    GameBack,
    GameTitle,
    GameExit,
    CheckInput
}