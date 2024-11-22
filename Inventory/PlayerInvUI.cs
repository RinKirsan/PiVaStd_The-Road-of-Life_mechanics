using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;



public class PlayerInvUI : MonoBehaviour
{
    public static PlayerInvUI Instance;
    public GameObject playerInventoryPanel; // Панель UI
    public GameObject buttonPrefab; // Префаб кнопки
    public Transform controlPanel;
    [SerializeField]
    private MonoBehaviour cameraControlScript; // Скрипт управления камерой

    public static MonoBehaviour CameraControlScript
    {
        get
        {
            return Instance?.cameraControlScript;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerInventoryPanel.SetActive(false); // Скрываем панель при старте
    }

   public static void OpenInv(PlayerInvUI playerInvUI, List<InventoryItem> inventoryItems)
    {
        int itemCount = (int)Math.Ceiling((float)inventoryItems.Count / 7) - 4;
        float heightControlPanel = 1150f;
        if (itemCount > 0){
            for (int i = 0; i < itemCount; i++)
            {
                heightControlPanel += 265f;
            }
        }

        RectTransform controlPanelRect = playerInvUI.controlPanel.GetComponent<RectTransform>();
        controlPanelRect.sizeDelta = new Vector2(controlPanelRect.sizeDelta.x, heightControlPanel);

        foreach (var item in inventoryItems)
        {
            // Создаем кнопку из префаба
            GameObject newButton = Instantiate(playerInvUI.buttonPrefab, playerInvUI.controlPanel);

            // Настраиваем текст кнопки
            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.text = item.itemName;

            // Найти дочерний объект Image для иконки
            Transform iconTransform = newButton.transform.Find("Icon");
            if (iconTransform != null)
            {
                Image iconImage = iconTransform.GetComponent<Image>();
                if (iconImage != null && item.icon != null)
                {
                    iconImage.sprite = item.icon;
                    iconImage.enabled = true; // Убедитесь, что Image включен
                }
            }
            else
            {
                Debug.LogWarning("Не удалось найти дочерний объект 'Icon' у кнопки.");
            }

            // Добавляем ControlPanel для ScrollRect в префабе
            ScrollRectHandler scrollHandler = newButton.GetComponent<ScrollRectHandler>();
            if (scrollHandler != null)
            {
                scrollHandler.SetContent(playerInvUI.controlPanel.GetComponent<RectTransform>());
            }

            // Добавляем EventTrigger для отображения подсказки
            EventTrigger trigger = newButton.AddComponent<EventTrigger>();

            // Добавляем событие PointerEnter
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => 
                TooltipUI.Instance.ShowTooltip(item.itemName, item.description));
            trigger.triggers.Add(entryEnter);

            // Добавляем событие PointerExit
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => TooltipUI.Instance.HideTooltip());
            trigger.triggers.Add(entryExit);

            // Добавляем функционал при клике
            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => playerInvUI.OnItemClicked(item));
            }
        }
        playerInvUI.playerInventoryPanel.SetActive(true);

        // Включаем курсор и отключаем управление камерой
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (CameraControlScript != null)
        {
            CameraControlScript.enabled = false;
        }
    }


    public static void CloseInv(PlayerInvUI playerInvUI)
    {
        foreach (Transform child in playerInvUI.controlPanel)
    {
        GameObject.Destroy(child.gameObject); // Удаляем дочерние объекты
    }
        playerInvUI.playerInventoryPanel.SetActive(false);

        // Скрываем курсор и включаем управление камерой
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (CameraControlScript != null)
        {
            CameraControlScript.enabled = true;
        }
    }

    void OnItemClicked(InventoryItem item)
    {
        // Пример обработки клика
        Debug.Log($"Вы нажали на {item.itemName}: {item.description}. ID: {item.id}");
    }
}