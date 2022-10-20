using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudySpawner : MonoBehaviour, IDamage
{
    [SerializeField] bool penetrable;
    [SerializeField] GameObject wall;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
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

                Instantiate(enemy, new Vector3(-134.16f, 3, 100.22f), Quaternion.identity);
                Instantiate(enemy2, new Vector3(-134.16f, 3, 102.92f), Quaternion.identity);

                enemiesSpawned += 2;


                Destroy(wall);
            }
        }
    }
}
