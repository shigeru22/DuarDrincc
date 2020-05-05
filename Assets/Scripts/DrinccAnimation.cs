using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DrinccAnimation : MonoBehaviour
{
    public GameObject mainObject; // set to DrinccObject
    public GameObject[] animationSprite; // set to 8 and set elements to each Drincc-* object
    public GameObject[] flavorInfo;
    public GameObject[] tempInfo;
    public GameObject[] toppingInfo;
    public GameObject steam; // set to SteamAnimation
    public Button door; // set to Door
    public GameObject change; // set to Change
    public Button changeHolder; // set to ChangePlaceholder

    public GameObject thisPage;
    public GameObject processing;
    public GameObject takeDrincc;
    public GameObject takeChange;
    public GameObject firstPage;

    public Animator change5k;
    public Animator change10k;

    MenuSelection store;

    string[] flavors;
    string[] temps;
    string[] toppings;

    string spritePath = "Animation/Drincc/";
    string targetPath;
    bool initial;
    bool only7frames;
    bool doorClickable;

    int run;
    float time;
    int activeObject;
    int frameCounter;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        door.onClick.AddListener(TakeDrincc);
        changeHolder.onClick.AddListener(TakeChange);

        // for file access
        flavors = new string[8]{ "Chocolate", "Milk", "Strawberry", "Blueberry", "GreenTea", "Mint", "Lemon", "Coffee" };
        temps = new string[2]{"no-ice", "ice"};
        toppings = new string[7]{"no-topping", "ice", "boba", "sparkles", "oreo", "whipcream", "chocolate-syrup"};

        initial = false;
        only7frames = false;
    }

    void Update()
    {
        // i know it's inefficient, but this method is far easier than using animator
        if(store.startAnimation == true)
        {
            if(initial == false)
            {
                targetPath = spritePath + flavors[store.selectedFlavor] + "/" + toppings[store.selectedTopping];

                if (store.selectedTopping >= 2) // if selected topping is not ice or no topping
                {
                    targetPath += "/" + temps[store.selectedTemp];
                }
                targetPath += "/";

                // hide every drincc objects
                for (int i = 0; i < animationSprite.Length; i++) animationSprite[i].SetActive(false);

                // small note, ice cube, boba, and no topping only uses 7 frames
                if (store.selectedTopping < 3) only7frames = true;
                for (int i = 0; i < animationSprite.Length; i++)
                {
                    string target = targetPath + flavors[store.selectedFlavor].ToLower() + "-" + i;
                    animationSprite[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(target);

                    if (only7frames == true && i == 6) break;
                }

                // show only selected menus
                for(int i = 0; i < flavorInfo.Length; i++)
                {
                    if (i == store.selectedFlavor) flavorInfo[i].SetActive(true);
                    else flavorInfo[i].SetActive(false);
                }

                for(int i = 0; i < tempInfo.Length; i++)
                {
                    if (i == store.selectedTemp) tempInfo[i].SetActive(true);
                    else tempInfo[i].SetActive(false);
                }

                for(int i = 0; i < toppingInfo.Length; i++)
                {
                    if (i == store.selectedTopping) toppingInfo[i].SetActive(true);
                    else toppingInfo[i].SetActive(false);
                }

                processing.SetActive(true);
                takeChange.SetActive(false);

                frameCounter = 0;
                run = 0;
                initial = true;
                time = 0;
                activeObject = 0;
            }
            else
            {
                if (run == 0)
                {
                    mainObject.SetActive(true);
                    animationSprite[0].SetActive(true);
                    for (int i = 1; i < animationSprite.Length; i++) animationSprite[i].SetActive(false);

                    run++;
                }
                else if (run == 1)
                {
                    time += Time.deltaTime;

                    if (time > 3f)
                    {
                        run++;
                        time = 0;
                    }
                }
                else if (run == 2)
                {
                    time += Time.deltaTime;

                    if (time / 0.5f > 1)
                    {
                        IncrementAnimation();
                        frameCounter++;
                        time -= 0.5f;

                        if (frameCounter == 6 & only7frames == true) run++;
                        if (frameCounter == 7) run++;
                    }
                }
                else if (run == 3)
                {
                    if (store.selectedTemp == 0)
                    {
                        steam.SetActive(true);
                        steam.GetComponent<Animator>().SetTrigger("runAnimation");
                    }

                    processing.SetActive(false);
                    takeDrincc.SetActive(true);

                    door.interactable = true;
                    run++;
                }
                // 4 skipped in order to wait for door click
                else if(run == 5)
                {
                    processing.SetActive(false);
                    takeChange.SetActive(true);

                    // change animation
                    if (store.insertedMoney < 10000) change5k.SetTrigger("Run");
                    else change10k.SetTrigger("Run");

                    store.insertedMoney = 0;

                    changeHolder.interactable = true;
                    run++;
                }
                // 6 skipped in order to wait for change click
                else if(run == 7)
                {
                    store.startAnimation = false;
                    initial = false;

                    takeChange.SetActive(false);
                    thisPage.SetActive(false);
                    firstPage.SetActive(true);
                }
                else if(run == 4 || run == 6)
                {
                    // do nothing
                }
                else
                {
                    Debug.Log("Wrong run value!");
                    store.startAnimation = false;
                    initial = false;
                }
            }
        }
    }

    void IncrementAnimation()
    {
        animationSprite[activeObject++].SetActive(false);
        animationSprite[activeObject].SetActive(true);
    }

    void TakeDrincc()
    {
        if (store.selectedTemp == 1) steam.GetComponent<Animator>().SetTrigger("glassClicked");
        mainObject.SetActive(false);
        takeDrincc.SetActive(false);

        door.interactable = false;

        if (store.insertedMoney != 0) run++;
        else run = 7;
    }

    void TakeChange()
    {
        if (store.insertedMoney < 10000) change5k.SetTrigger("Click");
        else change10k.SetTrigger("Click");
        change.SetActive(false);
        changeHolder.interactable = false;
        run++;
    }
}
