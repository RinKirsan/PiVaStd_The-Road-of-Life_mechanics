using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public InventoryItem itemData; // Данные о предмете, которые добавятся в инвентарь игрока

    // Метод для поднятия предмета
    public void Pickup()
    {
        // Добавляем предмет в инвентарь игрока
        PlayerInventory.Instance.AddItem(itemData);
        Debug.Log($"Предмет {itemData.itemName} поднят!");

        // Уничтожаем объект из мира после поднятия
        Destroy(gameObject);
    }
}
