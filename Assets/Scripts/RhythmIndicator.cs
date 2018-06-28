using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmIndicator : MonoBehaviour
{
    private Time m_time;
    private IEnumerator m_coroutine;
    private bool m_continueCourutine;

    public enum Status { green, red }
    public Status status;
    public float m_beatPerSecond;
    //public float myBeatPerSecond;
    public float m_reactionTime;
    private EnemyController enemyCont;
    private bool _enemyDidAction;
    private bool _waiting;
    private float _beat;

    public RhythmBubble[] myBubbles;

    // Use this for initialization
    void Start()
    {
        _beat = 0;
        _waiting = false;
        m_beatPerSecond = GameModel.Instance.WaitTime;
        m_reactionTime = GameModel.Instance.ReactionTime;
        
        enemyCont = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        status = Status.red;
        //m_continueCourutine = true;
        //m_coroutine = WaitOnBeat();
        //StartCoroutine(m_coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        _beat += Time.deltaTime;
    }

    /*
    IEnumerator WaitOnBeat()
    {
        if (m_beatPerSecond < m_reactionTime)
        {
            Debug.Log("Error: reactionTime is bigger than beat!");
        }
        else
        {
            while (m_continueCourutine)
            {
                status = Status.red;
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                yield return new WaitForSecondsRealtime(m_beatPerSecond);
                _enemyDidAction = false;
                   
                status = Status.green;
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                yield return new WaitForSecondsRealtime(m_reactionTime);
                ExecuteEnemyActions();
            }
        }
    }
    */

    IEnumerator WaitOnBeat()
    {
        _waiting = true;
        status = Status.green;
        _enemyDidAction = false;
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        yield return new WaitForSecondsRealtime(m_reactionTime);
        status = Status.red;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        ExecuteEnemyActions();
        _waiting = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Bubble" && !_waiting)
        {
            //Debug.Log("Beat: " + _beat);
            _beat = 0;
            StartCoroutine(WaitOnBeat());
        }

        if(col.gameObject.tag == "Bubble" && col.transform.GetComponent<RhythmBubble>().Important)
        {
            for(int i = 0; i < myBubbles.Length; i++)
            {
                myBubbles[i].ResetThisBubble();
            }
        }
    }

    public void ExecuteEnemyActions()
    {
        if (!_enemyDidAction)
        {
            _enemyDidAction = true;
            enemyCont.ExecuteActions();
        }
    }
}
