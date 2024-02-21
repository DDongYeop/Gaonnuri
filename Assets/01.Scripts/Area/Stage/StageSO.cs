using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Stage/StageData", fileName = "Stage")]
public class StageSO : ScriptableObject
{
    public List<AreaDatas> StageAreaData;
    public List<AreaCounterDatas> AreaCounterDatas;

    //플레이어 관련 있어야함.
    //위치 이런거 
}

[Serializable]
public class AreaDatas
{
    public List<AreaData> AreaDataContainer;
}

[Serializable]
public class AreaCounterDatas
{
    public AreaData Area;
    public int Cnt;
    
}
