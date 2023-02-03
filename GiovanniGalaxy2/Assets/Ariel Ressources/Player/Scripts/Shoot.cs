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

	private void Start()
    {
        gm = GameManager.Instance;
		anim = GetComponent<Animator>();
    }

	public void Shooting(Camera _mainCamera)
	{
		WeaponSlot wep = gm.activeWeapon;

		Debug.Log("Current Ammo: " + wep.currentAmmo + " | Mag Size: " + wep.magSize + " | Max Ammo: " + wep.maxAmmo);

		if (wep.currentAmmo <= 0)
		{
			ReloadInvoke();
			return;
		}

		gm.shoot.anim.SetBool("Shooting", true);
		wep.currentAmmo--;

		int randomNumberForMuzzelFlash = Random.Range(0, 5);
		//Debug.Log("Shooting now");
		//Muzzle Flash
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
			if (!hit.transform.CompareTag("Enemy"))
			{
				GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(impactGO, 1f);
			}
			else
            {
				GameObject impactGO = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
				impactGO.GetComponent<ParticleSystem>().Play();
            }
			

			//Weapon Sound (NEEDS TESTING)
			if (wep.weapon.gunshot != null)
			{
				wep.weapon.gunshot.Play();
			}
		}
	}

	public void ReloadInvoke() 
	{
		Invoke("Reload", 0.3f);
	}
	public void Reload()
	{
		WeaponSlot wep = gm.activeWeapon;
		Debug.Log("Reloading");
		//If currentAmmo is the same as magsize, don't reload
		if (wep.currentAmmo == wep.magSize)
			return;

		//If no ammo in maxammo, don't reload
		if (wep.maxAmmo == 0)
			return;
		
		//If there is more ammo than a mag size, reload mag size
		if (wep.maxAmmo > wep.magSize)
			wep.currentAmmo = wep.magSize;
		//If there is less ammo than a mag size, reload rest of ammo
		else
			wep.currentAmmo = wep.maxAmmo;
		//Reduce Max ammo
		wep.maxAmmo -= wep.magSize;
		
		//Set to 0 if it goes lower
		if (wep.maxAmmo < 0)
			wep.maxAmmo = 0;

		gm.wepUi.UpdateWeaponHud();
	}
}
