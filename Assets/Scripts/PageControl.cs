using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageControl : MonoBehaviour
{
    public Button[] flavors;
    public GameObject[] pages;
    public GameObject[] soldOuts;

    public GameObject back;
    public GameObject next;
    int currPage;

    MenuSelection store;
    DrinccStorage storage;

    [System.NonSerialized]
    public bool runOnce = false; // so update part won't keep looping

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
        storage = GameObject.FindGameObjectWithTag("DrinccStorage").GetComponent<DrinccStorage>();

        // show first page and hide other pages
        currPage = 0;
        pages[currPage].SetActive(true);
        for (int i = currPage + 1; i < pages.Length; i++) pages[i].SetActive(false);

        // show only next button
        back.SetActive(false);
        next.SetActive(true);

        // add listener to back and next buttons
        back.GetComponent<Button>().onClick.AddListener(BackClick);
        next.GetComponent<Button>().onClick.AddListener(NextClick);
    }

    void Update()
    {
        if(store.insertedMoney > 0 && runOnce == false)
        {
            SoldOutUpdate();
            runOnce = true;
        }
        if(store.insertedMoney == 0 && runOnce == true)
        {
            SoldOutUpdate();
            runOnce = false;
        }
    }

    void BackClick()
    {
        if (currPage != 0)
        {
            pages[currPage--].SetActive(false); // set current page to inactive and decrement currPage
            pages[currPage].SetActive(true); // set (decremented) currPage to active

            // show next button and if first page hide back button
            next.SetActive(true);
            if (currPage == 0) back.SetActive(false);

            SoldOutUpdate();
        }
        else Debug.Log("This is the first page!");
    }

    void NextClick()
    {
        if (currPage != pages.Length - 1)
        {
            pages[currPage++].SetActive(false); // set current page to inactive and increment currPage
            pages[currPage].SetActive(true); // set (incremented) currPage to active

            // show back button and if last page hide next button
            back.SetActive(true);
            if (currPage == pages.Length - 1) next.SetActive(false);

            SoldOutUpdate();
        }
        else Debug.Log("This is the last page!");
    }

    void SoldOutUpdate()
    {
        for (int i = 0; i < soldOuts.Length; i++)
        {
            if (storage.flavors[(((currPage + 1) * 4) - 1) - i] == 0)
            {
                flavors[(((currPage + 1) * 4) - 1) - i].interactable = false;
                soldOuts[soldOuts.Length - 1 - (i % soldOuts.Length)].SetActive(true);
            }
            else
            {
                flavors[(((currPage + 1) * 4) - 1) - i].interactable = true;
                soldOuts[soldOuts.Length - 1 - (i % soldOuts.Length)].SetActive(false);
            }
        }
    }
}
