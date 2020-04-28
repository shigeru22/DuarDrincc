using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowClick : MonoBehaviour
{
    public Animator windowAnimator;
    public Button slot;

    void Start()
    {
        slot.onClick.AddListener(delegate
        {
            windowAnimator.SetTrigger("SlotClicked");
        });
    }
}
