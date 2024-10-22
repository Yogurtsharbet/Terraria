using UnityEngine;
using UnityEngine.UI;

public class ColorChangeHex : MonoBehaviour {
    [SerializeField] Slider Hue, Saturation, Value;
    [SerializeField] Text text;
    [SerializeField] PlayerPartsColor playerPartsColor;

    private void OnEnable() {
        OnSlideChanged();
    }

    public void OnSlideChanged() {
        Color color = Color.HSVToRGB(Hue.normalizedValue, Saturation.normalizedValue, Value.normalizedValue);
        text.text = $"#{ColorUtility.ToHtmlStringRGB(color)}";
        playerPartsColor.parts[playerPartsColor.SelectedPart].color = color;

        Saturation.GetComponentInChildren<ColorSliderGradient>().
            OnSlideChanged(Hue.normalizedValue, Value.normalizedValue);
        Value.GetComponentInChildren<ColorSliderGradient>().
            OnSlideChanged(Hue.normalizedValue, Saturation.normalizedValue);
    }
    
    public void onRandomColor() {
        float H, S, V;
        Color color = Color.HSVToRGB(
            H = Random.Range(0f, 1f), 
            S = Random.Range(0f, 1f), 
            V = Random.Range(0f, 1f));
        text.text = $"#{ColorUtility.ToHtmlStringRGB(color)}";
        playerPartsColor.parts[playerPartsColor.SelectedPart].color = color;

        Hue.value = H;
        Saturation.value = S;
        Value.value = V;

        Saturation.GetComponentInChildren<ColorSliderGradient>().
            OnSlideChanged(H, V);
        Value.GetComponentInChildren<ColorSliderGradient>().
            OnSlideChanged(H, S);
    }
}
