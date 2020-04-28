using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyClick : MonoBehaviour
{
    public Animator windowAnimator;

    public Button limaRibu;
    public Button sepuluhRibu;
    public Button duaPuluhRibu;

    MenuSelection store;
    public GameObject confirmationPage;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        // money listener
        limaRibu.onClick.AddListener(delegate { addMoney(5000); windowAnimator.SetTrigger("5KInserted"); });
        sepuluhRibu.onClick.AddListener(delegate { addMoney(10000); windowAnimator.SetTrigger("10KInserted"); });
        duaPuluhRibu.onClick.AddListener(delegate { addMoney(20000); windowAnimator.SetTrigger("20KInserted"); });
    }

    void addMoney(int amount)
    {
        store.insertedMoney += amount;
        if(confirmationPage.activeSelf) confirmationPage.GetComponent<ConfirmUpdate>().update = true;
    }
}
