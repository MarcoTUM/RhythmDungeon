using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

    public Direction Dir;
    private MovingEnemy _enemy;
    private int _colCnt; //as countermeasure for doublecollision when 2objects are on the same spot
                         // Use this for initialization
    void Start()
    {
        _colCnt = 0;
        _enemy = transform.parent.GetComponent<MovingEnemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _colCnt = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(this.transform.localScale.x, this.transform.localScale.y, 0));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<MovingEnemy>() != null)
        {
            _enemy.setField(Dir, FieldType.Wall);
        }
        if (col.tag.Equals("Wall") && _colCnt < 1)
            _enemy.setField(Dir, FieldType.Wall);
        if (col.tag.Equals("Door"))
        {
            _enemy.setField(Dir, FieldType.Wall);
        }
        else if (col.tag.Equals("Floor") && _colCnt < 1)
            _enemy.setField(Dir, FieldType.Floor);
        else
            return;


        _colCnt++;
    }
}
