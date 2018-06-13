using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    private bool _checked;
	// Use this for initialization
	void Start () {
        _checked = false;
	}
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag.Equals("Player") && !_checked)
        {
            col.GetComponent<PlayerBehaviour>().setCheckpoint(this.transform.position);
            _checked = true;
        }
            
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            Gizmos.DrawCube(this.transform.position, new Vector3(col.bounds.extents.x*2, col.bounds.extents.y*2, 0));
        }
    }

}
