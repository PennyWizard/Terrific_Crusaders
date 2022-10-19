using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDoorSpawner : MonoBehaviour, IDamage
{
    [SerializeField] bool penetrable;
    [SerializeField] GameObject wall;

    [SerializeField] GameObject enemy;
    [SerializeField] int maxEnemies = 2;

    int HP = 1;
    int enemiesSpawned;
    

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.enemyAmount += maxEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int DMG)
    {
        if (penetrable)
        {
            HP -= DMG;
            if (HP <= 0)
            {
                
                    Instantiate(enemy, new Vector3(4.2f, 2, 7.11f), Quaternion.identity);
                    Instantiate(enemy, new Vector3(5.2f, 2, 8.11f), Quaternion.identity);
                    enemiesSpawned++;
                

                Destroy(wall);
            }
        }
    }
}
