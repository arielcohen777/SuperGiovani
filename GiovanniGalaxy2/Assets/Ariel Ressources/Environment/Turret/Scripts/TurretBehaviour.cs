using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject projectile;
    public GameObject areaOfAttack;
    public GameObject muzzle;

    public LeverSO lever;

    public float radius;
    public float nextFireTime;

    public int cooldown;
    public int cooldownExtra;

    public bool activated;
    public bool isActive;
    public bool canShootAgain;

    private int index = 0;

    public int countdown;

    public List<GameObject> enemiesInArea;

    public GameObject topPivot;
    public GameObject bottomPivot;
    public GameObject interact;

    public Animator anim;

    // Start is called bef`ore the first frame update
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

        if (activated)
        {
            anim.SetBool("Activate", true);
            isActive = true;
            StartCoroutine(Timer());
            StartCoroutine(Cooldown());
        }

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
        activated = false;
        countdown = lever.timer;
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


        //// get the relative position of the player
        //Vector3 relativePos = go.transform.position - turretBottom.transform.position;

        //// get the rotation that points the forward direction towards the player
        //Quaternion lookRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        //// create a new Quaternion that only rotates on the y-axis
        //Quaternion yRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

        //// slerp between the current rotation and the desired rotation
        //float rotationSpeed = 5f; // adjust this to change the speed of rotation
        //turretBottom.transform.rotation = Quaternion.Slerp(turretBottom.transform.rotation, yRotation, Time.deltaTime * rotationSpeed);
    }
}
