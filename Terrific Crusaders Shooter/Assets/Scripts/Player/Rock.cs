using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject rock;

    [Header("Setteings")]
    public float throwCooldown;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot") && readyToThrow)
        {
            Throw ();
        }
    }

    void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(rock, attackPoint.position, cam.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        rb.AddForce(forceToAdd, ForceMode.Impulse);

        Invoke(nameof(ResetThrow), throwCooldown);

        
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }
}
