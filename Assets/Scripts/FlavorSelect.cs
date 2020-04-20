using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlavorSelect : MonoBehaviour
{
    public Button[] flavors; // set size to 8 and follow MenuSelection.cs for flavors' order
    public GameObject thisPage; // set to MenuSelect
    public GameObject nextPage; // set to TempSelect

    MenuSelection store;

    void Start()
    {
        // find gameobject with tag "SelectedStore", which is Menus, and access this component
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        // in this loop, listener must be added in its own scope
        // else its parameters will follow the native looping i instead
        for(int i = 0; i < flavors.Length; i++)
        {
            int temp = i;
            flavors[i].onClick.AddListener(delegate
            {
                FlavorClick(temp);
            });
        }
    }

    void FlavorClick(int idx)
    {
        store.selectedFlavor = idx;
        thisPage.SetActive(false); // hide this page
        nextPage.SetActive(true); // and show next page
    }
}
