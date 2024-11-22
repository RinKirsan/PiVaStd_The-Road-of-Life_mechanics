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
    public LayerMask chestLayer; // Слой для документов
    private GameObject highlightedObject; // Текущий объект под курсором
    private bool isOpenPlayerInventary = false;
    [HideInInspector]
    public bool isOpenDocument = false;

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
        else if(Physics.Raycast(ray, out hit, maxInteractionDistance, chestLayer))
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

    // Обработка нажатия клавиш
    private void HandleInteractionInput()
    {
        if (highlightedObject != null && Input.GetKeyDown(KeyCode.E))
        {
            int layer = highlightedObject.layer;

            // Проверяем слой объекта и выполняем соответствующие действия
            if (layer == LayerMask.NameToLayer("Document")) // Если объект на слое Document
            {
                DocumentInteraction document = highlightedObject.GetComponent<DocumentInteraction>();
                if (document != null)
                {
                    document.OpenDocument();
                    isOpenDocument =true;
                }
            }
            else if (layer == LayerMask.NameToLayer("PickupItem")) // Если объект на слое Chest
            {
                PickupItem pickup = highlightedObject.GetComponent<PickupItem>();
                if (pickup != null)
                {
                    pickup.Pickup();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(isOpenPlayerInventary == false)
            {
                // PlayerInvUI.OpenInv(PlayerInvUI.Instance);
                PlayerInventory.OpenInv();
                isOpenPlayerInventary = true;
            } 
            else 
            {
                PlayerInvUI.CloseInv(PlayerInvUI.Instance);
                TooltipUI.Instance.tooltipPanel.SetActive(false);
                Debug.Log($"Инвентарь закрыт");
                isOpenPlayerInventary = false;
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
