﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBubble : MonoBehaviour {

    private RhythmIndicator m_rhythmIndicator;
    public Vector3 m_startPosition;
    public bool Important;
    //public enum BubbleType { left, right};
    //public BubbleType myBubbleType;

    void Start()
    {
        m_startPosition = transform.position;

        // Get rhythm indicator
        m_rhythmIndicator = GameObject.FindGameObjectWithTag("RhythmIndicator").GetComponent<RhythmIndicator>();
        Rigidbody2D myRigid = this.GetComponent<Rigidbody2D>();
        if (myRigid)
        {
            float normX = (m_rhythmIndicator.transform.position.x - m_startPosition.x) / m_rhythmIndicator.m_beatPerSecond; // Mathf.Abs(m_rhythmIndicator.transform.position.x - m_startPosition.x);
            Debug.Log("vel: " + m_rhythmIndicator.m_beatPerSecond);
            myRigid.velocity = new Vector2(normX, 0);
        }

        //StartCoroutine(MoveToPosition(transform, m_rhythmIndicator.transform.position, m_rhythmIndicator.m_beatPerSecond));
    }
	
	// Update is called once per frame
	void Update () 
    {

	}




    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
         Vector3 currentPos = transform.position;
            float t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime / timeToMove;
                //transform.position = Vector3.Lerp(currentPos, position, t);
                transform.position = new Vector3(Mathf.Lerp(currentPos.x, position.x, t), transform.position.y, transform.position.z);
                yield return null;
            }
    }

    public void ResetThisBubble()
    {
        StartCoroutine(ResetBubble());
    }

    public IEnumerator ResetBubble()
    {
        yield return new WaitForSecondsRealtime(m_rhythmIndicator.m_reactionTime);
        transform.position = m_startPosition;
        //StartCoroutine(MoveToPosition(transform, m_rhythmIndicator.transform.position, m_rhythmIndicator.m_beatPerSecond));
    }

    // When bubble hits indicator loop back to start position
    //void OnTriggerEnter2D(Collider2D col)
    //{

    //    if (col.gameObject.tag == "RhythmIndicator")
    //    {
    //        StartCoroutine(ResetBubble());
    //    }
    //}

}
