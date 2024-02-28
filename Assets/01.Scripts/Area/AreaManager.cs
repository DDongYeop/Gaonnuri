using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance;

    [FormerlySerializedAs("_currentAreaData")] [Header("Area")] 
    public AreaData CurrentAreaData = AreaData.GRASS;
    
    [Header("Area Init")]
    public List<AreaLine> AreaLines;

    [Header("Area Instantiate")]
    private Dictionary<Vector3Int, Area> _areaDic = new Dictionary<Vector3Int, Area>();
    [SerializeField] private GameObject _otherObject;
    [SerializeField] private GameObject _areaObject;
    private Vector3Int[] _addPos = new[]
    {
        new Vector3Int(0, 1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 0, 1), 
        new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, -1), new Vector3Int(0, -1, 0)
    };

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple AreaManager is running");
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < AreaLines.Count; i++)
        {
            for (int j = 0; j < AreaLines[i].Areas.Count; ++j)
            {
                AreaLines[i].Areas[j].Data = StageManager.Instance.Stages[PlayerPrefs.GetInt("CurrentStage")].StageAreaData[i].AreaDataContainer[j];
                AreaLines[i].Areas[j].GetComponent<Collider>().isTrigger = false;
                AddDic(Vector3ToVector3Int(AreaLines[i].Areas[j].transform.position), AreaLines[i].Areas[j]);
            }
        }
    }

    private void AddDic(Vector3Int pos, Area area)
    {
        if (_areaDic.TryGetValue(pos, out var existingArea))
        {
            Destroy(existingArea.gameObject);
        }
        
        _areaDic[pos] = area;
        CreateNewObject(pos);
    }

    public void CreateArea(Vector3Int pos)
    {
        if (_areaDic.TryGetValue(pos, out var existingArea))
        {
            if (existingArea.GetComponent<Area>().Data != AreaData.NULL)
                return;
        }

        Area area = Instantiate(StageManager.Instance.AreaObjects[(int)CurrentAreaData], pos, Quaternion.identity).GetComponent<Area>();
        _areaDic[pos] = area;
        CreateNewObject(pos);
    }

    private void CreateNewObject(Vector3Int pos)
    {
        for (int i = 0; i < 6; ++i)
        {
            Vector3Int value = pos + _addPos[i];
            if (!_areaDic.ContainsKey(value))
            {
                Transform obj = Instantiate(_areaObject, _otherObject.transform, true).transform;
                obj.position = value;
                Area area = obj.GetComponent<Area>();
                area.Data = AreaData.NULL;
                _areaDic.Add(value , area);
            }
        }
    }
    
    private Vector3Int Vector3ToVector3Int(Vector3 pos)
    {
        return new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
    }
}
