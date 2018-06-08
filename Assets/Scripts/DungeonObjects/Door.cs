    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Sprite OpenDoor;
    public void Open()
    {
        this.tag = "DoorOpen";
        GetComponent<SpriteRenderer>().sprite = OpenDoor;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }


}
