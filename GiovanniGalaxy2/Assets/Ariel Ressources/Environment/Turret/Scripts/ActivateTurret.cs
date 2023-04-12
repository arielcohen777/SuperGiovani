using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTurret : MonoBehaviour
{
    public LeverSO lever;

    public TurretBehaviour tb;
 
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        lever = (LeverSO)GetComponent<Item>().item;
    }

    public void Activate()
    {
        if (gm.playerStuff.coins < lever.price)
            return;

        gm.playerStuff.coins -= lever.price;

        gm.playerStuff.UpdateCoinDisplay();
        tb.activated = true;
    }

}
