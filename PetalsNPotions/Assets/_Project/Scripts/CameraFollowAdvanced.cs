using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowAdvanced : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [Header("Follow")]
    [SerializeField] private float followSpeed = 12f;

    [Header("Bounds")]
    [SerializeField] private BoxCollider2D boundsCollider;

    [Header("Zoom by Height")]
    [SerializeField] private float normalZoom = 5f;
    [SerializeField] private float zoomedOut = 6.5f;
    [SerializeField] private float upperZoomY = 5f;
    [SerializeField] private float zoomSpeed = 6f;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = normalZoom;
    }

    void LateUpdate()
    {
        if (!target) return;

        // -------- POSITION (NO SMOOTHDAMP) --------
        Vector3 desiredPos = target.position + offset;
        desiredPos.z = transform.position.z;

        Vector3 newPos = Vector3.MoveTowards(
            transform.position,
            desiredPos,
            followSpeed * Time.deltaTime
        );

        // -------- BOUNDS --------
        if (boundsCollider)
        {
            Bounds b = boundsCollider.bounds;
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            newPos.x = Mathf.Clamp(newPos.x, b.min.x + camWidth, b.max.x - camWidth);
            newPos.y = Mathf.Clamp(newPos.y, b.min.y + camHeight, b.max.y - camHeight);
        }

        transform.position = newPos;

        // -------- ZOOM (HEIGHT BASED ONLY) --------
        float targetZoom = target.position.y > upperZoomY ? zoomedOut : normalZoom;

        cam.orthographicSize = Mathf.MoveTowards(
            cam.orthographicSize,
            targetZoom,
            zoomSpeed * Time.deltaTime
        );
    }
}
