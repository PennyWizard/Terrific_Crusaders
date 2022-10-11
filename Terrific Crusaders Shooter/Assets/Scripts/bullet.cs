using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]Rigidbody rb;

    [Header("---Bullet Stats---")]
    [SerializeField] int speed;
    [SerializeField] int damage;
    [SerializeField] int destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.takeDamage(damage);
        }
        Destroy(gameObject);
    }
}
