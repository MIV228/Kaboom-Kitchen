using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : MonoBehaviour
{
    public GameController gc;
    public Player player;

    [System.Serializable]
    public class EnemyListEntry
    {
        public int ID;
        public Enemy enemy;
        public int minSpawnWave;
    }

    public EnemyListEntry[] all_enemies;
    public List<Enemy> availableEnemies;

    public int enemiesLeft;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        player = FindObjectOfType<Player>();
    }

    public void StartNight()
    {
        enemiesLeft = 4 + gc.currDay * 3;
        availableEnemies.Clear();
        foreach (EnemyListEntry enemy in all_enemies)
        {
            if (enemy.minSpawnWave <= gc.currDay) availableEnemies.Add(enemy.enemy);
        }
    }

    private void Update()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length < 15 && enemiesLeft > 0)
        {
            enemiesLeft -= 1;
            Vector2 circle = Random.insideUnitCircle.normalized;
            Vector3 enemySpawnPosition = player.transform.position + new Vector3(circle.x, 0, circle.y) * 30;
            enemySpawnPosition.y = 1;
            Enemy e = Instantiate(availableEnemies[Random.Range(0, availableEnemies.Count)].gameObject, enemySpawnPosition, Quaternion.identity).GetComponent<Enemy>();
            e.health = Mathf.RoundToInt(e.health * gc.currDay * gc.currDay * 0.25f);
            e.armor = Mathf.RoundToInt(e.armor * (gc.currDay - 1) * 0.5f);
        }
        if (enemies.Length == 0 && enemiesLeft == 0)
        {
            EndNight();
        }
    }

    public void EndNight()
    {

    }
}
