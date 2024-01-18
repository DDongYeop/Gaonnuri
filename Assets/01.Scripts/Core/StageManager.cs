using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public List<StageSO> Stages;
    public List<GameObject> AreaObjects;
}
