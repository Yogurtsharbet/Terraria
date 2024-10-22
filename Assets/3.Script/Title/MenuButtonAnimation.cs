using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private WaitForSeconds WaitForSeconds = new WaitForSeconds(0.01f);
    private Button buttonProperty;
    private Coroutine coroutine;
    private float MinSize = 1f, MaxSize = 1.2f;
    private bool isScaleUp;

    private string enterHexColor = "#FFFFFF";
    private string exitHexColor = "#A8A8A8";
    private Color enterColor, exitColor;

    private void OnEnable() {
        transform.localScale = new Vector3(MinSize, MinSize, 0);
        ColorBlock colorBlock = buttonProperty.colors;
        colorBlock.normalColor = exitColor;
        buttonProperty.colors = colorBlock;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (coroutine != null) StopCoroutine(coroutine);
        isScaleUp = true;
        coroutine = StartCoroutine(ScaleAnimation());
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (coroutine != null) StopCoroutine(coroutine);
        isScaleUp = false;
        StartCoroutine(ScaleAnimation());
    }
    private void Awake() {
        TryGetComponent(out buttonProperty);
        ColorUtility.TryParseHtmlString(enterHexColor, out enterColor);
        ColorUtility.TryParseHtmlString(exitHexColor, out exitColor);
    }
    IEnumerator ScaleAnimation() {
        ColorBlock colorBlock = buttonProperty.colors;
        while ((isScaleUp && transform.localScale.x < MaxSize) || (!isScaleUp && transform.localScale.x > MinSize)) {
            colorBlock.normalColor = enterColor;
            buttonProperty.colors = colorBlock;
            float scaleChange = isScaleUp ? 0.02f : -0.02f;
            transform.localScale = new Vector3(transform.localScale.x + scaleChange, transform.localScale.y + scaleChange, 0);
            yield return WaitForSeconds;
        }
        colorBlock.normalColor = exitColor;
        buttonProperty.colors = colorBlock;
    }
}
