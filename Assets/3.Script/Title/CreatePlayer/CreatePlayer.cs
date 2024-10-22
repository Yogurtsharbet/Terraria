using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveData {
    public string playerName;
    public string[] playerPartsName;
    public Color[] playerPartsColor;
    public int playerType;
    public int playerMaxHP, playerMaxMP;
    public int playerHP, playerMP;
}

public class CreatePlayer : MonoBehaviour {
    [SerializeField] private Text playerName;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject panelPlayerOriginal;
    [SerializeField] private GameObject[] playerParts;
    [SerializeField] private int playerType;

    private string filePath, folderPath;
    private int notExistedIndex = 0;
    private int count;

    public GameObject[] savedPlayers = new GameObject[3];
    public GameObject[] playerPanels = new GameObject[3];

    public void SetPlayerType(int type) {
        playerType = type;
    }

    private void Start() {
        folderPath = Path.Combine(Application.persistentDataPath + "/Save/Player/");
        filePath = Path.Combine(folderPath, $"Player{notExistedIndex}.json");
    }

    public void DeletePlayer(int index) {
        filePath = Path.Combine(folderPath, $"Player{index}.json");
        File.Delete(filePath);
    }

    public void LoadPlayer() {
        RectTransform rect;
        notExistedIndex = 0; count = 0;
        for (int i = 0; i < savedPlayers.Length; i++) {
            filePath = Path.Combine(folderPath, $"Player{i}.json");
            if (File.Exists(filePath)) {
                SaveData savedata = new SaveData();
                string jsondata = File.ReadAllText(filePath);
                savedata = JsonUtility.FromJson<SaveData>(jsondata);
                count++;
                if (savedPlayers[i] == null) {
                    savedPlayers[i] = Instantiate(player);

                    playerPanels[i] = Instantiate(panelPlayerOriginal);
                    playerPanels[i].transform.SetParent(panelPlayerOriginal.transform.parent.transform);
                    playerPanels[i].transform.Find("PanelName").GetComponent<Text>().text = savedata.playerName;
                    playerPanels[i].transform.Find("PanelType").GetComponentInChildren<Text>().text =
                        savedata.playerType == 0 ? "클래식" : "하드코어";
                    Debug.Log($"난도 : {playerType}");
                }

                RectTransform originalRect = panelPlayerOriginal.GetComponent<RectTransform>();
                rect = playerPanels[i].GetComponent<RectTransform>();

                rect.pivot = originalRect.pivot;
                rect.anchorMin = originalRect.anchorMin;
                rect.anchorMax = originalRect.anchorMax;
                rect.sizeDelta = originalRect.sizeDelta;
                rect.offsetMin = originalRect.offsetMin;
                rect.offsetMax = originalRect.offsetMax;
                rect.anchoredPosition = originalRect.anchoredPosition;
                rect.localScale = originalRect.localScale;
                rect.localPosition = new Vector3(0, (count - 1) * -223 + 223, 0);
                playerPanels[i].SetActive(true);

                PlayerStatus playerStatus = savedPlayers[i].GetComponent<PlayerStatus>();
                PlayerPartsColor playerParts = savedPlayers[i].GetComponent<PlayerPartsColor>();
                playerStatus.PlayerName = savedata.playerName;
                playerStatus.PlayerType = savedata.playerType;

                for (int j = 0; j < playerParts.parts.Length; j++) {
                    // TODO : playerParts의 name을 기준으로 parts Sprite 바꿔주는 코드가 필요함
                    playerParts.parts[j].color = savedata.playerPartsColor[j];
                }

                savedPlayers[i].transform.SetParent(
                    playerPanels[i].transform.Find("PanelCharacter"));
                rect = savedPlayers[i].GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(1, 1);
                rect.anchoredPosition = Vector2.zero;
                rect.localScale = new Vector3(30f, 30f, 30f);
                rect.localPosition = new Vector3(0, 0, 0);
                rect.offsetMin = new Vector2(0, 10);
                rect.offsetMax = new Vector2(0, 0);

                savedPlayers[i].SetActive(true);
            }

            else {
                Destroy(savedPlayers[i]);
                Destroy(playerPanels[i]);
                // TODO : DESTROY 할 때, 배열을 삭제하기 때문에 제대로 INDEXING 되지 않는 오류
                // TODO : Array 를 LIST 로 바꾸고, Remove(index) 형태로 써야 제대로 작동할 것임
                if (notExistedIndex == 0) notExistedIndex = i;
            }
        }
        int newFileNumber = 0;
        DirectoryInfo directory = new DirectoryInfo(folderPath);
        foreach (FileInfo each in directory.GetFiles()) {
            filePath = Path.Combine(folderPath, $"Player{newFileNumber}.json");
            each.MoveTo(filePath); newFileNumber++;
        }
        foreach (FileInfo each in directory.GetFiles()) { Debug.Log(each.Name); }
    }

    public void SavePlayer() {
        for (int i = 0; i < savedPlayers.Length; i++) {
            filePath = Path.Combine(folderPath, $"Player{i}.json");
            if (!File.Exists(filePath)) {
                break;
            }
        }

        SaveData savedata = new SaveData();
        savedata.playerName = playerName.text;
        savedata.playerType = playerType;
        savedata.playerMaxHP = savedata.playerHP = 100;
        savedata.playerMaxMP = savedata.playerMP = 20;

        savedata.playerPartsColor = new Color[playerParts.Length];
        savedata.playerPartsName = new string[playerParts.Length];
        for (int i = 0; i < playerParts.Length; i++) {
            savedata.playerPartsName[i] = playerParts[i].GetComponent<SpriteRenderer>().sprite.name;
            savedata.playerPartsColor[i] = playerParts[i].GetComponent<SpriteRenderer>().color;
        }

        if (!Directory.Exists(folderPath)) 
            Directory.CreateDirectory(folderPath);

        string json = JsonUtility.ToJson(savedata, true);
        File.WriteAllText(filePath, json);
    }
}