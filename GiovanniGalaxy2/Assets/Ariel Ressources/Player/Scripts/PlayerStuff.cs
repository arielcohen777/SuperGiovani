using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayerStuff : MonoBehaviour
{
    public WeaponSlot activeWeapon;

    //Coin Variables
    public TMP_Text coinText;
    public int coins = 0;
    private const string DEFAULT_COIN_TEXT = "Coins: ";
    [SerializeField] private AudioSource coinAudio;

    // Start is called before the first frame update
    void Start()
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

}
