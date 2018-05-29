using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBubble : MonoBehaviour {

    private RhythmIndicator m_rhythmIndicator;
    private Vector3 m_startPosition;

    void Start()
    {
        m_startPosition = transform.position;

        // Get rhythm indicator
        m_rhythmIndicator = GameObject.FindGameObjectWithTag("RhythmIndicator").GetComponent<RhythmIndicator>();

        StartCoroutine(MoveToPosition(transform, m_rhythmIndicator.transform.position, m_rhythmIndicator.m_beatPerSecond));
    }
	
	// Update is called once per frame
	void Update () 
    {
        // When bubble hits indicator loop back to start position
        if (transform.position.x == m_rhythmIndicator.transform.position.x)
        {
            StartCoroutine(ResetBubble());
        }
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

    public IEnumerator ResetBubble()
    {
        yield return new WaitForSecondsRealtime(m_rhythmIndicator.m_reactionTime);

        transform.SetPositionAndRotation(m_startPosition, Quaternion.identity);

        StartCoroutine(MoveToPosition(gameObject.transform, m_rhythmIndicator.transform.position, m_rhythmIndicator.m_beatPerSecond));
    }

}
