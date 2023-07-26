using UnityEngine;
using UnityEngine.EventSystems;

// Inventory Button
public class InventoryManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Inventory inventoryPanel;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryPanel.gameObject.activeSelf)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            inventoryPanel.InitializeInventory();
            inventoryPanel.gameObject.SetActive(true);
        }
    }
}
