using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon {

    public GameObject projectile;

    int i;
    public override void Fire()
    {
        base.Fire();
        print("Bullets used: " + i + "\nThis burst: " + (behaviour.burstAmount - burstsLeft) + "/" + behaviour.burstAmount);
        i++;

        Vector3 direction = GetDirection();

        GameObject instantiatedProjectile = Instantiate(projectile,GetOrigin(), Quaternion.Euler(direction));
        instantiatedProjectile.GetComponent<Rigidbody>().AddForce(direction * 2000);        

    }

}
