using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

    public GameObject[] Enemies;
    public GameObject[] Walls;
    [SerializeField]
    private int _eventState;
    public GameObject Key;
    public GameObject[] Floors;
	// Use this for initialization
	void Start () {
        _eventState = 0;

        for (int i = 0; i < Floors.Length; i++)
            Floors[i].SetActive(false);
        for (int i = 1; i < Enemies.Length; i++)
            Enemies[i].SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (_eventState == 0 && Enemies[0] == null) {
            _eventState++;
            for(int i=0; i<Walls.Length; i++)
                Destroy(Walls[i]);
            for (int i = 0; i < Floors.Length; i++)
                Floors[i].SetActive(true);

        }
        else if(_eventState == 1 && Key == null)
        {
            _eventState++;
            for(int i=1; i<Enemies.Length; i++)
                Enemies[i].SetActive(true);
        }
	}
}
