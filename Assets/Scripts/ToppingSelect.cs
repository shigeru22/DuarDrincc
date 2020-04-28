using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingSelect : MonoBehaviour
{
    public Button[] toppings; // set size to 7 and follow MenuSelection.cs for toppings' order
    public GameObject thisPage; // set to ToppingSelect
    public GameObject nextPage; // set to Confirmation

    MenuSelection store;

    void Start()
    {
        Debug.Log("this thing ran late");
        // find gameobject with tag "SelectedStore", which is Menus, and access this component
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

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

    void ToppingClick(int idx)
    {
        store.selectedTopping = idx;

        thisPage.SetActive(false); // hide this page
        nextPage.SetActive(true); // and show next page
    }
}
