using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] public GameObject target;
    [SerializeField] private float smoothTime;
    private Vector3 current = Vector3.zero;
    public float distanceFormTager = 10f;
    public float heightAboveTager = 5f;

    private void Start()
    {

    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref current, smoothTime);

            transform.LookAt(target.transform);

        }
    }
    private void OnEnable()
    {
        LevelManager.loadLevelEvent += SetUpCamera;
    }
    private void OnDisable()
    {
        LevelManager.loadLevelEvent -= SetUpCamera;

    }

    private void SetUpCamera()
    {
        Vector3 newPosition = target.transform.position - target.transform.forward * distanceFormTager;
        newPosition.y = target.transform.position.y + heightAboveTager;
        transform.position = newPosition;
        offset = transform.position - target.transform.position;
    }
}
