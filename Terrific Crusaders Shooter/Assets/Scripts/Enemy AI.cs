using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("---Enemy Stats--")]
    [Range(0,100)][SerializeField] int HP;
    [Range(0,20)][SerializeField] int facePlayerspeed;
    [SerializeField] int sightDist;
    
    [Header("--- Gun Stats---")]
    [Range(1, 25)][SerializeField] int shootDMG;
    [Range(0, 10)][SerializeField] int enemySpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int damage)
    {
        HP -= damage;
        if (HP <=0)
        {

        }
    }
}
