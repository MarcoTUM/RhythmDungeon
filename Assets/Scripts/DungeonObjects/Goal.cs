using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
            Debug.Log("gg");
    }
}
