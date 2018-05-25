using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private Dictionary<Direction, FieldType> _nextField;
    public GameObject StartTile;
	// Use this for initialization
	void Start ()
    {

        _nextField = new Dictionary<Direction, FieldType>();
        this.transform.position = StartTile.transform.position;

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
}
