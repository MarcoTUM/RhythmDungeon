using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private Door door;
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
    }
   
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            door.Open();
            Destroy(this.gameObject);
            Debug.Log("got Key");
        }
    }
}
