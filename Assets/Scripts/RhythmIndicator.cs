﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmIndicator : MonoBehaviour
{
    private Time m_time;
    private IEnumerator m_coroutine;
    private bool m_continueCourutine;

    public float m_beatPerSecond;
    public float m_reactionTime;

    // Use this for initialization
    void Start()
    {
        m_continueCourutine = true;
        m_coroutine = WaitOnBeat();
        StartCoroutine(m_coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitOnBeat()
    {
        if(m_beatPerSecond <= m_reactionTime)
        {
            Debug.Log("Error: reactionTime is bigger than beat!");
        }
        else
        {
            while (m_continueCourutine)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                yield return new WaitForSecondsRealtime(m_beatPerSecond);

                GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                yield return new WaitForSecondsRealtime(m_reactionTime);
            }
        }
    }
}
