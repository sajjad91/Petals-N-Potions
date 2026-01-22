using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFocusZoom : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform flask;
    [SerializeField] private Vector3 defaultPosition;

    [Header("Zoom")]
    [SerializeField] private float normalZoom = 5f;
    [SerializeField] private float zoomInZoom = 3f;
    [SerializeField] private float zoomSpeed = 6f;
    [SerializeField] private float moveSpeed = 6f;

    [Header("Screen Framing")]
    [Tooltip("0 = left, 0.5 = center, 1 = right")]
    [SerializeField] private float flaskViewportX = 0.78f;

    private Camera cam;
    private bool zoomedIn;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = normalZoom;
        transform.position = defaultPosition;
    }

    void Update()
    {
        if (GameplayManager.Instance && GameplayManager.Instance.IsPlayerCollidingWithFlask)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                zoomedIn = true;
                MenuController.Instance.ShowPetalsMenu();
                GameplayManager.Instance.DisablePlayerControls = true;


            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                zoomedIn = false;
                MenuController.Instance.HidePetalsMenu();
                GameplayManager.Instance.DisablePlayerControls = false;
            }
        }
    }

    void LateUpdate()
    {
        if (zoomedIn)
            ZoomInToFlask();
        else
            ZoomOut();
    }

    void ZoomInToFlask()
    {
        cam.orthographicSize = Mathf.MoveTowards(
            cam.orthographicSize,
            zoomInZoom,
            zoomSpeed * Time.deltaTime
        );

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        float targetCamX =
            flask.position.x - (flaskViewportX - 0.5f) * camWidth;

        Vector3 targetPos = new Vector3(
            targetCamX,
            flask.position.y,
            transform.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }

    void ZoomOut()
    {
        cam.orthographicSize = Mathf.MoveTowards(
            cam.orthographicSize,
            normalZoom,
            zoomSpeed * Time.deltaTime
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            defaultPosition,
            moveSpeed * Time.deltaTime
        );
    }
}
