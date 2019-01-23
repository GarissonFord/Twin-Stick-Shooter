using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        FollowTarget();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = target.transform.position + distanceFromTarget;
    }
}
