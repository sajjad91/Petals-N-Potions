using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutTriggerDetector : MonoBehaviour
{
    public bool isPlayerInsideHutTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.transform.parent.name == "Herbalist")
        {
            isPlayerInsideHutTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");

        if (other.transform.parent.name == "Herbalist")
        {
            isPlayerInsideHutTrigger = false;
        }
    }
}
