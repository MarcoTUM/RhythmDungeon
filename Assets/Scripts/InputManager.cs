using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public ParticleSystem m_psHitTheBeat;
    private RhythmIndicator m_rhythmIdicator;
    private PlayerBehaviour m_playerBehavior;

    // Use this for initialization
    void Start()
    {
        m_playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        m_rhythmIdicator = GameObject.FindGameObjectWithTag("Indicator").GetComponent<RhythmIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rhythmIdicator.status == RhythmIndicator.Status.green)
        {
            if (Input.GetKeyDown("down") && m_playerBehavior._nextField[Direction.Down] != FieldType.Wall)
            {
                m_playerBehavior.MovePlayer("down");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown("left") && m_playerBehavior._nextField[Direction.Left] != FieldType.Wall)
            {
                m_playerBehavior.MovePlayer("left");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown("up") && m_playerBehavior._nextField[Direction.Up] != FieldType.Wall)
            {
                m_playerBehavior.MovePlayer("up");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else if (Input.GetKeyDown("right") && m_playerBehavior._nextField[Direction.Right] != FieldType.Wall)
            {
                m_playerBehavior.MovePlayer("right");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
        }
    }
}
