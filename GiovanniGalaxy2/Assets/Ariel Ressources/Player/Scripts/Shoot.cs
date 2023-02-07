using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameObject[] flashList;
	public GameObject muzzleSpawn;
	private GameObject holdFlash;
	public Animator anim;
	[SerializeField] private GameObject impact;
	[SerializeField] private GameObject blood;

	private bool canReload;

	private void Start()
    {
        gm = GameManager.Instance;
		anim = GetComponent<Animator>();
    }

	public void Shooting(Camera _mainCamera)
	{
		WeaponSlot wep = gm.activeWeapon;
		
		//Reduce ammo
		wep.currentAmmo--;

		//If out of ammo, reload
		if (wep.currentAmmo <= 0)
		{
			wep.currentAmmo = 0;
			StartCoroutine(CanReload());
			return;
		}

		//Gun anim
		anim.SetTrigger("Shooting");

		//Muzzle Flash
		int randomNumberForMuzzelFlash = Random.Range(0, 5);
		holdFlash = Instantiate(flashList[randomNumberForMuzzelFlash], muzzleSpawn.transform.position /*- muzzelPosition*/, muzzleSpawn.transform.rotation * Quaternion.Euler(0, 0, 90));
		holdFlash.transform.parent = muzzleSpawn.transform;

		//Weapon Sound (NEEDS TESTING)
		if (wep.weapon.gunshot != null)
		{
			wep.weapon.gunshot.Play();
		}

		//Aiming
		if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit, wep.weapon.range))
		{
			Debug.Log(hit.transform.name);

			//Making sure it's an enemy
			Enemy2 target = hit.transform.GetComponent<Enemy2>();
			if (target != null)
			{
				target.IsHit((int)wep.weapon.damage);
			}

			Debug.Log(hit.transform.gameObject.tag);
			if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Dummie"))
			{
				GameObject impactGO = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
				impactGO.GetComponent<ParticleSystem>().Play();
			}
			else
            {
				GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(impactGO, 1f);
			}

			//Weapon Sound (NEEDS TESTING)
			if (wep.weapon.gunshot != null)
			{
				wep.weapon.gunshot.Play();
			}
		}
	}

	public IEnumerator CanReload()
    {
		canReload = true;
		Invoke("Reload", 0.5f);
		yield return new WaitForSeconds(0.5f);
    }

	public void Reload()
	{
		if (canReload) { 
			canReload = false;
		}
		else
			return;

		WeaponSlot wep = gm.activeWeapon;
		//If currentAmmo is the same as magsize, don't reload
		if (wep.currentAmmo == wep.magSize)
		{
			return;
		}

		//If no ammo in maxammo, don't reload
		if (wep.maxAmmo == 0)
		{
			return;
		}

        int toReload;
		//If there is less ammo than a mag, reload maxAmmo
		if (wep.magSize > wep.maxAmmo)
		{
			toReload = wep.maxAmmo;
		}
		//Else reload amount necessary for a full mag
		else
		{
			toReload = wep.magSize - wep.currentAmmo;
		}

        wep.currentAmmo += toReload;
		
		//Set to 0 if it goes lower
		if ((wep.maxAmmo -= toReload) < 0)
			wep.maxAmmo = 0;

		if (wep.currentAmmo > wep.magSize)
			wep.currentAmmo = wep.magSize;

		gm.wepUi.UpdateWeaponHud();
	}
}
