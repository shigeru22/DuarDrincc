using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorSlideShow : MonoBehaviour
{
    public GameObject[] flavor;
    int flavorCount;
    int counter;
    float time;

    void Start()
    {
        flavorCount = flavor.Length;
        counter = 0;
        time = 0;

        // first run, set first flavor to active and others to inactive
        flavor[counter].SetActive(true);
        for (int i = 1; i < flavorCount; i++) flavor[i].SetActive(false);
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1f)
        {
            flavor[counter++].SetActive(false); // disable current (counter) object and increment counter

            if (counter == flavorCount) counter = 0; // if current counter equals flavorCount size, change counter value to zero
            flavor[counter].SetActive(true); // enable current (counter) object

            time -= 1;
        }
    }
}
