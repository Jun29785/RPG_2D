using UnityEngine;

public class TeamSettingUnitSelect : MonoBehaviour
{
    private bool isSelected;
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            SelectedAnyObject();
        }
    }
    public TeamSettingUnitSelectObject[] selectObjects;
    public TeamSettingUnitSelectObject selectedObject;

    void Start()
    {

    }

    void Update()
    {

    }

    void InitializeObjects()
    {

    }

    private void SelectedAnyObject()
    {
        foreach(TeamSettingUnitSelectObject obj in selectObjects)
        {
            if (selectedObject == obj)
            {
                obj.SetSelectedImage(true);
            }
            else
            {
                obj.SetSelectedImage(false);
            }
        }
    }

    public void OnClickSelectButton()
    {
        if (isSelected)
        {
            InGameManager.Instance.canvas.teamSetting.selectedObject.type = selectedObject.type;
        }
        gameObject.SetActive(false);
    }
}
