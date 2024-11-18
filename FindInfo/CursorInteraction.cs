/*
README
Скрипт накинуть на контроллер игрока.
В playerCamera передать активную камеру игрока.
В documentLayer выбрать слой Document, предварительно создав его
в maxInteractionDistance можно поставить нужную дистанцию для взаимодействия (по умолчанию 5)
*/

using UnityEngine;

public class CursorInteraction : MonoBehaviour
{
    public Camera playerCamera; // Камера игрока
    public float maxInteractionDistance = 5f; // Максимальная дистанция для взаимодействия
    public LayerMask documentLayer; // Слой для документов
    private GameObject highlightedObject; // Текущий объект под курсором

    void Update()
    {
        HandleCursorRaycast();
        HandleInteractionInput();
    }

    // Обработка луча от камеры
    private void HandleCursorRaycast()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем попадание в объект документа
        if (Physics.Raycast(ray, out hit, maxInteractionDistance, documentLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Подсвечиваем объект, если наводимся
            if (highlightedObject != hitObject)
            {
                if (highlightedObject != null)
                {
                    UnhighlightObject(highlightedObject);
                }

                HighlightObject(hitObject);
                highlightedObject = hitObject;
            }
        }
        else
        {
            // Убираем подсветку, если луч не попадает никуда
            if (highlightedObject != null)
            {
                UnhighlightObject(highlightedObject);
                highlightedObject = null;
            }
        }
    }

    // Обработка нажатия клавиши E
    private void HandleInteractionInput()
    {
        if (highlightedObject != null && Input.GetKeyDown(KeyCode.E))
        {
            DocumentInteraction document = highlightedObject.GetComponent<DocumentInteraction>();
            if (document != null)
            {
                document.OpenDocument();
            }
        }
    }

    // Метод для подсветки объекта
    private void HighlightObject(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.yellow; // Замените на цвет подсветки
        }
    }

    // Метод для снятия подсветки
    private void UnhighlightObject(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white; // Вернуть исходный цвет
        }
    }
}
