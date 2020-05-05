using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminFunctions : MonoBehaviour
{
    public Button[] flavorButtons;
    public Button[] toppingButtons;
    public Text[] flavorCounter;
    public Text[] toppingCounter;

    public GameObject[] pages;
    public GameObject back;
    public GameObject next;

    DrinccStorage storage;

    [System.NonSerialized]
    public bool update;
    int currPage;

    void Start()
    {
        storage = GameObject.FindGameObjectWithTag("DrinccStorage").GetComponent<DrinccStorage>();

        for (int i = 0; i < flavorButtons.Length; i++)
        {
            int temp = i;
            flavorButtons[i].onClick.AddListener(delegate{ FlavorAdd(temp); });
        }

        for (int i = 0; i < toppingButtons.Length; i++)
        {
            int temp = i;
            toppingButtons[i].onClick.AddListener(delegate { ToppingAdd(temp); });
        }

        currPage = 0;
        for(int i = 0; i < pages.Length; i++)
        {
            if (i == 0) pages[i].SetActive(true);
            else pages[i].SetActive(false);
        }

        back.SetActive(false);
        next.SetActive(true);

        back.GetComponent<Button>().onClick.AddListener(BackClick);
        next.GetComponent<Button>().onClick.AddListener(NextClick);

        update = true;
    }

    void Update()
    {
        if (update == true) UpdateCounters();
    }

    void FlavorAdd(int idx)
    {
        storage.flavors[idx] += 10;
        flavorCounter[idx].text = storage.flavors[idx].ToString();
    }

    void ToppingAdd(int idx)
    {
        storage.topping[idx + 1] += 10;
        toppingCounter[idx].text = storage.topping[idx + 1].ToString();
    }

    void UpdateCounters()
    {
        for (int i = 0; i < flavorCounter.Length; i++) flavorCounter[i].text = storage.flavors[i].ToString();
        for (int i = 0; i < toppingCounter.Length; i++) toppingCounter[i].text = storage.topping[i + 1].ToString();
    }

    void BackClick()
    {
        if (currPage != 0)
        {
            pages[currPage--].SetActive(false);
            pages[currPage].SetActive(true);

            next.SetActive(true);
            if (currPage == 0) back.SetActive(false);
        }
        else Debug.Log("This is the first page!");
    }

    void NextClick()
    {
        if (currPage != pages.Length - 1)
        {
            pages[currPage++].SetActive(false);
            pages[currPage].SetActive(true);

            back.SetActive(true);
            if (currPage == pages.Length - 1) next.SetActive(false);
        }
        else Debug.Log("This is the last page!");
    }
}
