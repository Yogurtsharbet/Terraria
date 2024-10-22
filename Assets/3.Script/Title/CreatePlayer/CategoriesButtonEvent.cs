using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CategoriesButtonEvent : MonoBehaviour {
    [SerializeField] private GameObject PanelColor, PanelInfo;
    [SerializeField] private GameObject player;
    public Button[] buttons;
    public bool[] isSelected;
    
    private PlayerPartsColor playerColor;
    public playerPart part;

    private void Awake() {
        playerColor = player.GetComponent<PlayerPartsColor>();
        isSelected = new bool[buttons.Length];
        for (int i = 0; i < isSelected.Length; i++) isSelected[i] = false;
    }

    private void OnEnable() {
        PanelColor.SetActive(false);
        PanelInfo.SetActive(true);
        for (int i = 0; i < isSelected.Length; i++) isSelected[i] = false;
        buttons[0].Select();
        isSelected[0] = true;
    }

    public void onCategoriesButtonClicked(int category) {
        Slider[] sliders;
        float H = 0, S = 0, V = 0;
        for (int i = 0; i < isSelected.Length; i++) isSelected[i] = false;
        isSelected[category] = true;

        switch (category) {
            case 0:
                PanelInfo.SetActive(true);
                break;
            case 1:
                break;
            case 2:
                break;

            case 3: part = playerPart.partHair; break;
            case 4: part = playerPart.partEyeball; break;
            case 5: part = playerPart.partHead; break;
            case 6: part = playerPart.partArmor; break;
            case 7: part = playerPart.partArmor; break;
            case 8: part = playerPart.partLeg; break;
            case 9: part = playerPart.partShoes; break;
                
        }
        if(category != 0) PanelInfo.SetActive(false);
        if (category > 2) {
            playerColor.SelectedPart = (int)part;
            PanelColor.SetActive(true);
            Color.RGBToHSV(playerColor.parts[(int)part].color, out H, out S, out V);
            sliders = PanelColor.GetComponentsInChildren<Slider>();
            sliders[0].value = H;
            sliders[1].value = S;
            sliders[2].value = V;
        }
        else PanelColor.SetActive(false);

    }


}
