using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level3 : LevelEvent {

    public ObjectPool enemyPool;
    public int enemiesToSpawn = 5;
    public float spawnInterval = 1.0f;

    override protected IEnumerator EventActions() {
        List<Transform> enemies = new List<Transform>(enemiesToSpawn);
        for (int i = 0; i < enemiesToSpawn; i++) {
            enemies.Add(spawn());
            yield return new WaitForSeconds(spawnInterval);
        }

        while (enemies.Count > 0) {
            for (int i = 0; i < enemies.Count; i++) {
                Transform enemy = enemies[i];
                if (!enemy.gameObject.activeSelf) {
                    enemies.Remove(enemy);
                    i--;
                }
            }
            yield return new WaitForSeconds(1f);
        }
        FinishEvent();
    }

    private Transform spawn() {
        Transform enemy = enemyPool.GetInstance();
        enemy.GetComponent<MovementController>().QueuePath("Level_3_enter", 4f, "none");
        enemy.GetComponent<MovementController>().QueuePath("Level_3_loop", 4f, "plain");
        return enemy;
    }
}