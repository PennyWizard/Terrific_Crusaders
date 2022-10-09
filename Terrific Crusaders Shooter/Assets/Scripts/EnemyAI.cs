using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour, IDamage
{
    [Header("--- Component ---")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;

    [Header("---Enemy Stats--")]
    [Range(0, 100)][SerializeField] int HP;
    [Range(0, 20)][SerializeField] int facePlayerspeed;
    [SerializeField] int sightDist;

    [Header("--- Gun Stats---")]
    [Range(1, 25)][SerializeField] int shootDMG;
    [Range(0, 10)][SerializeField] int enemySpeed;
    void Start()
    {
        GameManager.instance.enemyAmount++;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void takeDamage(int damage)
    {
        HP -= damage;
       
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator damageFeedback()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }
}
