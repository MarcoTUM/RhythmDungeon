using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private Dictionary<Direction, FieldType> _nextField;

	// Use this for initialization
	void Start ()
    {
        _nextField = new Dictionary<Direction, FieldType>();
        foreach (Direction d in System.Enum.GetValues(typeof(Direction)))
            _nextField.Add(d, FieldType.Floor);
        GameObject startTile = GameObject.FindGameObjectWithTag("Start");
        if(startTile != null)
            this.transform.position = startTile.transform.position;

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("down") && _nextField[Direction.Down] != FieldType.Wall) {
            transform.Translate(Vector3.down * GameModel.Instance.Step);
        }
        else if (Input.GetKeyDown("left") && _nextField[Direction.Left] != FieldType.Wall)
        {
            transform.Translate(Vector3.left * GameModel.Instance.Step);
        }
        else if (Input.GetKeyDown("up") && _nextField[Direction.Up] != FieldType.Wall)
        {
            transform.Translate(Vector3.up * GameModel.Instance.Step);
        }
        else if (Input.GetKeyDown("right") && _nextField[Direction.Right] != FieldType.Wall)
        {
            transform.Translate(Vector3.right * GameModel.Instance.Step);
        }
    }

    public void setField(Direction dir, FieldType val)
    {
        _nextField[dir] = val;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        GameObject startTile = GameObject.FindGameObjectWithTag("Start");
        if (startTile != null) {
            Gizmos.DrawCube(startTile.transform.position, new Vector3(startTile.transform.localScale.x, startTile.transform.localScale.y, 0));
        }
    }
}
