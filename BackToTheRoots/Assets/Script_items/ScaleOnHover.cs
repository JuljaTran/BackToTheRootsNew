using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleFactor = 1.1f;
    public float animationSpeed = 10f;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover!");
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 target)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * animationSpeed);
            yield return null;
        }

        transform.localScale = target;
    }
}
