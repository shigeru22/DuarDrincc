using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageControl : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject back;
    public GameObject next;
    int currPage;

    void Start()
    {
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

    void BackClick()
    {
        if (currPage != 0)
        {
            pages[currPage--].SetActive(false); // set current page to inactive and decrement currPage
            pages[currPage].SetActive(true); // set (decremented) currPage to active

            // show next button and if first page hide back button
            next.SetActive(true);
            if (currPage == 0) back.SetActive(false);
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
        }
        else Debug.Log("This is the last page!");
    }
}
