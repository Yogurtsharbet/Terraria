using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PanelButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Outline[] outlines;
    [SerializeField] private Image Border, Background;
    private void Start() {
        outlines = Border.GetComponents<Outline>();
    }
    private void OnEnable() {
        Border.color = Color.black;
        foreach (Outline outline in outlines)
            outline.effectColor = Color.black;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        Border.color = Color.yellow;
        foreach (Outline outline in outlines) {
            outline.effectColor = Color.yellow;
        }
    }
    public void OnPointerExit(PointerEventData eventData) {
        Border.color = Color.black;
        foreach (Outline outline in outlines)
            outline.effectColor = Color.black;
    }

}
