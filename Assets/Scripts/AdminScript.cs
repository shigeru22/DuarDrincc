using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminScript : MonoBehaviour
{
    private string[] konamiCode = { "UpArrow", "UpArrow", "DownArrow", "DownArrow", "LeftArrow", "RightArrow", "LeftArrow", "RightArrow", "B", "A", "Return" };
    private int currPos = 0;

    public GameObject thisPage;
    public GameObject adminMenu;
    public Button adminBack;

    MenuSelection store;
    CameraControl cameraAnimator;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();
        cameraAnimator = GameObject.FindGameObjectWithTag("CameraTrigger").GetComponent<CameraControl>();

        adminBack.onClick.AddListener(CloseAdminMenu);
    }

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
            if (currPos == konamiCode.Length)
            {
                OpenAdminMenu();
                currPos = 0;
            }
        }
        else
        {
            if (currPos != 0) Debug.Log("Konami code failed in " + (currPos + 1));
            currPos = 0;
        }
    }

    void OpenAdminMenu()
    {
        cameraAnimator.trigger = true;

        thisPage.SetActive(false);
        adminMenu.SetActive(true);
    }

    void CloseAdminMenu()
    {
        if (store.insertedMoney == 0) cameraAnimator.trigger = true;
        adminMenu.SetActive(false);
        thisPage.SetActive(true);
    }
}
