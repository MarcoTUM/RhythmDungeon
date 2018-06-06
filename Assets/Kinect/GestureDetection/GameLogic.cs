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

        IRelativeGestureSegment[] moveLeft = { new MoveLeftSegment1(), new MoveLeftSegment2() };
        gc.AddGesture("MoveLeft", moveLeft);
	}


    void moveGestureRecognized(string direction)
    {
        m_playerBehavior.MovePlayer(direction);
        Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
        m_OnlyOneGesturePerBeat = false;
        m_rhythmIdicator.ExecuteEnemyActions();
    }

    void Update()
    {
        //Keyboard Input
        if (!GameModel.Instance.Kinect)
        {
            if (m_rhythmIdicator.status == RhythmIndicator.Status.green && m_OnlyOneGesturePerBeat)
            {
                if (Input.GetKeyDown("down"))
                {
                    moveGestureRecognized("down");
                }
                else if (Input.GetKeyDown("left"))
                {
                    moveGestureRecognized("left");
                }
                else if (Input.GetKeyDown("up"))
                {
                    moveGestureRecognized("up");
                }
                else if (Input.GetKeyDown("right"))
                {
                    moveGestureRecognized("right");
                }
            }
          
        }
        if (m_rhythmIdicator.status == RhythmIndicator.Status.red)
        {
            m_OnlyOneGesturePerBeat = true;
        }
    }

    void OnGestureRecognized(object sender, GestureEventArgs e)
    {
        if (m_rhythmIdicator.status == RhythmIndicator.Status.green && m_OnlyOneGesturePerBeat)
        {
            // Kinect input:
            if (e.GestureName == "MoveRight")
            {
                // Move right
                m_playerBehavior.MovePlayer("right");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
                m_OnlyOneGesturePerBeat = false;
            }
            else if (e.GestureName == "MoveLeft")
            {
                // Move left
                m_playerBehavior.MovePlayer("left");
                // Spawn particle effect
                Instantiate(m_psHitTheBeat, m_rhythmIdicator.transform.position, Quaternion.identity);
                m_OnlyOneGesturePerBeat = false;
            }
        }
        else
        {
            Debug.Log("fail");
        }
    }


}
