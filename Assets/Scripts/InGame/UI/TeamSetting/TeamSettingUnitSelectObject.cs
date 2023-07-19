using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Define;

public class TeamSettingUnitSelectObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject SelectedImage;
    public GameObject UnlockImage;
    public bool isUnlocked;
    public UnitType type;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUnlocked) return;
        transform.parent.parent.TryGetComponent<TeamSettingUnitSelect>(out TeamSettingUnitSelect unitSelect);
        if (unitSelect.selectLimit >= GameManager.Instance.userDataManager.data.TeamLimit) return;
        unitSelect.unitSelected[type] = !unitSelect.unitSelected[type];
        SetSelectedImage(unitSelect.unitSelected[type]); // selected effect outline
        unitSelect.selectLimit++;
    }

    public void SetUnlockImage(bool value)
    {
        UnlockImage.SetActive(value);
    }

    public void SetSelectedImage(bool value)
    {
        SelectedImage.SetActive(value);
    }
}
