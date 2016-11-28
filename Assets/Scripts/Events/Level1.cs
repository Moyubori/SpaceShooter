using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1 : LevelEvent {

    public ObjectPool enemyPool;

    override protected IEnumerator EventActions() {
        Transform enemy1 = enemyPool.GetInstance();
        enemy1.GetComponent<MovementController>().QueuePath("Level_1_enter1", 4f);

        Transform enemy2 = enemyPool.GetInstance();
        enemy2.GetComponent<MovementController>().QueuePath("Level_1_enter2", 4f);

        while (enemy1.gameObject.activeSelf || enemy2.gameObject.activeSelf) {
            yield return new WaitForSeconds(1f);
        }
        FinishEvent();
    }
}