using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform hutCheck;
    [SerializeField] private Transform feetCheck;
    [SerializeField] private Vector2 zOrder;
    [SerializeField] private HutController _hutController;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zOrder.x);
        //if (horizontalInput < 0)
        //    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        //else
        //    transform.localScale = new Vector3(transform.localScale.x * 1, transform.localScale.y, transform.localScale.z);
    }



    private float horizontal;
    private float vertical;

    void Update()
    {
        if (GameplayManager.Instance && GameplayManager.Instance.DisablePlayerControls)
            return;

        horizontal = 0f;
        vertical = 0f;

        // Horizontal input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1f;
            SetNegativeXScale();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1f;
            SetPositiveXScale();
        }

        if (GameplayManager.Instance && !GameplayManager.Instance.PlayerIsInsideHut)
        {
            // Vertical input (ONLY when key is pressed)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                vertical = 1f;
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                vertical = -1f;
        }

        Vector3 movement = new Vector3(horizontal, vertical, 0f);

        if (movement.magnitude > 1f)
            movement.Normalize();

        // Predict next position
        Vector3 nextPos = transform.position + movement * moveSpeed * Time.deltaTime;

        // Only move if next position is on ground
        if (!IsHeadOverlappingHut() && IsOnGround(nextPos))
        {
            transform.position = nextPos;
        }
        else
        {
            if (vertical < 0)
            {
                movement = new Vector3(horizontal, vertical, 0f);
                nextPos = transform.position + movement * moveSpeed * Time.deltaTime;
                transform.position = nextPos;
            }
            else
                // Allow horizontal even if vertical blocked
                transform.position += new Vector3(horizontal, 0f, 0f) * moveSpeed * Time.deltaTime;
        }

        CheckIfPlayerCollidingWithObstacle();
    }

    void SetNegativeXScale()
    {
        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void SetPositiveXScale()
    {
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    bool IsOnGround(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position, groundLayer);
        return hit != null;
    }

    bool IsHeadOverlappingHut()
    {
        Collider2D hit = Physics2D.OverlapPoint(hutCheck.position, obstacleMask);
        return hit != null;
    }

    public void CheckIfPlayerCollidingWithObstacle()
    {
        if (!_hutController.HutTriggerDetector.isPlayerInsideHutTrigger)
            return;

        if (GameplayManager.Instance && GameplayManager.Instance.PlayerIsInsideHut)
            return;

        Collider2D footOnHut = Physics2D.OverlapPoint(feetCheck.position, obstacleMask);
        Collider2D headOnHut = Physics2D.OverlapPoint(hutCheck.position, obstacleMask);


        if (footOnHut != null)
        {
            if (transform.position.y < (footOnHut.transform.position.y * 1.5f))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zOrder.y);
            }
        }
        else if (headOnHut != null)
        {
            if (transform.position.y > (headOnHut.transform.position.y / 2.5f))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zOrder.y);
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zOrder.x);
        }
    }

    public void UpdateZOrderOfPlayer(bool shouldMoveBackward)
    {
        if (shouldMoveBackward)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zOrder.y);

        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zOrder.x);
        }
    }

}
