using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance; // Для доступа к инвентарю игрока

    public List<InventoryItem> items = new List<InventoryItem>(); // Список предметов в инвентаре

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

    // Добавить предмет в инвентарь
    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        Debug.Log($"Добавлен предмет: {item.itemName}");
    }

    // Удалить предмет из инвентаря
    public void RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Удалён предмет: {item.itemName}");
        }
    }

    public static void OpenInv()
{
    if (Instance != null)
    {
        PlayerInvUI.OpenInv(PlayerInvUI.Instance, Instance.items);
        Debug.Log("Инвентарь открыт.");
    }
    else
    {
        Debug.LogError("Instance PlayerInventory не существует!");
    }
}
    public static List<InventoryItem> getPlayerInv(){
        return Instance.items;
    }
}
