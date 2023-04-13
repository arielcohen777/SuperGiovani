using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSound : MonoBehaviour
{
    public AudioSource reload;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        reload = GetComponent<AudioSource>();
    }

    public void PlayReload()
    {
        if (gm.playerStuff.activeWeapon.weaponSo.reload != null)
        {
            reload.Play();
        }
    }

    public float GetReloadTime()
    {
        reload.clip = gm.playerStuff.activeWeapon.weaponSo.reload.GetComponent<AudioSource>().clip;

        if (gm.playerStuff.activeWeapon.weaponSo.reload != null)
            return reload.clip.length;

        return 0;
    }
}
