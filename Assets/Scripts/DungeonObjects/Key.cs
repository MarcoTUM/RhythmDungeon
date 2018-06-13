using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    [SerializeField]
    private Door door;
    void Start()
    {
        GameObject doorObj = GameObject.Find("door");
        if(doorObj != null){
            
            door = doorObj.GetComponent<Door>();
        }
        else
        {
            Debug.Log("door not found");
        }
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
