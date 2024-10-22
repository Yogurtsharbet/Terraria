using UnityEngine;
using UnityEngine.UI;

public class ColorSliderGradient : MonoBehaviour {
    private int width;
    private int height;
    private float step;
    private Slider slider;
    private Texture2D texture;
    RawImage rawImage;
    private Color32[] pixels;
    public HSVtype type;


    private void Awake() {
        width = (int)transform.parent.GetComponent<RectTransform>().rect.width;
        height = (int)transform.parent.GetComponent<RectTransform>().rect.height;
        transform.parent.parent.TryGetComponent(out slider);
        TryGetComponent(out rawImage);
        texture = new Texture2D(width, height);
        pixels = new Color32[width * height];
        step = 1f / (width - 1);
    }

    void Start() {
        OnSlideChanged(0, 0);
    }
    
    public void OnSlideChanged(float value1, float value2) {
        ChangeBackgroundColor(type, value1, value2);
    }

    public void ChangeBackgroundColor(HSVtype type, float value1 = 1f, float value2 = 1f) {

        for (int x = 0; x < width; x++) {
            Color color = new Color();
            if (type.Equals(HSVtype.Hue))
                color = Color.HSVToRGB(step * x, 1.0f, 1.0f);
            else if (type.Equals(HSVtype.Saturation))
                color = Color.HSVToRGB(value1, step * x, value2);
            else if (type.Equals(HSVtype.Value))
                color = Color.HSVToRGB(value1, value2, step * x);


            for (int y = 0; y < height; y++) {
                pixels[y * width + x] = color;
            }
        }
        texture.SetPixels32(pixels);
        texture.Apply();

        rawImage.texture = texture;
        rawImage.material.mainTexture = texture;
    }
}
public enum HSVtype {
    Hue,
    Saturation,
    Value
}