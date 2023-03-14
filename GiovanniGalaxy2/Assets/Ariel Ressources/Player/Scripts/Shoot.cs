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

	public LayerMask collisionLayerMask;

	[SerializeField] private GameObject rocket;
	private Shotgun sg;

	private bool canReload;

	private void Start()
	{
		gm = GameManager.Instance;
		anim = GetComponent<Animator>();
		sg = GetComponent<Shotgun>();
	}

	public void Shooting(Camera _mainCamera)
	{
		WeaponSlot wep = gm.playerStuff.activeWeapon;

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
		int randomNumberForMuzzelFlash = Random.Range(0, flashList.Length);
		holdFlash = Instantiate(flashList[randomNumberForMuzzelFlash], muzzleSpawn.transform.position /*- muzzelPosition*/, muzzleSpawn.transform.rotation * Quaternion.Euler(0, 0, 90));
		holdFlash.transform.parent = muzzleSpawn.transform;
		Destroy(holdFlash, 0.05f);

		//Weapon Sound (NEEDS TESTING)
		if (wep.weapon.gunshot != null)
			wep.weapon.gunshot.Play();

		//Shotgun
		if (wep.weapon.wepType.Equals(WeaponType.Shotgun))
		{
			sg.Shoot(_mainCamera, impact);
		}
		else if (wep.weapon.wepType.Equals(WeaponType.Bazooka))
        {
			Instantiate(rocket, muzzleSpawn.transform.position, muzzleSpawn.transform.rotation);
		}
		else
		{
			//Aiming
			if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit, wep.weapon.range))
			{
				if (hit.transform.CompareTag("Enemy"))
				{
					Enemy1 enemy = hit.collider.GetComponentInParent<Enemy1>();
					Enemy2 enemy2 = hit.collider.GetComponentInParent<Enemy2>();

					if (enemy != null) enemy.IsHit(wep.weapon.damage);
					else enemy2.IsHit(wep.weapon.damage);

					GameObject impactGO = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
					impactGO.GetComponent<ParticleSystem>().Play();
					Destroy(impactGO, 1f);

				}
				else if (!hit.transform.CompareTag("Coin"))
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
		//Reduce ammo
		wep.currentAmmo--;
	}

	public IEnumerator CanReload()
	{
		canReload = true;
		Invoke("Reload", 0.5f);
		yield return new WaitForSeconds(0.5f);
	}

	public void Reload()
	{
		if (canReload)
		{
			canReload = false;
		}
		else
			return;

		WeaponSlot wep = gm.playerStuff.activeWeapon;
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