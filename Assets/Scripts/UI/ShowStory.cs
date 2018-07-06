using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowStory : MonoBehaviour {
    
    Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 dirVector;
    public float timeToSlowDown;
    private Vector3 vel = Vector3.zero;
    public AudioSource source;
	// Use this for initialization
	void Start () {
        startPosition = this.gameObject.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += dirVector;
        if (this.transform.position.x < endPosition.x)
        {
            dirVector = new Vector3(0, 0, 0);
            Debug.Log("Start first Level here");
            StartCoroutine(End());
        } else if (this.transform.position.x < (endPosition.x + 5))
        {
            Debug.Log("should slow down");
            transform.position = Vector3.SmoothDamp(transform.position, endPosition, ref vel, timeToSlowDown);
        }
	}

    IEnumerator End()
    {
        for (int i = 0; i < 15; i++)
        {
            source.volume = source.volume - 0.02f;

            yield return new WaitForSeconds(0.1f);
        }
        source.Stop();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

    
}
