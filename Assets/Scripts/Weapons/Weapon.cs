using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{


    public WeaponBehaviour behaviour;

    protected float cooldown;
    protected float burstCooldown;

    protected float cyclingTimer;
    protected float burstTimer;

    protected int burstsLeft;


    // Use this for initialization
    protected virtual void Start()
    {
        // Get a cooldown timer from the RPM requested

        SetCooldown();

    }

    // Update is called once per frame
    protected virtual void Update()
    {

        Debug.DrawRay(GetOrigin(), GetDirection()*20, Color.green, 0.09f);

        if (IsFiring())
        {
            if (burstCooldown == 0)
            {
                for (int i = 0; i < behaviour.burstAmount; i++)
                {
                    Fire();

                }
            } else { 

                Fire();
            }
        }

        if (behaviour.firingMode == WeaponBehaviour.FiringMode.AUTOMATIC_BURST || behaviour.firingMode == WeaponBehaviour.FiringMode.BURST)
        {
            if (burstsLeft > 0 && (burstTimer <= 0))
            {
                Fire();
            }
        }

        cyclingTimer -= Time.deltaTime;
        burstTimer -= Time.deltaTime;
    }

    public virtual void Fire()
    {
        burstsLeft--;
        burstTimer = burstCooldown;
    }

    bool IsFiring()
    {
        if (behaviour.firingMode == WeaponBehaviour.FiringMode.SEMI_AUTOMATIC)
        {
            if (Input.GetButtonDown(behaviour.axis) && cyclingTimer <= 0)
            {
                cyclingTimer = cooldown;
                return true;
            }
        }

        else if (behaviour.firingMode == WeaponBehaviour.FiringMode.AUTOMATIC)
        {
            if (Input.GetButton(behaviour.axis) && cyclingTimer <= 0)
            {
                cyclingTimer = cooldown;
                return true;
            }
        }

        else if (behaviour.firingMode == WeaponBehaviour.FiringMode.BURST)
        {
            if (Input.GetButtonDown(behaviour.axis) && cyclingTimer <= 0)
            {
                cyclingTimer = cooldown;
                burstsLeft = behaviour.burstAmount;
                return true;
            }
        }

        else if (behaviour.firingMode == WeaponBehaviour.FiringMode.AUTOMATIC_BURST)
        {
            if (Input.GetButton(behaviour.axis) && cyclingTimer <= 0)
            {
                cyclingTimer = cooldown;
                burstsLeft = behaviour.burstAmount;
                return true;
            }
        }

        return false;
    }


    void SetCooldown()
    {
        if (behaviour.firingMode == WeaponBehaviour.FiringMode.AUTOMATIC)
        {
            cooldown = behaviour.recyclingTime;
        }

        else if (behaviour.firingMode == WeaponBehaviour.FiringMode.SEMI_AUTOMATIC)
        {
            cooldown = behaviour.recyclingTime;
        }

        else if (behaviour.firingMode == WeaponBehaviour.FiringMode.BURST)
        {
            burstCooldown = behaviour.burstRecyclingTime;
            cooldown = behaviour.recyclingTime;

        }

        else if (behaviour.firingMode == WeaponBehaviour.FiringMode.AUTOMATIC_BURST)
        {
            burstCooldown = behaviour.burstRecyclingTime;
            cooldown = behaviour.recyclingTime;
        }
    }

    protected Vector3 GetOrigin()
    {
        Vector3 origin = Vector3.zero;


        if (behaviour.directionOrigin == WeaponBehaviour.DirectionOrigin.TRANSFORM)
        {
            origin = transform.position;
            origin += transform.forward.normalized * behaviour.originOffset.z;
            origin += transform.up.normalized * behaviour.originOffset.y;
            origin += transform.right.normalized * behaviour.originOffset.x;
        }

        else if (behaviour.directionOrigin == WeaponBehaviour.DirectionOrigin.CAMERA)
        {
            origin = Camera.main.transform.position;

            origin += Camera.main.transform.forward.normalized * behaviour.originOffset.z;
            origin += Camera.main.transform.up.normalized * behaviour.originOffset.y;
            origin += Camera.main.transform.right.normalized * behaviour.originOffset.x;

        }

        return origin;

    }

    protected Vector3 GetDirection()
    {
        if (behaviour.direction == WeaponBehaviour.DirectionType.FORWARD) { 
            return transform.forward;
        }

        else if (behaviour.direction == WeaponBehaviour.DirectionType.CAMERA)
        {
            return Camera.main.transform.forward;
        }

        throw new System.Exception("Something went wrong here. Have you forgotten to code in the direction option you wished to use?");
    }

}
