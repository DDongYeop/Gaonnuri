using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public List<StageSO> Stages;
    public List<PlayerSO> PlayerSOs;
    public List<GameObject> AreaObjects;
    public List<Sprite> AreaUiImage;
}
