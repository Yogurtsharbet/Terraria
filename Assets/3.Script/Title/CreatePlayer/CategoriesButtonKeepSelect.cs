using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CategoriesButtonKeepSelect : MonoBehaviour, IPointerDownHandler {
    [SerializeField] private CategoriesButtonEvent categories;

    public void OnPointerDown(PointerEventData eventData) {
        if (categories.gameObject.activeSelf)
            for (int i = 0; i < categories.buttons.Length; i++) {
                if (categories.isSelected[i]) categories.buttons[i].Select();
            }
    }
}