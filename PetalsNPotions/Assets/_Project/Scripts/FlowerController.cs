using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    private Vector3 initialFlowerHeadScale;
    private Vector3 initialFlowerBodyScale;

    [SerializeField] private bool isMushroom = default;
    [SerializeField] private Transform flowerHead;
    [SerializeField] private Transform flowerBody;
    [SerializeField] private FlowerData flowerData;

    private bool isCollidingWithPlayer = false;
    private PlayerController _playerController;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {        
        if(isCollidingWithPlayer && _playerController != null)
        {
            if(_playerController.HasUserPressedEToPickFlower)
            {
                Debug.Log("HaseUserPressed E: " + _playerController.HasUserPressedEToPickFlower);

                flowerData.isFlowerPicked = true;
                UpdateDataInGameController();
                gameObject.SetActive(false);
                _playerController.HasUserPressedEToPickFlower = false;
                _playerController = null;
            }
        }
    }
    private void Start()
    {
        if(!isMushroom)
        {
            initialFlowerHeadScale = flowerHead.localScale;
        }

        initialFlowerBodyScale = flowerBody.localScale;


    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Other Name: " + other.name);

        if(other.transform.parent.name == "Herbalist")
        {
            if(!isMushroom)
            {
                flowerHead.localScale *= 1.2f;
            }

            flowerBody.localScale *= 1.2f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.name == "Herbalist")
        {
            Transform parent = other.transform.parent;
            isCollidingWithPlayer = true;
            _playerController = parent.GetComponent<PlayerController>();

            if (parent.position.y > flowerBody.transform.position.y)
            {
                //Debug.LogFormat("Parent Y: {0}, Flower Y: {1}", parent.position.y, flowerBody.transform.position.y);

                parent.GetComponent<PlayerMovementController>().UpdateZOrderOfPlayer(true);
            }
            else
            {
                parent.GetComponent<PlayerMovementController>().UpdateZOrderOfPlayer(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "Herbalist")
        {
            isCollidingWithPlayer = false;
            _playerController = null;

            if (!isMushroom)
            {
                flowerHead.localScale = initialFlowerHeadScale;
            }

            flowerBody.localScale = initialFlowerBodyScale;
        }
    }

    private void UpdateDataInGameController()
    {
        if (GameController.Instance)
        {
            GameController.Instance.UpdateFlowersDataAndCount(flowerData, 1);
        }
    }
}
