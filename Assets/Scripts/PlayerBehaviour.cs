using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour {
    
    

    //Variables for animation
    public Animator animator;
    public float speed;
    public float moveOffset;
    private bool _standing;
    private Vector3 _startPos;

  
    public Dictionary<Direction, FieldType> _nextField;

	// Use this for initialization
	void Start ()
    {
        _nextField = new Dictionary<Direction, FieldType>();
        foreach (Direction d in System.Enum.GetValues(typeof(Direction)))
            _nextField.Add(d, FieldType.Floor);
        GameObject startTile = GameObject.FindGameObjectWithTag("Start");
        if (startTile != null)
        {
            _startPos = startTile.transform.position;
            this.transform.position = startTile.transform.position;
        }
            

    }

    // Update is called once per frame
    void Update () {
                
    }

    public void Die()
    {
        transform.position = _startPos;
    }
    public bool getStanding()
    {
        return _standing;
    }

    public void MovePlayer(string direction)
    {
        animator.SetBool("Standing", false);
        _standing = false;
        switch (direction){
            case "up":

                animator.SetTrigger("GoUp");
                if (_nextField[Direction.Up] != FieldType.Wall)
                {
                    StartCoroutine(MovePlayerTo(Vector3.up));
                }
                else
                {
                    StartCoroutine(MovePlayerTo(Vector3.zero));
                }
                break;
            case "down":
                animator.SetTrigger("GoDown");
                if (_nextField[Direction.Down] != FieldType.Wall)
                {
                    StartCoroutine(MovePlayerTo(Vector3.down));
                }
                else
                {
                    StartCoroutine(MovePlayerTo(Vector3.zero));
                }
                break;
            case "right":
                animator.SetTrigger("GoRight");
                if (_nextField[Direction.Right] != FieldType.Wall)
                {
                    StartCoroutine(MovePlayerTo(Vector3.right));
                }
                else
                {
                    StartCoroutine(MovePlayerTo(Vector3.zero));
                }
                break;
            case "left":
                animator.SetTrigger("GoLeft");
                if (_nextField[Direction.Left] != FieldType.Wall)
                {
                    StartCoroutine(MovePlayerTo(Vector3.left));
                }
                else
                {
                    StartCoroutine(MovePlayerTo(Vector3.zero));
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

    ///<summary>
    ///animationTime = speed / Offset 
    /// </summary>

    IEnumerator MovePlayerTo(Vector3 direction)
    {
        float tmp = 0;
        while(tmp < GameModel.Instance.Step)
        {
            transform.Translate(direction * moveOffset);
            tmp += moveOffset;
            yield return new WaitForSeconds(speed);
            
        }
        _standing = true;
        animator.SetBool("Standing", true);

    }
}
