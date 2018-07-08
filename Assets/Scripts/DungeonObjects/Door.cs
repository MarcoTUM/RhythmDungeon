    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Sprite OpenDoor;

    //sorry fuer das unschoene hardcoden...
    public Animator freeableCharacter;

    public void Open()
    {
        this.tag = "DoorOpen";
        GetComponent<SpriteRenderer>().sprite = OpenDoor;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if (freeableCharacter)
            {
                freeableCharacter.SetTrigger("Freed");
            }

            Destroy(this.gameObject);
        }
    }


}
