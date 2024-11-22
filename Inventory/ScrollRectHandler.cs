using UnityEngine;
using UnityEngine.UI;

public class ScrollRectHandler : MonoBehaviour
{
    public ScrollRect scrollRect; // Поле для привязки ScrollRect
    public RectTransform contentTransform; // Поле для привязки Content через код

    /// <summary>
    /// Устанавливает Content для ScrollRect.
    /// </summary>
    public void SetContent(RectTransform content)
    {
        scrollRect.content = content; // Устанавливаем Content
    }
}
