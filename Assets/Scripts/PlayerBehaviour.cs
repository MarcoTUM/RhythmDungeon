using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour {
    
    

    //Variables for animation
    public Animator animator;
    public float speed;
    public float moveOffset;
    private bool _standing;
    private Vector3 _startPos;

    public int CheatRight, CheatUp;
    [SerializeField]
    private GameObject _menuNext, _menuRestart, _menu, _pointer;
    
    public Dictionary<Direction, FieldType> _nextField;
    private int _life;
    private MovingEnemy _nextEnemy;
    private EnemyController _enemyCont;
    private Transform _livePanel;
    private GameObject[] _hearts = new GameObject[3];
	// Use this for initialization
	void Start ()
    {
        _livePanel = GameObject.FindGameObjectWithTag("Hearts").transform;

        for (int i = 0; i < 3; i++)
        {
            _hearts[i] = _livePanel.GetChild(i).gameObject;
        }
            _enemyCont = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        _standing = true;
        _life = 3;
        _pointer.SetActive(false);
        _menu.SetActive(false);
        _nextField = new Dictionary<Direction, FieldType>();
        foreach (Direction d in System.Enum.GetValues(typeof(Direction)))
            _nextField.Add(d, FieldType.Floor);

      
        GameObject startTile = GameObject.Find("StartTile");
        if (startTile != null)
        {
            _startPos = startTile.transform.position;
            this.transform.position = startTile.transform.position;
        }
        else
        {
            Debug.Log("No StartTiel Found");
        }

        SoundManager.Instance.PlayBGM(2);
    }

    // Debug Stuff - delete later
    void Update () {
        if (Input.GetKeyDown("p"))
            Cheat();
    }

    public void Attack()
    {
        if (_nextEnemy != null)
        {


            //add Attack Animation
            float tmpX = _nextEnemy.transform.position.x - this.transform.position.x;
            float tmpY = _nextEnemy.transform.position.y - this.transform.position.y;
            Debug.Log("TmpY: " + tmpY);
            Debug.Log("TmpX: " + tmpX);
            if (Mathf.Abs(tmpX) < Mathf.Abs(tmpY))
            {

                if (tmpY < 0)
                {
                    animator.SetTrigger("AttackDown");
                }
                else
                {
                    animator.SetTrigger("AttackUp");
                }
            }
            else
            {
                if (tmpX < 0)
                {
                    animator.SetTrigger("AttackLeft");
                }
                else
                {
                    animator.SetTrigger("AttackRight");
                }
            }

            _nextEnemy.Die();
            _nextEnemy = null;
        }
        else
        {
            animator.SetTrigger("Attack");
        }

    }
    IEnumerator WinWithWait()
    {
        yield return new WaitForSeconds(speed / moveOffset);
        SoundManager.Instance.StopBGM();
        Time.timeScale = 0;
        _pointer.SetActive(true);
        _menu.SetActive(true);
        _menuNext.SetActive(true);
        _menuRestart.SetActive(false);
    }

    public void Win()
    {
        StartCoroutine(WinWithWait());
    }

    public void TakeDamage(int damage)
    {
        _life -= damage;
        if(_life > -1)
            _hearts[_life].SetActive(false);
        Debug.Log("Remaining Lifes: " + _life);// switch with GUI
        if (_life > 0)
        {
            transform.position = _startPos;
            _enemyCont.ResetEnemies();
        }
        else
        {
            Die();
        }

    }

    public void Die()
    {
        SoundManager.Instance.StopBGM();
        Time.timeScale = 0;
        _pointer.SetActive(true);
        _menu.SetActive(true);
        _menuNext.SetActive(false);
        _menuRestart.SetActive(true);
    }
    public void setEnemy(MovingEnemy enemy)
    {
        _nextEnemy = enemy;
    }

    public GameObject getEnemy()
    {
        return _nextEnemy == null ? null : _nextEnemy.gameObject;
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
    /// with current setup animTime = 0.2 sec;
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

    public void setCheckpoint(Vector3 pos)
    {
        _startPos = pos;
    }

    private void Cheat()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
