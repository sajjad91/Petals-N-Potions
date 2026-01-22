using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform doorTrigger;
    [SerializeField] private Transform doorTriggerInside;
    [SerializeField] private Vector3 playerScaleOutsie;
    [SerializeField] private Vector3 playerScaleInside;
    [SerializeField] private Vector3 playerInsidePosition;
    [SerializeField] private Vector3 playerOutsidePosition;
    [SerializeField] private HutController _hutController;

    private bool goingInsideHutPressed = false;
    private bool goingOutsideHutPressed = false;
    private bool hasUserPressedEToPickFlower = false;

    public bool HasUserPressedEToPickFlower { get => hasUserPressedEToPickFlower; set { hasUserPressedEToPickFlower = value; } }


    private void Start()
    {
        transform.position = playerOutsidePosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Position:" + Vector3.Distance(transform.position, doorTrigger.position));

        if(Input.GetKey(KeyCode.E))
        {
            hasUserPressedEToPickFlower = true;
        }

        if (!goingInsideHutPressed)
        {
            if (Vector3.Distance(transform.position, doorTrigger.position) < 1f)
            {
                //Debug.Log("doortrigger position:" + doorTrigger.position);

                if (GameplayManager.Instance)
                {
                    GameplayManager.Instance.SetEnvironmentToFade(true);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    //Debug.Log("Going Inside Hut");
                    goingInsideHutPressed = true;
                    goingOutsideHutPressed = false;
                    StartCoroutine(_StartGoingInsideRoutine());
                }
            }
            else
            {
                if (GameplayManager.Instance)
                {
                    GameplayManager.Instance.SetEnvironmentToFade(false);
                }
            }
        }

        if (!goingOutsideHutPressed && GameplayManager.Instance.PlayerIsInsideHut)
        {
            //Debug.Log("Position:" + Vector3.Distance(transform.position, doorTriggerInside.position));

            if ((Vector3.Distance(transform.position, doorTriggerInside.position) < 0.4f))
            {
                if (GameplayManager.Instance)
                {
                    GameplayManager.Instance.SetInsideEnvironmentToFade(true);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    //Debug.Log("Going Inside Hut");
                    goingOutsideHutPressed = true;
                    goingInsideHutPressed = false;
                    StartCoroutine(_StartGoingOutsideRoutine());
                }
            }
            else
            {
                if (GameplayManager.Instance)
                {
                    GameplayManager.Instance.SetInsideEnvironmentToFade(false);
                }
            }
        }
    }

    private void LateUpdate()
    {
        //hasUserPressedEToPickFlower = false;
    }

    private IEnumerator _StartGoingInsideRoutine()
    {
        if (GameplayManager.Instance)
            GameplayManager.Instance.CameraController.FadeToBlack();

        transform.position = Vector3.zero;

        yield return new WaitForSeconds(1f);

        if (GameplayManager.Instance)
            GameplayManager.Instance.SetInsideHutActive();

        yield return new WaitForSeconds(0.2f);

        transform.localScale = playerScaleInside;
        transform.position = playerInsidePosition;

        if (GameplayManager.Instance)
            GameplayManager.Instance.CameraController.RevertBlackFade();

        yield break;
    }

    private IEnumerator _StartGoingOutsideRoutine()
    {
        if (GameplayManager.Instance)
            GameplayManager.Instance.CameraController.FadeToBlack();

        //transform.position = Vector3.zero;
        yield return new WaitForSeconds(1f);

        if (GameplayManager.Instance)
            GameplayManager.Instance.SetOutSideActive();

        yield return new WaitForSeconds(0.2f);

        transform.localScale = playerScaleOutsie;
        transform.position = playerOutsidePosition;

        if (GameplayManager.Instance)
            GameplayManager.Instance.CameraController.RevertBlackFade();

        yield break;
    }
}
