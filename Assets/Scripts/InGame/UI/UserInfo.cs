using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI coinText;

    public void UpdateText()
    {
        var igm = InGameManager.Instance;
        strengthText.text = igm.teamStrength.ToString();
        coinText.text = igm.userCoin.ToString();
    }
}
