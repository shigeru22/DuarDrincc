using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinccAnimation : MonoBehaviour
{
    public GameObject mainObject; // set to DrinccObject
    public GameObject[] animationSprite; // set to 8 and set elements to each Drincc-* object

    MenuSelection store;

    string[] flavors;
    string[] temps;
    string[] toppings;

    string spritePath = "Assets/Sprites/Animation/Drincc/";
    string targetPath;
    bool initial;
    bool only7frames;

    int run;
    float time;
    int activeObject;
    int frameCounter;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        // for file access
        flavors = new string[8]{ "Chocolate", "Milk", "Strawberry", "Blueberry", "GreenTea", "Mint", "Lemon", "Coffee" };
        temps = new string[2]{"no-ice", "ice"};
        toppings = new string[7]{"no-topping", "ice", "boba", "sparkles", "oreo", "whipcream", "chocolate-syrup"};

        initial = false;
        only7frames = false;

        run = 0;
        time = 0;
        activeObject = 0;
        frameCounter = 0;
    }

    void Update()
    {
        // i know it's inefficient, but this method is far easier than using animator
        if(store.startAnimation == true)
        {
            if(initial == false)
            {
                targetPath = spritePath + flavors[store.selectedFlavor] + "/" + toppings[store.selectedTopping];
                Debug.Log(targetPath);

                if (store.selectedTopping >= 2) // if selected topping is not ice or no topping
                {
                    targetPath += "/" + temps[store.selectedTemp];
                }
                targetPath += "/";

                Debug.Log(targetPath);

                // small note, ice cube, boba, and no topping only uses 7 frames
                if (store.selectedTopping < 3) only7frames = true;
                for (int i = 0; i < animationSprite.Length; i++)
                {
                    string target = targetPath + flavors[store.selectedFlavor].ToLower() + "-" + i + ".png";
                    animationSprite[i].GetComponent<Image>().sprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(target, typeof(Sprite));

                    if (only7frames == true && i == 6) break;
                }

                initial = true;
            }
            else
            {
                if (run == 0)
                {
                    mainObject.SetActive(true);
                    for (int i = 0; i < animationSprite.Length; i++)
                    {
                        if (i == 0) animationSprite[i].SetActive(true);
                        else animationSprite[i].SetActive(false);
                    }

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
                    // steam animation
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
}
