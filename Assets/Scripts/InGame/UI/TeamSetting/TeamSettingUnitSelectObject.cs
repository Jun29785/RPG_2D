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
        Debug.Log("Selected Object");
        if (!isUnlocked) return;
        transform.parent.parent.TryGetComponent<TeamSettingUnitSelect>(out TeamSettingUnitSelect unitSelect);

        if (unitSelect.unitSelected[(int)type]) // 이미 선택 되어 있을때
        {
            unitSelect.selectLimit--;
        }
        else
        {
            if (unitSelect.selectLimit + 1 > GameManager.Instance.userDataManager.data.TeamLimit) return;
            unitSelect.selectLimit++;
        }
        unitSelect.unitSelected[(int)type] = !unitSelect.unitSelected[(int)type];
        SetSelectedImage(unitSelect.unitSelected[(int)type]);
    }

    /// <summary>
    /// 잠겨 있는지를 가시화하는 장치
    /// </summary>
    public void SetUnlockImage(bool value)
    {
        UnlockImage.SetActive(value);
    }

    /// <summary>
    /// 선택 됬는지를 가시화하는 장치
    /// </summary>
    public void SetSelectedImage(bool value)
    {
        SelectedImage.SetActive(value);
    }
}
