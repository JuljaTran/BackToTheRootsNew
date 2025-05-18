using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText; // Dein TMP-Text-Element
    public Color hoverColor = Color.black; // Farbe beim Hover
    private Color originalColor;

    public Image whiteIcon;  // Das weiße Icon
    public Image blackIcon;  // Das schwarze Icon

    private void Start()
    {
        if (buttonText != null)
        {
            originalColor = buttonText.color;
        }

        if (whiteIcon != null) whiteIcon.gameObject.SetActive(true);
        if (blackIcon != null) blackIcon.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = hoverColor;
        }

        if (whiteIcon != null) whiteIcon.gameObject.SetActive(false);
        if (blackIcon != null) blackIcon.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor;
        }

        if (whiteIcon != null) whiteIcon.gameObject.SetActive(true);
        if (blackIcon != null) blackIcon.gameObject.SetActive(false);
    }
}