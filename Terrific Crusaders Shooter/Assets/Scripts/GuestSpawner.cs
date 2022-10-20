using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour, IDamage
{
    [SerializeField] bool penetrable;
    [SerializeField] GameObject wall;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy1;
    [SerializeField] int maxEnemies;

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

                Instantiate(enemy, new Vector3(-140.84f, 3, 114.74f), Quaternion.identity);
                Instantiate(enemy1, new Vector3(-132.49f, 2, 114.74f), Quaternion.identity);

                enemiesSpawned += 2;


                Destroy(wall);
            }
        }
    }
}
