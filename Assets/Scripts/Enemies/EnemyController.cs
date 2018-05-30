using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject Level;
    Enemy[] enemies;
	// Use this for initialization
	void Start () {
        enemies = Level.GetComponentsInChildren<Enemy>();
	}
	
    public void ExecuteActions()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.action();
        }
    }
}
