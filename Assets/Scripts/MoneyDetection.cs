using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDetection : MonoBehaviour
{
    public GameObject thisPage;
    public GameObject nextPage;
    public GameObject Screen;

    MenuSelection store;
    CameraControl cameraAnimator;

    void Start()
    {
        // find gameobject with tag "SelectedStore", which is Menus, and access this component
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
        cameraAnimator = GameObject.FindGameObjectWithTag("CameraTrigger").GetComponent<CameraControl>();
    }

    void Update()
    {
        // show next page if money has been inserted (higher than 0)
        if(store.insertedMoney > 0)
        {
            if(cameraAnimator.getZoomStatus() == false) cameraAnimator.trigger = true;

            thisPage.SetActive(false);
            nextPage.SetActive(true);
        }
    }
}
