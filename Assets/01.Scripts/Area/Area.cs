using UnityEngine;

public class Area : MonoBehaviour
{
    public AreaData Data;

    private void Start()
    {
        GameObject obj = Instantiate(StageManager.Instance.AreaObjects[(int)Data], transform, true);
        obj.transform.position = transform.position;
    }
}
