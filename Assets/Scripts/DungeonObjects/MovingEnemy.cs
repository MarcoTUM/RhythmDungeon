using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEnemy : Enemy {
    
    protected GameObject _player;
    public Dictionary<Direction, FieldType> _nextField;
    protected Vector3 _startPos;
    protected float _speed;
    protected float _moveOffset;
    protected Vector3 _lastPos;
    public int Prio;

    protected int _counter = 0;
    void Start()
    {
        _startPos = this.transform.position;
        _lastPos = _startPos;
        _player = GameObject.Find("Player");
        _nextField = new Dictionary<Direction, FieldType>();
        foreach (Direction d in System.Enum.GetValues(typeof(Direction)))
            _nextField.Add(d, FieldType.Floor);

        _speed = _player.GetComponent<PlayerBehaviour>().speed;
        _moveOffset = _player.GetComponent<PlayerBehaviour>().moveOffset;
    }

    public override void doReset()
    {
        transform.position = _startPos;
        _lastPos = _startPos;
        _counter = 0;
        StopAllCoroutines();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    protected IEnumerator MoveTo(Vector3 direction, float Distance)
    {
        float tmp = 0;
        while (tmp < Distance)
        {
            transform.Translate(direction * _moveOffset);
            tmp += _moveOffset;
            yield return new WaitForSeconds(_speed);
        }
        transform.position = _lastPos + direction * Distance;
        _lastPos = transform.position;
    }

    public void setField(Direction dir, FieldType val)
    {
        _nextField[dir] = val;
    }

    public void putToLastPos()
    {
        StopAllCoroutines();

        Vector3 dist = (_lastPos - transform.position);
        _lastPos = transform.position;
        Debug.Log(dist.normalized);
        StartCoroutine(MoveTo(dist.normalized, dist.magnitude));
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && col.GetComponent<PlayerBehaviour>().getStanding())
            col.GetComponent<PlayerBehaviour>().TakeDamage(1);
        if (col.tag.Equals("MovingEnemy"))
        {
            MovingEnemy other = col.GetComponent<MovingEnemy>();
            if(other.Prio < this.Prio)
            {
                other.putToLastPos();
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }

}
