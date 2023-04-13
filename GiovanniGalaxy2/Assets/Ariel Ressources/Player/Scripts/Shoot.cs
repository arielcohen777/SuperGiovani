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
	[SerializeField] private ReloadSound reloadSound;
	public float reloadTimer;
	private bool reloading = false;


	public AudioSource gunshot;

	public LayerMask collisionLayerMask;

	[SerializeField] private GameObject rocket;
	private Shotgun sg;

	private bool canReload;

	private void Start()
	{
		gm = GameManager.Instance;
		gunshot = gm.cam.GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		sg = GetComponent<Shotgun>();
	}

	public void Shooting(Camera _mainCamera)
	{
		WeaponSlot wep = gm.playerStuff.activeWeapon;

		// If out of ammo, reload
		if (wep.currentAmmo <= 0)
		{
			wep.currentAmmo = 0;
			StartCoroutine(CanReload());
			return;
		}

		// Gunshot Sound
		if (gm.playerStuff.activeWeapon.gunshot != null)
		{
			gunshot.Play();
		}

		// Shooting Anim
		anim.SetTrigger("Shooting");

		// Muzzle Flash
		int randomNumberForMuzzelFlash = Random.Range(0, flashList.Length);
		holdFlash = Instantiate(flashList[randomNumberForMuzzelFlash], muzzleSpawn.transform.position /*- muzzelPosition*/, muzzleSpawn.transform.rotation * Quaternion.Euler(0, 0, 90));
		holdFlash.transform.parent = muzzleSpawn.transform;
		Destroy(holdFlash, 0.05f);

		// Shotgun
		if (wep.weaponSo.wepType.Equals(WeaponType.Shotgun))
		{
			sg.Shoot(_mainCamera, impact);
		}
		// Bazooka
		else if (wep.weaponSo.wepType.Equals(WeaponType.Bazooka))
        {
			Instantiate(rocket, muzzleSpawn.transform.position, muzzleSpawn.transform.rotation);
		}
		// Rest of guns  (UZI, Rifle, Sniper)
		else
		{
			//Aiming
			if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out RaycastHit hit, wep.range))
			{
				if (hit.transform.CompareTag("Enemy"))
				{
					Enemy1 enemy = hit.collider.GetComponentInParent<Enemy1>();
					Enemy2_fixed enemy2 = hit.collider.GetComponentInParent<Enemy2_fixed>();

					if (enemy != null) enemy.IsHit(wep.damage);
					else enemy2.IsHit(wep.damage);

					GameObject impactGO = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
					impactGO.GetComponent<ParticleSystem>().Play();
					Destroy(impactGO, 1f);

				}
				else if (!hit.transform.CompareTag("Coin") && !hit.transform.CompareTag("Turret"))
				{
					Debug.Log(hit.transform.tag);
					GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
					Destroy(impactGO, 1f);
				}
			}
		}

		//Reduce ammo
		wep.currentAmmo--;
	}

	public IEnumerator CanReload()
	{
		
		canReload = true;
		reloadTimer = reloadSound.GetReloadTime();
		if (!reloading)
		{
			reloadSound.PlayReload();
			anim.SetBool("Reloading", true);
			reloading = true;
		}
		yield return new WaitForSeconds(reloadTimer);
		reloading = false;
		anim.SetBool("Reloading", false);
		Reload();
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