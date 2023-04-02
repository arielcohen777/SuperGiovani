using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : MonoBehaviour
{
    [SerializeField] int nbPellets = 8;

    static GameManager gm;

    bool haveKilled;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void Shoot(Camera cam, GameObject impact)
    {
        WeaponSlot wep = gm.playerStuff.activeWeapon;

        for (int i = 1; i <= nbPellets; i++)
        {
            Vector3 direction = cam.transform.forward;
            Vector3 spread = Vector3.zero;
            spread += cam.transform.up * Random.Range(-1f, 1f);
            spread += cam.transform.right * Random.Range(-1f, 1f);

            direction += spread.normalized * Random.Range(0f, 0.2f);

            if (Physics.Raycast(cam.transform.position, 
                                direction,
                                out RaycastHit hit,
                                wep.weapon.range))
            {
                if (hit.transform.CompareTag("Enemy") && !haveKilled)
                {
                    haveKilled = true;
                    if (hit.collider.GetComponentInParent<Enemy1>() != null)
                        hit.collider.GetComponentInParent<Enemy1>().IsHit(wep.weapon.damage);
                    else
                        hit.collider.GetComponentInParent<Enemy2_fixed>().IsHit(wep.weapon.damage);
                    break;
                }

                else if (!hit.transform.CompareTag("Coin"))
                {
                    GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 5f);
                }
            }
        }
        haveKilled = false;
    }
}
