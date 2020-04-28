using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinccAnimation : MonoBehaviour
{
    public Image[] animationSprite;

    MenuSelection store;

    string[] flavors;
    string[] temps;
    string[] toppings;

    string spritePath = "Assets/Sprites/Animation/Drincc/";
    string targetPath;
    bool initial;
    bool only7frames;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        // for file access
        flavors = new string[8]{ "Chocolate", "Milk", "Strawberry", "Blueberry", "GreenTea", "Mint", "Lemon", "Coffee" };
        temps = new string[2]{"no-ice", "ice"};
        toppings = new string[7]{"no-topping", "ice", "boba", "sprinkles", "oreo", "whipcream", "chocolate-syrup"};
        initial = false;
        only7frames = false;
    }

    void Update()
    {
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
                    animationSprite[i].sprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(target, typeof(Sprite));

                    if (only7frames == true && i == 6)
                    {
                        Debug.Log("Yeah, it's 6 frames");
                        break;
                    }
                }

                initial = true;
            }

            // animation per deltaTime
        }
    }
}
