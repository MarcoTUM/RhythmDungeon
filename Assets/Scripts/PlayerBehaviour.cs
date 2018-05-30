using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {
    
    

    //Variables for animation
    public Animator animator;
    public float speed;
    public float moveOffset;



  
    public Dictionary<Direction, FieldType> _nextField;

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
                
    }


    public void MovePlayer(string direction)
    {
        Debug.Log("N");
        animator.SetBool("Standing", false);
        switch(direction){
            case "up":
                if (_nextField[Direction.Up] != FieldType.Wall)
                {
                    animator.SetTrigger("GoUp");
                    StartCoroutine(MovePlayerTo(Vector3.up));
                }
                break;
            case "down":
                if(_nextField[Direction.Down] != FieldType.Wall)
                {
                    animator.SetTrigger("GoDown");
                    StartCoroutine(MovePlayerTo(Vector3.down));
                }
                break;
            case "right":
                if (_nextField[Direction.Right] != FieldType.Wall)
                {
                    animator.SetTrigger("GoRight");
                    StartCoroutine(MovePlayerTo(Vector3.right));
                }
                break;
            case "left":
                if (_nextField[Direction.Left] != FieldType.Wall)
                {
                    animator.SetTrigger("GoLeft");
                    StartCoroutine(MovePlayerTo(Vector3.left));
                }
                break;
            default:
                Debug.LogError("Something went wrong in switch case statement in PlayerBehaviour");
                break;
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

    IEnumerator MovePlayerTo(Vector3 direction)
    {
        float tmp = 0;
        while(tmp < GameModel.Instance.Step)
        {
            transform.Translate(direction * moveOffset);
            tmp += moveOffset;
            yield return new WaitForSeconds(speed);
            
        }

        animator.SetBool("Standing", true);

    }
}
