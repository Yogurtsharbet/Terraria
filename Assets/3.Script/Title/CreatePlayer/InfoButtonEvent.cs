using UnityEngine;
using UnityEngine.UI;

public class InfoButtonEvent : MonoBehaviour {
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text discription;
    [SerializeField] private CreatePlayer createPlayer;
    public int isSelected = 0;

    private void OnEnable() {
        buttons[0].Select();
        onInfoButtonClicked(0);
    }

    public void onInfoButtonClicked(int button) {
        if (button.Equals(0)) {
            isSelected = 0;
            discription.text = "클래식 난이도의 캐릭터는 죽으면 소지금을 떨어트립니다.";
        }
        else if (button.Equals(1)) {
            isSelected = 1;
            discription.text = "하드코어 난이도의 캐릭터는 잘 죽습니다.";
        }
        createPlayer.SetPlayerType(button);
    }
}
