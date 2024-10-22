using UnityEngine;
using UnityEngine.UI;

public class GameMenuControl : MonoBehaviour {
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject[] Buttons;
    [SerializeField] AudioClip audioOpen, audioClose;
    private AudioSource menuAudio;

    private ButtonLabel checkExit;

    private void Awake() {
        TryGetComponent(out menuAudio);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            for(int i = 1; i < Buttons.Length;i++) 
                Buttons[i].SetActive(false);
            Buttons[(int)ButtonLabel.GameMain].
                SetActive(!Buttons[(int)ButtonLabel.GameMain].activeSelf);

            MenuPanel.SetActive(false);
            Buttons[3].GetComponent<Button>().Select();
        }
    }

    public void onButton_GameMenu_Clicked() {
        MenuPanel.SetActive(true);
        for (int i = (int)ButtonLabel.GameBack; i <= (int)ButtonLabel.GameExit; i++)
            Buttons[i].SetActive(true);
        menuAudio.clip = audioOpen;
        menuAudio.Play();
    }

    public void onButton_GameBack_Clicked() {
        MenuPanel.SetActive(false);
        menuAudio.clip = audioClose;
        menuAudio.Play();
    }

    public void onButton_GameTitle_Clicked() {
        menuAudio.clip = audioOpen;
        menuAudio.Play();
        checkExit = ButtonLabel.GameTitle;
        Buttons[(int)ButtonLabel.CheckInput].SetActive(true);
    }

    public void onButton_GameExit_Clicked() {
        menuAudio.clip = audioOpen;
        menuAudio.Play();
        checkExit = ButtonLabel.GameExit;
        Buttons[(int)ButtonLabel.CheckInput].SetActive(true);
    }

    public void onButton_CheckInput_Exit_Clicked() {
        menuAudio.clip = audioOpen;
        menuAudio.Play();
        if (checkExit.Equals(ButtonLabel.GameExit)) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif   
        }
        else if (checkExit.Equals(ButtonLabel.GameTitle)) {
            GameObject.Find("SceneManager").GetComponent<SceneControl>().Title();
        }
        Buttons[(int)ButtonLabel.CheckInput].SetActive(false);
    }

    public void onnButton_CheckInput_Back_Clicked() {
        menuAudio.clip = audioClose;
        menuAudio.Play();
        Buttons[(int)ButtonLabel.CheckInput].SetActive(false);
    }

}
