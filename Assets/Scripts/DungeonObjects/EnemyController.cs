using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameObject Level;
    Enemy[] enemies;
	// Use this for initialization
	void Awake () {
        Level = GameObject.FindGameObjectWithTag("Level");
        enemies = Level.GetComponentsInChildren<Enemy>();
        Debug.Log(enemies.Length + " Enemies found");
	}
    public void ResetEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            enemy.doReset();
        }
    }
    public void ExecuteActions()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy != null && enemy.gameObject.activeSelf)
                enemy.action();
        }
    }
}
