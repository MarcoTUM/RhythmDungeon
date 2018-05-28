using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Change());
	}
	
    IEnumerator Change()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("CHange");
        this.GetComponent<SpriteRenderer>().sprite = GameModel.Instance.GroundDark;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
