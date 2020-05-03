using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminScript : MonoBehaviour
{
    private string[] konamiCode = { "UpArrow", "UpArrow", "DownArrow", "DownArrow", "LeftArrow", "RightArrow", "LeftArrow", "RightArrow", "B", "A", "Return" };
    private int currPos = 0;
 
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && Input.anyKeyDown && e.keyCode.ToString() != "None") Insert(e.keyCode);
    }

    void Insert(KeyCode s)
    {
        if (s.ToString() == konamiCode[currPos])
        {
            currPos++;
            if ((currPos + 1) > konamiCode.Length)
            {
                RunEasterEgg();
                currPos = 0;
            }
        }
        else
        {
            if (currPos != 0) Debug.Log("Konami code failed in " + (currPos + 1));
            currPos = 0;
        }
    }

    void RunEasterEgg()
    {
        Debug.Log("Hello, world!");
    }
}
