using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Enemy enemyPrefab = null;
    [SerializeField] private float enemyLow;
    [SerializeField] private float enemyHigh;
    [Header("Settings")]
    [SerializeField]private float secondsBetweenSpawns = 5f;
    [Header("Fields")]
    [SerializeField] private float stageEnd = -15f;
    private float spawntimer;
    private readonly List<Enemy> enemies = new List<Enemy>();
    // Update is called once per frame
    private void Update()
    {   
        RemoveOldEnemies();

        SpawnNewEnemies();
    }
    public void ResetEnemies()
        {
            foreach(var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
            enemies.Clear();
            spawntimer = 0f;
        }

        private void RemoveOldEnemies()
        {
            for (int i = enemies.Count-1; i > 0; i--)
            {
                if(enemies[i].transform.position.x < stageEnd)
                {
                    Destroy(enemies[i].gameObject);
                    enemies.RemoveAt(i);
                }
            }
        }
        private void SpawnNewEnemies()
        {
            spawntimer -= Time.deltaTime;

            if(spawntimer > 0f){return;}
            if(enemies.Count > 3){return;}
        
                Enemy enemy = Instantiate(enemyPrefab,transform.position,Quaternion.identity);
                float random = UnityEngine.Random.Range(enemyLow,enemyHigh);
                enemy.transform.Translate(Vector3.up * random,Space.World);
                enemies.Add(enemy);
                spawntimer = secondsBetweenSpawns;

        }
}
