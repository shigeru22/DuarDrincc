using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSelect : MonoBehaviour
{
    public Button[] temps; // set size to 2 and add 0 for hot and 1 to cold
    public Button backButton;

    public GameObject prevPage; // set to MenuSelect
    public GameObject thisPage; // set to TempSelect
    public GameObject nextPage; // set to ToppingSelect

    MenuSelection store;

    void Start()
    {
        // find gameobject with tag "SelectedStore", which is Menus, and access this component
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
        backButton.onClick.AddListener(BackPageClick);

        // same as FlavorSelect.cs, add variable in its own scope
        for (int i = 0; i < temps.Length; i++)
        {
            int temp = i;
            temps[i].onClick.AddListener(delegate
            {
                TempClick(temp);
            });
        }
    }

    void TempClick(int idx)
    {
        store.selectedTemp = idx;
        thisPage.SetActive(false); // hide this page
        nextPage.SetActive(true); // and show next page
    }

    void BackPageClick()
    {
        thisPage.SetActive(false);
        prevPage.SetActive(true);
    }
}
