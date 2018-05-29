using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public ParticleSystem m_psHitTheBeat;
    public RhythmIndicator m_rhythmIdicator;

    public PlayerBehaviour m_playerBehavior;

	// Use this for initialization
	void Start () {
        m_playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_rhythmIdicator.status == RhythmIndicator.Status.green)
        {
            if (Input.GetKeyDown("down") && m_playerBehavior._nextField[Direction.Down] != FieldType.Wall)
            {
                m_playerBehavior.transform.Translate(Vector3.down * GameModel.Instance.Step);
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown("left") && m_playerBehavior._nextField[Direction.Left] != FieldType.Wall)
            {
                m_playerBehavior.transform.Translate(Vector3.left * GameModel.Instance.Step);
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown("up") && m_playerBehavior._nextField[Direction.Up] != FieldType.Wall)
            {
                m_playerBehavior.transform.Translate(Vector3.up * GameModel.Instance.Step);
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown("right") && m_playerBehavior._nextField[Direction.Right] != FieldType.Wall)
            {
                m_playerBehavior.transform.Translate(Vector3.right * GameModel.Instance.Step);
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
        }
	}
}
