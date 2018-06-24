using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyA : MovingEnemy {

    protected int wait = 2;
    public override void action()
    {
        _counter++;
        if (_counter == wait)
        {
            StartCoroutine(MoveTo(chooseDirection(0, 0), GameModel.Instance.Step));
            _counter = 0;
        }
        
    }


    public Vector3 chooseDirection(float minusX, float minusY)
    {
        Vector3 diff = _player.transform.position - this.transform.position;
        float x = diff.x - minusX;
        float y = diff.y - minusY;
        FieldType nextField;
        if(Math.Abs(x) > Math.Abs(y))
        {
            nextField = x < 0 ? _nextField[Direction.Left] : _nextField[Direction.Right];
            if (nextField == FieldType.Floor)
                return new Vector3(x < 0 ? -1 : 1, 0, 0);
            else
                return chooseDirection(x, minusY);
        }
        else if(Math.Abs(y) > Math.Abs(x))
        {
            nextField = y < 0 ? _nextField[Direction.Down] : _nextField[Direction.Up];
            if (nextField == FieldType.Floor)
                return new Vector3(0, y < 0 ? -1 : 1, 0);
            else
                return chooseDirection(minusX, y);
        }
        return Vector3.zero;
    }
}
