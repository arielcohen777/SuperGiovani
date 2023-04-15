using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject areaOfAttack;
    [SerializeField] private GameObject muzzle;

    [SerializeField] private LeverSO lever;

    [SerializeField] private float nextFireTime;

    private int cooldown;
    [SerializeField] private int cooldownExtra;

    //TurretActivated is used like a trigger while
    //isActive shows that the turret is active
    public bool turretActivated;
    public bool isActive;
    [SerializeField] private bool canShootAgain;

    [SerializeField] private int index = 0;

    public List<GameObject> enemiesInArea;

    [SerializeField] private GameObject topPivot;
    [SerializeField] private GameObject bottomPivot;
    [SerializeField] private GameObject interact;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInArea = new List<GameObject>();
        canShootAgain = true;
        cooldown = lever.timer + cooldownExtra;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesInArea.RemoveAll(en => en == null);

        if (index >= enemiesInArea.Count)
            index = 0;

        //If the lever has been pulled, activate turret
        if (turretActivated)
        {
            anim.SetBool("Activate", true);
            isActive = true;
            StartCoroutine(Timer());
            StartCoroutine(Cooldown());
        }

        //If there are no enemies in the list of enemies and it cannot shoot again,
        //don't do anything.
        if (enemiesInArea.Count == 0)
            return;

        if (!canShootAgain)
            return;

        if (isActive)
            TurretShoot();
    }

    void TurretShoot()
    {
        //Looks through the list of enemies in area of attack, rotates, and shoots rocket
        //then waits an amount of seconds till next fire.
        GameObject go = enemiesInArea[index];
        if (go != null)
        {
            RotateTurret(go);
            Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation);
        }
        StartCoroutine(NextFire());
    }

    IEnumerator NextFire()
    {
        canShootAgain = false;
        index++;
        yield return new WaitForSeconds(nextFireTime);
        canShootAgain = true;
    }

    IEnumerator Timer()
    {
        turretActivated = false;
        yield return new WaitForSeconds(lever.timer);
        enemiesInArea.Clear();
        isActive = false;
        anim.SetBool("Activate", false);
    }

    IEnumerator Cooldown()
    {
        interact.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        interact.SetActive(true);
    }

    void RotateTurret(GameObject go)
    {
        //Top of turret rotation
        topPivot.transform.LookAt(go.transform.position);
        //Bottom of turret rotation
        Vector3 relativePos = go.transform.position - bottomPivot.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        rotation.x = rotation.z = 0;
        bottomPivot.transform.rotation = rotation;
    }
}
