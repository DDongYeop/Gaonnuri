using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaSelectManager : MonoSingleton<AreaSelectManager>
{
    [SerializeField] private AreaUI _areaUIObj;
    private Transform _contents;

    public Dictionary<AreaData, AreaUI> AreaDic = new Dictionary<AreaData, AreaUI>(); 
    public Dictionary<AreaData, int> AreaDicCnt = new Dictionary<AreaData, int>(); 

    private void OnEnable()
    {
        PlayerPrefs.SetInt("CurrentStage", 1);
        _contents = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
    }

    private void Start()
    {
        foreach (AreaCounterDatas areaCounterData in StageManager.Instance.Stages[PlayerPrefs.GetInt("CurrentStage")].AreaCounterDatas)
        {
            AreaUI areaUI = Instantiate(_areaUIObj, _contents.transform);
            areaUI.SetUp(areaCounterData.Area, StageManager.Instance.AreaUiImage[(int)areaCounterData.Area], areaCounterData.Cnt);
            AreaDic[areaCounterData.Area] = areaUI;
            AreaDicCnt[areaCounterData.Area] = areaCounterData.Cnt;
        }
    }
}
