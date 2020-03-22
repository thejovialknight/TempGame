using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDepthController : MonoBehaviour
{
    public float depthPointOffset;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + depthPointOffset);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + depthPointOffset, transform.position.y + depthPointOffset), 0.1f);
    }
}
