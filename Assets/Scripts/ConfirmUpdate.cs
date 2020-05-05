using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUpdate : MonoBehaviour
{

    // Game Screen
    public Animator cameraAnimator;

    // public Button[] previousButtons;
    public Button buyButton;
    public GameObject insuffientBalance;

    // set size for each element and follow order as in MenuSelection.cs
    public GameObject[] flavorImg;
    public GameObject[] tempInfo;
    public GameObject[] flavorInfo;
    public GameObject[] toppingInfo;

    public Text remainingBalance;
    public Text toppingPrice;
    public Text totalPrice;

    public Button backButton;
    public GameObject thisPage;
    public GameObject prevPage;
    public GameObject nextScreen;

    MenuSelection store;
    DrinccStorage storage;
    AdminFunctions counters;
    int price;

    [System.NonSerialized]
    public bool update;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
        storage = GameObject.FindGameObjectWithTag("DrinccStorage").GetComponent<DrinccStorage>();
        counters = GameObject.FindGameObjectWithTag("CounterStore").GetComponent<AdminFunctions>();

        backButton.onClick.AddListener(BackPageClick);
        buyButton.onClick.AddListener(StartProcess);
    }

    void Update()
    {
        if(update == true)
        {
            PageUpdate();
            update = false;
        }
    }

    void PageUpdate()
    {
        remainingBalance.text = store.insertedMoney.ToString("#,.##0");

        // flavor image and info
        for (int i = 0; i < flavorImg.Length; i++)
        {
            if (i == store.selectedFlavor)
            {
                flavorImg[i].SetActive(true);
                flavorInfo[i].SetActive(true);
            }
            else
            {
                flavorImg[i].SetActive(false);
                flavorInfo[i].SetActive(false);
            }
        }

        // temp info
        for (int i = 0; i < tempInfo.Length; i++)
        {
            if (i == store.selectedTemp) tempInfo[i].SetActive(true);
            else tempInfo[i].SetActive(false);
        }

        //  topping info
        for (int i = 0; i < toppingInfo.Length; i++)
        {
            if (i == store.selectedTopping) toppingInfo[i].SetActive(true);
            else toppingInfo[i].SetActive(false);
        }

        if (store.selectedTopping < 2) // ice or no topping
        {
            toppingPrice.text = "0,00";
            totalPrice.text = "10.000,00";

            price = 10000;
        }
        else // everything else
        {
            toppingPrice.text = "5.000,00";
            totalPrice.text = "15.000,00";

            price = 15000;
        }

        // show or not based on money inserted
        if (store.insertedMoney < price)
        {
            insuffientBalance.SetActive(true);
            buyButton.interactable = false;
        }
        else
        {
            insuffientBalance.SetActive(false);
            buyButton.interactable = true;
        }
    }

    void UpdateBuyButtons()
    {
        remainingBalance.text = store.insertedMoney.ToString("#,.##0");

        // show or not based on money inserted
        if (store.insertedMoney < price)
        {
            insuffientBalance.SetActive(true);
            buyButton.interactable = false;
        }
        else
        {
            insuffientBalance.SetActive(false);
            buyButton.interactable = true;
        }
    }

    void BackPageClick()
    {
        thisPage.SetActive(false);
        prevPage.SetActive(true);
    }

    void StartProcess()
    {
        // decrement inserted money, selected flavor and topping
        store.insertedMoney -= price;
        storage.flavors[store.selectedFlavor]--;
        if (store.selectedTopping != 0)
        {
            storage.topping[store.selectedTopping]--;
        }

        counters.update = true;

        thisPage.SetActive(false);
        zoomOut();
        nextScreen.SetActive(true);

        store.startAnimation = true;
    }

    void zoomOut()
    {
        cameraAnimator.ResetTrigger("ZoomIn");
        cameraAnimator.SetTrigger("ZoomOut");
    }
}
