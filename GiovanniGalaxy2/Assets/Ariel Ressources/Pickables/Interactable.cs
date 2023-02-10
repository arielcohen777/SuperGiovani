using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private string toReturn;
    private GameManager gm;
    private ItemSO item;

    private void Start()
    {
        item = GetComponent<Item>().item;
        gm = GameManager.Instance;
    }

    private void Update()
    {
        toReturn = "Buy " + item.itemName
            + "\n" + item.price + " Coins";
        if (gm.playerStuff.coins < item.price)
            toReturn += "\nNot Enough Coins!";
    }
    
    public string ShowPrompt()
    {
        return toReturn;
    }

}