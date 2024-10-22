using UnityEngine;

public class ButtonEvent : MonoBehaviour {
    [SerializeField] private GameObject[] Menu;
    [SerializeField] private GameObject player;
    [SerializeField] AudioClip audioOpen, audioClose;

    private CreatePlayer createplayer;
    private AudioSource menuAudio;

    public void playOpenAudio() {
        menuAudio.clip = audioOpen;
        menuAudio.Play();
    }
    public void playCloseAudio() {
        menuAudio.clip = audioClose;
        menuAudio.Play();
    }

    private void Awake() {
        TryGetComponent(out menuAudio);
    }

    public void OnButtonBacktoMain() {
        foreach(var menu in Menu) 
            menu.SetActive(false);
        Menu[0].SetActive(true);
        playCloseAudio();
    }
    public void onButtonSingleClicked() {
        player.SetActive(false);
        Menu[0].SetActive(false);
        Menu[2].SetActive(false);
        Menu[1].SetActive(true);
        createplayer = GetComponentInChildren<CreatePlayer>();
        createplayer.LoadPlayer();
        playOpenAudio();
    }

    public void onButtonMultiClicked() {
        Menu[0].SetActive(false);
        Menu[3].SetActive(true);
        playOpenAudio();
    }
    public void onButtonArchiveClicked() {
        Menu[0].SetActive(false);
        Menu[4].SetActive(true);
        playOpenAudio();

    }
    public void onButtonSettingClicked() {
        Menu[0].SetActive(false);
        Menu[5].SetActive(true);
        playOpenAudio();
    }

    public void onButtonNewClicked() {
        Menu[1].SetActive(false);
        Menu[2].SetActive(true);
        player.SetActive(true);
        playOpenAudio();
    }
    public void onButtonExitClicked() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif    
    }
}
