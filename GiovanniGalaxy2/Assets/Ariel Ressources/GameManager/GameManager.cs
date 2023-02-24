using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    
    //Player
    public GameObject player;
    public Health playerHealth;
    public StarterAssets.FirstPersonController fpc;
    public Camera cam;
    public Shoot shoot;
    public PlayerStuff playerStuff;

    //Inventory
    public ChangeGun changeGun;
    public InventoryObject inventory;
    public PlayerInventory pInv;
    public Interact interact;

    //UIs
    public GameObject crosshair;
    public WeaponUI wepUi;
    public BarUI barUi;

    #region Singleton

    //Instance
    private static GameManager instance;
    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
                Debug.LogError("Game Manager is NULL");
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        fpc = player.GetComponent<StarterAssets.FirstPersonController>();
    }
}
