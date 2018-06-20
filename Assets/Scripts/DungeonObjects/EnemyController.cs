using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameObject Level;
    Enemy[] enemies;
	// Use this for initialization
	void Start () {
        Level = GameObject.FindGameObjectWithTag("Level");
        enemies = Level.GetComponentsInChildren<Enemy>();
        Debug.Log(enemies.Length + " Enemies found");
	}
	
    public void ExecuteActions()
    {
        foreach(Enemy enemy in enemies)
        {
            //MovingEnemy movingEnemy = enemy as MovingEnemy;
            //if (movingEnemy != null)
                enemy.action();
        }
    }
}
