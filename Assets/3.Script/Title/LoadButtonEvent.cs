using UnityEngine;

public class LoadButtonEvent : MonoBehaviour {
    [SerializeField] private CreatePlayer createPlayer;
    [SerializeField] private SceneControl sceneControl;

    public void onLoadButtonClicked(int button) {
        int savedIndex;
        if (transform.parent.localPosition.y > 0) savedIndex = 0;
        else if (transform.parent.localPosition.y < 0) savedIndex = 2;
        else savedIndex = 1;

        switch (button) {
            case 0:
                // TODO : �� ����. ���� ����
                sceneControl.selectedSave = savedIndex;
                sceneControl.RunGame();
                break;
            case 1:
                // TODO : Favorite bool ������ ����
                break;
            case 2:
                break;
            case 3:
                createPlayer.DeletePlayer(savedIndex);
                createPlayer.LoadPlayer();
                break;
        }
    }
}
