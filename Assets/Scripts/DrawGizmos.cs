using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public Transform nextRoadTrigger;
    public float drawWireSphereSize = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDrawGizmos()
    {
        if (nextRoadTrigger != null)
        {
            Gizmos.DrawLine(this.gameObject.transform.position, nextRoadTrigger.position);
        }
        Gizmos.DrawWireSphere(transform.position, drawWireSphereSize);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
