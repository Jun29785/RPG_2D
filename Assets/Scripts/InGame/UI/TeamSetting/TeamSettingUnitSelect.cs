using UnityEngine;
using Define;
using System.Collections.Generic;
using TMPro;

public class TeamSettingUnitSelect : MonoBehaviour
{
    [Header("Unit Select Status")]
    public Dictionary<UnitType, bool> unitSelected = new Dictionary<UnitType, bool>();

    [SerializeField]
    private int selectlimit;
    public int selectLimit { get { return selectlimit; } set { selectlimit = Mathf.Clamp(value, 0, GameManager.Instance.userDataManager.data.TeamLimit); UIUpdate(); } }

    [Header("UI")]
    private TextMeshProUGUI teamStrengthTMP;
    private TextMeshProUGUI teamLimitTMP;

    [Header("UI Prefab")]
    [SerializeField] private GameObject selectedObjectPrefab;
    [SerializeField] private Transform selectedObjectParent;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.userDataManager.data.unlockedUnits[UnitType.Bandit] = true;
            GameManager.Instance.userDataManager.data.selectedUnits[UnitType.Bandit] = true;
            Debug.Log("Change");
        }    
    }

    private void OnEnable()
    {
        InitializeObjects();
    }

    void InitializeObjects()
    {
        unitSelected = GameManager.Instance.userDataManager.data.selectedUnits;
        // Destroy Child Object
        foreach(Transform obj in selectedObjectParent)
        {
            Destroy(obj.gameObject);
        }
        // Need Creating Prefab Method
        foreach(KeyValuePair<UnitType,bool> pair in GameManager.Instance.userDataManager.data.unlockedUnits)
        {
            Debug.Log("pair");
            Instantiate(selectedObjectPrefab, selectedObjectParent).TryGetComponent<TeamSettingUnitSelectObject>(out TeamSettingUnitSelectObject obj);
            obj.type = pair.Key;
            if (unitSelected[pair.Key])
            {
                selectLimit++;
            }

            obj.isUnlocked = pair.Value;
            obj.SetSelectedImage(unitSelected[pair.Key]);
            obj.SetUnlockImage(!obj.isUnlocked);
        }
    }

    public void OnClickSelectButton()
    {
        foreach (KeyValuePair<UnitType,bool> pair in unitSelected)
        {
            if (pair.Value)
            {
                GameManager.Instance.userDataManager.data.selectedUnits = unitSelected;
                // Active False Team Select Panel
                gameObject.SetActive(false);
            }
        }
    }

    private void UIUpdate()
    {
        teamLimitTMP.text = $"{selectLimit}/{GameManager.Instance.userDataManager.data.TeamLimit}";
    }
}
