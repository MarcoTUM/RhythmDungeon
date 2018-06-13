using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private Door door;
    void Start()
    {
        GameObject doorObj = GameObject.FindGameObjectWithTag("Door");
        if(doorObj != null)
            door = doorObj.GetComponent<Door>();
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
