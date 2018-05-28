using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Door door;

   
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
