using UnityEngine;

public class Area : MonoBehaviour
{
    public AreaData Data = AreaData.NULL;
    public GameObject _child;

    private void Start()
    {
        if (!_child)
            return;
        
        GameObject obj = Instantiate(StageManager.Instance.AreaObjects[(int)Data], transform, true);
        obj.transform.position = transform.position;
        obj.transform.parent = transform;
        _child = obj;
    }

    public void SetUp(AreaData data)
    {
        Data = data;
        Destroy(_child);
        GameObject obj = Instantiate(StageManager.Instance.AreaObjects[(int)Data], transform, true);
        obj.transform.position = transform.position;
        obj.transform.parent = transform;
        _child = obj;

        switch (Data)
        {
            case AreaData.NULL:
                GetComponent<Collider>().isTrigger = true;
                break;
            case AreaData.GRASS:
                GetComponent<Collider>().isTrigger = false;
                break;
        }
    }
}
