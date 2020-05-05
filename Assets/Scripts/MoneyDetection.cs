using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDetection : MonoBehaviour
{
    public GameObject thisPage;
    public GameObject nextPage;
    public GameObject Screen;
    public Animator cameraAnimator;
    MenuSelection store;

    void Start()
    {
        // find gameobject with tag "SelectedStore", which is Menus, and access this component
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
    }

    void Update()
    {
        // show next page if money has been inserted (higher than 0)
        if(store.insertedMoney > 0)
        {
            zoomIn();
            thisPage.SetActive(false);
            nextPage.SetActive(true);
        }
    }

    void zoomIn()
    {
        cameraAnimator.ResetTrigger("ZoomOut");
        cameraAnimator.SetTrigger("ZoomIn");
    }
}
