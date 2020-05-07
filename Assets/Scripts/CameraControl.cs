using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Animator cameraAnimator;

    [System.NonSerialized]
    public bool trigger;
    bool zoomed;

    void Start()
    {
        cameraAnimator = GetComponent<Animator>();

        trigger = false;
        zoomed = false;
    }

    void Update()
    {
        if(trigger == true)
        {
            // camera animation
            if (zoomed == false)
            {
                cameraAnimator.ResetTrigger("ZoomOut");
                cameraAnimator.SetTrigger("ZoomIn");

                zoomed = true;
            }
            else
            {
                cameraAnimator.ResetTrigger("ZoomIn");
                cameraAnimator.SetTrigger("ZoomOut");

                zoomed = false;
            }

            trigger = false;
        }
    }
}
