using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDetection : MonoBehaviour
{
    public GameObject thisPage;
    public GameObject nextPage;

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
            thisPage.SetActive(false);
            nextPage.SetActive(true);
        }
    }
}
