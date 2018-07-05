using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGame : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        //6 is for story scene
        GetComponent<Button>().onClick.AddListener(() => GameModel.Instance.LoadLevel(6));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Destroy()
    {
        GetComponent<Button>().onClick.RemoveListener(() => GameModel.Instance.LoadLevel(6));
    }
}

