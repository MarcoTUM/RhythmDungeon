using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public ParticleSystem m_psHitTheBeat;
    public RhythmIndicator m_rhythmIdicator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump") && m_rhythmIdicator.status == RhythmIndicator.Status.green)
        {
            Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
        }
	}
}
