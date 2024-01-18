using UnityEngine;
using System.Collections.Generic;

public class AreaManager : MonoBehaviour
{
    public List<AreaLine> AreaLines;
    [SerializeField] private int _currentStage;

    private void Awake()
    {
        for (int i = 0; i < AreaLines.Count; i++)
        {
            for (int j = 0; j < AreaLines[i].Areas.Count; ++j)
                AreaLines[i].Areas[j].Data = StageManager.Instance.Stages[_currentStage].StageAreaData[i].AreaDataContainer[j];
        }
    }
}
