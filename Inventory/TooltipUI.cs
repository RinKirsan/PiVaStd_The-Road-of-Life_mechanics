using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance; // Для доступа к панели из других скриптов

    public GameObject tooltipPanel; // Панель подсказки
    public Text titleText; // Текст для названия предмета
    public Text descriptionText; // Текст для описания предмета

    private RectTransform panelRectTransform; // Для управления позицией

    private void Awake()
    {
        Instance = this; // Сохраняем ссылку на объект
        panelRectTransform = tooltipPanel.GetComponent<RectTransform>();
        tooltipPanel.SetActive(false); // Скрываем подсказку при старте
    }

    public void ShowTooltip(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 mousePosition = Input.mousePosition;

            // Добавляем небольшое смещение
            float offsetX = 250f; // Сдвиг вправо
            float offsetY = 50f; // Сдвиг вниз
            mousePosition.x += offsetX;
            mousePosition.y -= offsetY;

            // Перемещаем панель в позицию мыши с учётом смещения
            panelRectTransform.position = mousePosition;
        }
    }


}
