using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoSingleton<AreaManager>
{
    [HideInInspector] public AreaType CurrentArea; 
    private List<GameObject> _areas;

    public void AreaSelect(AreaType type)
    {
        CurrentArea = type;
    }
}
