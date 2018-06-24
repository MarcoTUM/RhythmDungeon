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
    protected bool _movingBack;
    protected int _counter = 0;

    //variables for animator
    protected Animator enemyAnimator;

    void Start()
    {
        _movingBack = false;
        _startPos = this.transform.position;
        _lastPos = _startPos;
        _player = GameObject.Find("Player");
        _nextField = new Dictionary<Direction, FieldType>();
        foreach (Direction d in System.Enum.GetValues(typeof(Direction)))
            _nextField.Add(d, FieldType.Floor);

        _speed = _player.GetComponent<PlayerBehaviour>().speed;
        _moveOffset = _player.GetComponent<PlayerBehaviour>().moveOffset;

        enemyAnimator = GetComponent<Animator>();
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
        if (_movingBack)
            _movingBack = false;
    }

    public void setField(Direction dir, FieldType val)
    {
        _nextField[dir] = val;
    }

    public void putToLastPos()
    {
        Debug.Log("moveBack" + Prio);
        StopAllCoroutines();

        _movingBack = true;
        Vector3 dist = (_lastPos - transform.position);
        _lastPos = transform.position;
        Debug.Log(dist.normalized);
        StartCoroutine(MoveTo(dist.normalized, dist.magnitude));
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && col.GetComponent<PlayerBehaviour>().getStanding())
        {
            Debug.Log("hit");
            col.GetComponent<PlayerBehaviour>().TakeDamage(1);
        }
        if (col.tag.Equals("MovingEnemy"))
        {
            MovingEnemy other = col.GetComponent<MovingEnemy>();
            if(other.Prio > this.Prio && !_movingBack)
            {
                putToLastPos();
            }
        }
        else if (col.tag.Equals("Wall")) // Still kinda buggy
        {
            putToLastPos();
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }

    protected void selectAnimation(Vector3 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x < 0)
            {
                enemyAnimator.SetTrigger("GoLeft");
            }else
            {
                enemyAnimator.SetTrigger("GoRight");
            }
        }else
        {
            if (direction.y < 0)
            {
                enemyAnimator.SetTrigger("GoDown");
            }
            else
            {
                enemyAnimator.SetTrigger("GoUp");
            }
        }
    }

}
