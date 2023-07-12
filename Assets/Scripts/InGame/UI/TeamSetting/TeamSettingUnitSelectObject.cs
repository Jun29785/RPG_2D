using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Define;

public class TeamSettingUnitSelectObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject SelectedImage;
    public GameObject UnlockImage;
    public bool isUnLocked;
    public UnitType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUnLocked) return;
        transform.parent.parent.TryGetComponent<TeamSettingUnitSelect>(out TeamSettingUnitSelect unitSelect);
        if (!unitSelect.IsSelected) unitSelect.selectedObject = this;
        else unitSelect.selectedObject = null;
        unitSelect.IsSelected = !unitSelect.IsSelected;
    }

    public void SetUnLockImage(bool value)
    {
        UnlockImage.SetActive(value);
    }

    public void SetSelectedImage(bool value)
    {
        SelectedImage.SetActive(value);
    }
}
