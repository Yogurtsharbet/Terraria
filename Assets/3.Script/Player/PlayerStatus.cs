using System.IO;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    [SerializeField] private SceneControl sceneControl;
    private PlayerPartsColor playerPartsColor;

    public string PlayerName;
    public int PlayerType;
    public float PlayerMaxHP = 100, PlayerMaxMP = 20;
    public float PlayerHP = 100, PlayerMP = 20;
   
    private int index;

    private void Awake() {
        sceneControl = GameObject.Find("SceneManager").GetComponent<SceneControl>();
        TryGetComponent(out playerPartsColor);
        index = sceneControl.selectedSave;
    }

    private void Start() {
        SaveData loaded = new SaveData();
        string folderPath = Path.Combine(Application.persistentDataPath + "/Save/Player/");
        string filePath = Path.Combine(folderPath, $"Player{index}.json");
        string jsondata = File.ReadAllText(filePath);
        loaded = JsonUtility.FromJson<SaveData>(jsondata);

        PlayerName = loaded.playerName;
        PlayerType = loaded.playerType;
        PlayerMaxHP = loaded.playerMaxHP;
        PlayerMaxMP = loaded.playerMaxMP;
        PlayerHP = loaded.playerHP;
        PlayerMP = loaded.playerMP;
        for (int i = 0; i < loaded.playerPartsColor.Length; i++) {
            playerPartsColor.parts[i].color = loaded.playerPartsColor[i];
        }
    }

    public void TakeDamage(float damage) {
        PlayerHP -= damage;
        if (PlayerHP <= 0) Die();
    }

    public void Die() {
        gameObject.SetActive(false);
    }
}
