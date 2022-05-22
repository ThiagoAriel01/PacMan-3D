using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrezeeZ : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetHongo;
    [SerializeField] private float distance;

    void LateUpdate()
    {
        Vector3 dir = targetHongo.position - transform.position;
        dir.Normalize();
        dir *= distance;
        transform.position = target.position - dir;
        transform.rotation = Quaternion.LookRotation(dir);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
