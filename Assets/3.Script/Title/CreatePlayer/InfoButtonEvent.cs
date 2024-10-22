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
            discription.text = "Ŭ���� ���̵��� ĳ���ʹ� ������ �������� ����Ʈ���ϴ�.";
        }
        else if (button.Equals(1)) {
            isSelected = 1;
            discription.text = "�ϵ��ھ� ���̵��� ĳ���ʹ� �� �׽��ϴ�.";
        }
        createPlayer.SetPlayerType(button);
    }
}
