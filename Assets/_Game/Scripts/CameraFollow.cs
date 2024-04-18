using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 current = Vector3.zero;
    public float distanceFormTager = 10f;
    public float heightAboveTager = 5f;

    private void Start()
    {
        Vector3 newPosition = target.position - target.forward * distanceFormTager;
        newPosition.y = target.position.y + heightAboveTager;
        transform.position = newPosition;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref current, smoothTime);

        transform.LookAt(target);
    }
}
