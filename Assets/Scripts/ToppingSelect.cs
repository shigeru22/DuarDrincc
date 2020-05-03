using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingSelect : MonoBehaviour
{
    public Button[] toppings; // set size to 7 and follow MenuSelection.cs for toppings' order
    public GameObject[] soldOuts; // set size to 6 and add SoldOutButton2-* in incremental order

    public Button backButton;

    public GameObject prevPage; // set to TempSelect
    public GameObject thisPage; // set to ToppingSelect
    public GameObject nextPage; // set to Confirmation

    MenuSelection store;
    DrinccStorage storage;

    [System.NonSerialized]
    public bool runOnce = false; // so update part won't keep looping
    [System.NonSerialized]
    public bool tempClicked = false; // whether temperature buttons are clicked

    void Start()
    {
        // find gameobject with tag "SelectedStore", which is Menus, and access this component
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
        // same for "DrinccStorage"
        storage = GameObject.FindGameObjectWithTag("DrinccStorage").GetComponent<DrinccStorage>();

        try
        {
            backButton.onClick.AddListener(BackPageClick); // this causes an unknown error (not set to an object), but still works
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }

        // same as FlavorSelect.cs, add variable in its own scope
        for (int i = 0; i < toppings.Length; i++)
        {
            int temp = i;
            toppings[i].onClick.AddListener(delegate
            {
                ToppingClick(temp);
                GameObject.FindGameObjectWithTag("UpdateTrigger").GetComponent<ConfirmUpdate>().update = true;
            });
        }
    }

    void Update()
    {
        if(tempClicked == true)
        {
            SoldOutUpdate();
        }
    }

    void ToppingClick(int idx)
    {
        store.selectedTopping = idx;

        thisPage.SetActive(false); // hide this page
        nextPage.SetActive(true); // and show next page
    }

    void BackPageClick()
    {
        thisPage.SetActive(false);
        prevPage.SetActive(true);
    }

    void SoldOutUpdate()
    {
        for (int i = 0; i < toppings.Length; i++)
        {
            if (storage.topping[i] == 0)
            {
                toppings[i].interactable = false;
                if(i != 0) soldOuts[i - 1].SetActive(true);
            }
            else
            {
                toppings[i].interactable = true;
                if(i != 0) soldOuts[i - 1].SetActive(false);
            }
        }

        tempClicked = false;
    }
}
