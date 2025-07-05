using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;

    public void AddItem(Item item)
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                //SpawnNewItem(item, slot);
                //return true;
            }
        }
    }

    //void SpawnNewItem (Item item, InventorySlot slot)
    //{
    //    GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
    //    InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
    //    inventoryItem.InitializeItem(item);
    //}
}
