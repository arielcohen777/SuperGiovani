using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //Instance
    private static GameManager instance;
    
    //Coin Variables
    public TMP_Text coinText;
    public int coins = 0;
    private const string DEFAULT_COIN_TEXT = "Coins: ";
    [SerializeField] private AudioSource coinAudio;

    //Player
    public GameObject player;
    public GameObject playerHealth;
    public StarterAssets.FirstPersonController fpc;
    public Camera cam;
    public Shoot shoot;

    //Inventory
    public ChangeGun changeGun;
    public InventoryObject inventory;
    public WeaponSlot activeWeapon;
    public GameObject crosshair;
    public PlayerInventory pInv;
    public Interact interact;

    //UIs
    public WeaponUI wepUi;
    public BarUI barUi;

    #region Singleton
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

    #region Coins

    private void Start()
    {
        UpdateCoinDisplay();
    }

    public void UpdateCoin()
    {
        coins++;
        coinAudio.Play();
        UpdateCoinDisplay();
    }

    public void UpdateCoinDisplay()
    {
        string v = DEFAULT_COIN_TEXT + coins;
        coinText.text = v;
    }
    #endregion
}
