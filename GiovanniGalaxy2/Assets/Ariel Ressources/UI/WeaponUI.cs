using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text ammoCount;
    [SerializeField] private Text ammoMax;
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
        ammoCount.text = wep.ammoCount.ToString();
        ammoMax.text = wep.ammoMax.ToString();
    }
}
