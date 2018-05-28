using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorCamera : MonoBehaviour {

    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("right") || Input.GetKey("d"))
        {
            transform.Translate(Vector3.right*Time.deltaTime*GameModel.Instance.Step*Speed);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * GameModel.Instance.Step * Speed);
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * GameModel.Instance.Step * Speed);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * GameModel.Instance.Step * Speed);
        }
    }
}
