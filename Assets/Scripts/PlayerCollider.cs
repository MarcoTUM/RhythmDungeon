using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCollider : MonoBehaviour {
    public Direction Dir;
    private PlayerBehaviour _player;
	// Use this for initialization
	void Start () {
        _player = transform.parent.GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y, 0));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Wall"))
            _player.setField(Dir, FieldType.Wall);
        else if (col.tag.Equals("Floor"))
            _player.setField(Dir, FieldType.Floor);

    }
}
