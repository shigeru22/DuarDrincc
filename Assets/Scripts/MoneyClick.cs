using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyClick : MonoBehaviour
{
    public Button seribu;
    public Button duaRibu;
    public Button limaRibu;
    public Button sepuluhRibu;

    MenuSelection store;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        // money listener
        seribu.onClick.AddListener(delegate { addMoney(1000); });
        duaRibu.onClick.AddListener(delegate { addMoney(2000); });
        limaRibu.onClick.AddListener(delegate { addMoney(5000); });
        sepuluhRibu.onClick.AddListener(delegate { addMoney(10000); });
    }

    void addMoney(int amount)
    {
        store.insertedMoney += amount;
    }
}
