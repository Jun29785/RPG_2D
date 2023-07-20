using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;

public class TeamSetting : MonoBehaviour
{
    public Transform teamSettingObjectParent;

    [SerializeField] private Transform teamSelectPanel;
    [SerializeField] private GameObject teamSettingObject;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        CreateObject();
    }

    void Update()
    {

    }

    public void CreateObject()
    {
        foreach (Transform obj in teamSettingObjectParent) Destroy(obj.gameObject);

        foreach(KeyValuePair<int,bool> pair in GameManager.Instance.userDataManager.data.selectedUnits)
        {
            if (pair.Value)
            {
                Instantiate(teamSettingObject, teamSettingObjectParent).TryGetComponent<TeamSettingObject>(out TeamSettingObject obj);
                obj.type = (UnitType)pair.Key;
                obj.UpdateUI();
            }
        }
    }

    public void OnClickTeamSettingButton()
    {
        teamSelectPanel.gameObject.SetActive(true);
    }
}
