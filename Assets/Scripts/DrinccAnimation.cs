using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinccAnimation : MonoBehaviour
{
    public Image[] animationSprite;
    public int lastFlavor;
    public int lastTemp;
    public int lastTopping;

    MenuSelection store;

    string[] flavors;
    string[] temps;
    string[] toppings;

    string spritePath = "Assets/Sprites/Animation/Drincc/Blueberry/";
    string targetPath;
    bool initial;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("SelectedStore").GetComponent<MenuSelection>();

        // for file access
        flavors = new string[8]{ "Chocolate", "Vanilla", "Strawberry", "Blueberry", "GreenTea", "Mint", "Lemon", "Coffee" };
        temps = new string[2]{"no-ice", "ice"}; // probably not used
        toppings = new string[7]{"no-topping", "ice", "boba", "sprinkles", "oreo", "whipcream", "chocolate-syrup"};
        initial = false;
    }

    void Update()
    {
        if(store.startAnimation)
        {
            if(initial == false)
            {
                targetPath = spritePath + flavors[store.selectedFlavor] + "/" + toppings[store.selectedTopping];
                if (store.selectedTopping == 2 || store.selectedTopping == 3) // if selected topping is not ice or no topping
                {
                    targetPath += "/" + temps[store.selectedTemp];
                }
                targetPath += "/";

                initial = true;
            }

            for(int i = 0; i < animationSprite.Length; i++)
            {
                string target = targetPath + flavors[store.selectedFlavor].ToLower() + "-" + i + ".png";
                animationSprite[i].sprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(target, typeof(Sprite));
            }

            // animation per deltaTime
        }
    }
}
