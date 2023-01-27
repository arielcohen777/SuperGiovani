using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Instance
    private static GameManager instance;
    public GameObject player; // Player Ref

    //Coin Variables
    public TMP_Text coinText;
    public int coins = 0;
    private const string DEFAULT_COIN_TEXT = "Coins: ";
    [SerializeField] private AudioSource coinAudio;

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
