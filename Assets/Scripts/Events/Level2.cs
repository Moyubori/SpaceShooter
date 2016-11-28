using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level2 : LevelEvent {
    private static int ENEMY_COUNT = 3;

    public ObjectPool enemyPool;

    override protected IEnumerator EventActions() {
        List<Transform> enemies = new List<Transform>(ENEMY_COUNT);
        for(int i=0; i<ENEMY_COUNT; i++) {
            enemies.Add(spawn(i));
        }

        while (enemies.Count > 0) {
            for(int i=0; i<enemies.Count; i++) {
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

    private Transform spawn(int index) {
        Transform enemy = enemyPool.GetInstance();
        enemy.GetComponent<MovementController>().QueuePath("Level_2_enter"+(index + 1), 1f, "none");
        enemy.GetComponent<MovementController>().QueuePath("Level_2_move"+ (index + 1), 4f, "reverse", 2);
        return enemy;
    }
}