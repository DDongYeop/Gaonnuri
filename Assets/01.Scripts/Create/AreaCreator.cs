using UnityEngine;

public class AreaCreator : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsArea;
    
    private void Update()
    {
        AreaCreate();
    }

    private void AreaCreate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, _whatIsArea))
        {
            // 여기에 Area 생성 
            // hit.point;
        }
    }
}
