using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {
    public int selectedSave = 0;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void RunGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void Title() {
        SceneManager.LoadScene("Title");
    }
}
