using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowClick : MonoBehaviour
{
    public Animator windowAnimator;
    public Button slot;

    MenuSelection store;

    bool open;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        slot.onClick.AddListener(Trigger);
    }

    void Update()
    {
        if(store.insertedMoney >= 30000 && open == true)
        {
            windowAnimator.SetTrigger("SlotClicked");
            open = false;
        }
    }

    void Trigger()
    {
        if (store.insertedMoney < 30000)
        {
            windowAnimator.SetTrigger("SlotClicked");

            if (open == true) open = false;
            else open = true;
        }
    }
}
