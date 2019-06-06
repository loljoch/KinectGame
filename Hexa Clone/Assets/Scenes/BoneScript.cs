using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    public Vector3 start;
    public Vector3 target;
    public float colliderLength;

    LineRenderer line;
    CapsuleCollider capsule;

    public float LineWidth; // use the same as you set in the line renderer.

    void Start()
    {
        line = GetComponent<LineRenderer>();
        capsule = gameObject.AddComponent<CapsuleCollider>();
        capsule.radius = LineWidth / 2;
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis for easier "LookAt" orientation
    }

    void LateUpdate()
    {
        //line.SetPosition(0, start.position);
        //line.SetPosition(1, target.position);
        start = line.GetPosition(0);
        target = line.GetPosition(1);

        capsule.transform.position = start + (target - start) / 2;
        capsule.transform.LookAt(start);
        capsule.height = (target - start).magnitude + colliderLength;

        //capsule.transform.position = start.position + (target.position - start.position) / 2;
        //capsule.transform.LookAt(start.position);
        //capsule.height = (target.position - start.position).magnitude + colliderLength;
    }
}
