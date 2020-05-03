using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinccStorage : MonoBehaviour
{
    // refer to MenuSelection.cs for element order

    public int[] flavors = new int[8]; // flavors, will be set to 15 for each
    public int[] topping = new int[7]; // topping, will be set to 30 for each

    void Start()
    {
        for (int i = 0; i < flavors.Length; i++) flavors[i] = 15;
        for (int i = 0; i < topping.Length; i++) topping[i] = 30;
    }
}
