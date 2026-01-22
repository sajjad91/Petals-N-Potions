using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutController : MonoBehaviour
{
    [SerializeField] private HutTriggerDetector _hutTriggerDetector;

    public HutTriggerDetector HutTriggerDetector => _hutTriggerDetector;
}
