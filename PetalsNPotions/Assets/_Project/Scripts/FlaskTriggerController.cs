using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskTriggerController : MonoBehaviour
{
    private Vector3 initialLocalScale;
    private bool isPotionPrepared = false;

    public bool IsPotionPrepared { get => isPotionPrepared; set => isPotionPrepared = value; }

    private void Start()
    {
        initialLocalScale = transform.localScale;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Herbalist")
        {
            if (GameplayManager.Instance)
            {
                GameplayManager.Instance.IsPlayerCollidingWithFlask = true;
            }
            transform.localScale *= 1.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.name == "Herbalist")
        {
            if (GameplayManager.Instance)
            {
                GameplayManager.Instance.IsPlayerCollidingWithFlask = false;

            }

            transform.localScale = initialLocalScale;
        }
    }
}
