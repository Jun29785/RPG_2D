using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI currentStage;
    [SerializeField] private TextMeshProUGUI enemyInfo;

    public void UpdateStageInfo()
    {
        StageManager sm = InGameManager.Instance.stageManager;
        slider.maxValue = sm.allEnemyCount;
        slider.minValue = 0;
        slider.value = sm.currentEnemyCount;
        currentStage.text = $"스테이지 {sm.currentStage}";
        enemyInfo.text = $"{sm.currentEnemyCount}/{sm.allEnemyCount}";
    }
}
