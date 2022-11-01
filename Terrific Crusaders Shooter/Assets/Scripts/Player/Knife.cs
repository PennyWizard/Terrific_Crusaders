using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public int damage;
    public float range;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot") && GameManager.instance.isPaused)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, range))
        {
            if (hit.collider.GetComponent<IDamage>() != null)
            {
                hit.collider.GetComponent<IDamage>().takeDamage(damage);
            }


        }
    }
}
