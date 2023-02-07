using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text currentAmmo;
    [SerializeField] private Text magSize;
    [SerializeField] private Text maxAmmo;
    
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    public void UpdateWeaponHud()
    {
        WeaponSlot wep = gm.activeWeapon;
        currentAmmo.text = wep.currentAmmo.ToString();
        maxAmmo.text = wep.maxAmmo.ToString();
        magSize.text = wep.magSize.ToString();
    }
}
