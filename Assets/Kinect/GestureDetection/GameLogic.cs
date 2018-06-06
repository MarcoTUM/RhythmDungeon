using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public GestureController gc;

    public ParticleSystem m_psHitTheBeat;
    private RhythmIndicator m_rhythmIdicator;
    private PlayerBehaviour m_playerBehavior;

    private bool m_OnlyOneGesturePerBeat;

	// Use this for initialization
	void Start () {
        m_OnlyOneGesturePerBeat = true;
        m_playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        m_rhythmIdicator = GameObject.FindGameObjectWithTag("RhythmIndicator").GetComponent<RhythmIndicator>();

        gc.GestureRecognizedInController += OnGestureRecognized;
        IRelativeGestureSegment[] swipeLeft = { new SwipeToLeftSegment1(), new SwipeToLeftSegment2(), new SwipeToLeftSegment3() };
        gc.AddGesture("SwipeLeft", swipeLeft);
        IRelativeGestureSegment[] waveLeft = { new WaveLeftSegment1(), new WaveLeftSegment2(),
                                             new WaveLeftSegment1(),new WaveLeftSegment2(),
                                             new WaveLeftSegment1(),new WaveLeftSegment2() };
        gc.AddGesture("WaveLeft", waveLeft);

        IRelativeGestureSegment[] pullLeft = { new PullToLeftSegment1(), new PullToLeftSegment2(), new PullToLeftSegment3(), };
        gc.AddGesture("PullLeft", pullLeft);

        IRelativeGestureSegment[] fireBall = { new FireBallSegment1(), new FireBallSegment2(), };
        gc.AddGesture("FireBall", fireBall);

        IRelativeGestureSegment[] moveRight = { new MoveRightSegment1(), new MoveRightSegment2() };
        gc.AddGesture("MoveRight", moveRight);
	}

    void OnGestureRecognized(object sender, GestureEventArgs e)
    {
        if (m_rhythmIdicator.status == RhythmIndicator.Status.green && m_OnlyOneGesturePerBeat)
        {
            /*if (Input.GetKeyDown("down") && m_playerBehavior._nextField[Direction.Down] != FieldType.Wall)
            {
                m_playerBehavior.MovePlayer("down");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
            }
            else */
            if (e.GestureName == "MoveRight")
            {
                m_playerBehavior.MovePlayer("right");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
                m_OnlyOneGesturePerBeat = false;
                Debug.Log("MoveRight Recognized");
            }
            /*else if (Input.GetKeyDown("up") && m_playerBehavior._nextField[Direction.Up] != FieldType.Wall)
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
            }*/
        }

        if(m_rhythmIdicator.status == RhythmIndicator.Status.red)
        {
            m_OnlyOneGesturePerBeat = true;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
