using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyB : MovingEnemy {

    protected int wait = 1;
    public bool Special;
    public override void action()
    {
        _counter++;
        if (_counter == wait)
        {
            StartCoroutine(MoveTo(chooseDirection(), GameModel.Instance.Step));
            _counter = 0;
        }
        
    }


    public Vector3 chooseDirection()
    {
        Vector3 diff = _player.transform.position - this.transform.position;
        int x = Math.Sign(diff.x);
        int y = Math.Sign(diff.y);
        while (x == 0)
            x = Math.Sign(UnityEngine.Random.Range(-1.0f, 1.0f));

        while (y == 0)
            y = Math.Sign(UnityEngine.Random.Range(-1.0f, 1.0f));

        FieldType nextField;
        if (x == 1 && y == 1)
            nextField = _nextField[Direction.Right];
        else if (x == 1 && y == -1)
            nextField = _nextField[Direction.Down];
        else if (x == -1 && y == -1)
            nextField = _nextField[Direction.Left];
        else if (x == -1 && y == 1)
            nextField = _nextField[Direction.Up];
        else
            nextField = FieldType.Wall;

        if (nextField == FieldType.Floor)
            return new Vector3(x, y, 0);
        int[] xArr = { 1, 1, -1, -1 };
        int[] yArr = { 1, -1, -1, 1 };
        int timeout = 50;
        Debug.Log("normalDirectoin Wall" + x + " - " + y);
        while(nextField == FieldType.Wall && timeout>0)
        {
            int t = UnityEngine.Random.Range(0, 4);
            x = xArr[t]; y = yArr[t];
            if (x == 1 && y == 1)
                nextField = _nextField[Direction.Right];
            else if (x == 1 && y == -1)
                nextField = _nextField[Direction.Down];
            else if (x == -1 && y == -1)
                nextField = _nextField[Direction.Left];
            else if (x == -1 && y == 1)
                nextField = _nextField[Direction.Up];
            else
                nextField = FieldType.Wall;
        }
        if (nextField == FieldType.Floor)
            return new Vector3(x, y, 0);

        return Vector3.zero;
    }
}
