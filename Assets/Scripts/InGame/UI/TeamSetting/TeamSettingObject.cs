using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Define;

public class TeamSettingObject : MonoBehaviour
{
    public UnitType type;

    [SerializeField] private Image unitImage;
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI fightScoreTMP;

    public void UpdateUI()
    {
        nameTMP.text = DataBaseManager.Instance.tdUnitDict[(int)type].Name;
    }
}
