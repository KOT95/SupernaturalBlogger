using UnityEngine;

public class VideoCameraRaycast : MonoBehaviour
{
    [SerializeField] private int rays = 8;
    [SerializeField] private int layer = 3;
    [SerializeField] private float distance = 33;
    [SerializeField] private float angle = 40;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask layerMask;

    public Supernatural Supernatural { get; private set; }
    
    private bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 pos = transform.position + offset;
        if (Physics.Raycast(pos, dir, out hit, distance, layerMask))
        {
            if (hit.transform.TryGetComponent(out Supernatural supernatural))
            {
                if (!supernatural.IsActivate)
                {
                    result = true;
                    Debug.DrawLine(pos, hit.point, Color.green);
                    Supernatural = supernatural;
                }
                else
                {
                    Debug.DrawLine(pos, hit.point, Color.yellow);
                }
            }
            else
            {
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.red);
        }
        return result;
    }

    public bool RayToScan()
    {
        bool result = false;
        
        for (int l = 0; l < layer; l++)
        {
            bool a = false;
            bool b = false;
            float j = 0;
            
            for (int i = 0; i < rays; i++)
            {
                var x = Mathf.Sin(j);
                var y = Mathf.Cos(j);

                j += angle * Mathf.Deg2Rad / rays;

                Vector3 dir = transform.TransformDirection(new Vector3(x, Mathf.Lerp(0, l, 0.3f), y));
                if (GetRaycast(dir)) a = true;

                if (x != 0)
                {
                    dir = transform.TransformDirection(new Vector3(-x, Mathf.Lerp(0, l, 0.3f), y));
                    if (GetRaycast(dir)) b = true;
                }
            }  
            
            if (a || b) result = true;
        }
        
        return result;
    }
}
