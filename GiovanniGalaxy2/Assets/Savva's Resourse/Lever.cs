using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Turret turret;
    public float activationTime = 5f;
    public float cooldownTime = 60f;

    private bool activated = false;
    private float activationCountdown = 0f;
    private float cooldownCountdown = 0f;

    void Update()
    {
        if (activated)
        {
            if (activationCountdown > 0f)
            {
                activationCountdown -= Time.deltaTime;
            }
            else
            {
                Deactivate();
            }
        }
        else if (cooldownCountdown > 0f)
        {
            cooldownCountdown -= Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            activated = true;
            activationCountdown = activationTime;
            turret.enabled = true;
        }
    }

    void Deactivate()
    {
        activated = false;
        cooldownCountdown = cooldownTime;
        turret.enabled = false;
    }
}
