using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEnemy : Enemy {
    
    protected GameObject _player;
    public Dictionary<Direction, FieldType> _nextField;
    protected Vector3 _startPos;
    protected float _speed;
    protected float _moveOffset;
    void Start()
    {
        _startPos = this.transform.position;
        _player = GameObject.Find("Player");
        _nextField = new Dictionary<Direction, FieldType>();
        foreach (Direction d in System.Enum.GetValues(typeof(Direction)))
            _nextField.Add(d, FieldType.Floor);

        _speed = _player.GetComponent<PlayerBehaviour>().speed;
        _moveOffset = _player.GetComponent<PlayerBehaviour>().moveOffset;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    protected IEnumerator MoveTo(Vector3 direction)
    {
        float tmp = 0;
        while (tmp < GameModel.Instance.Step)
        {
            transform.Translate(direction * _moveOffset);
            tmp += _moveOffset;
            yield return new WaitForSeconds(_speed);
        }
    }

    public void setField(Direction dir, FieldType val)
    {
        _nextField[dir] = val;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && col.GetComponent<PlayerBehaviour>().getStanding())
            col.GetComponent<PlayerBehaviour>().TakeDamage(1);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }

}
