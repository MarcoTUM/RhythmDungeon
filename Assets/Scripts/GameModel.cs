using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction { Up, Down, Right, Left };
public enum FieldType { Wall, Floor};
public class GameModel : MonoBehaviour {

    public bool Kinect;
    public static GameModel Instance;
    public float Step;

    public GameObject Tile;
    public Sprite GroundLight, GroundDark;
    public int MaxLevel, UnlockedLevel;

    public bool GameActive;

	// Use this for initialization
	void Awake () {
		if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Step = Tile.GetComponent<Renderer>().bounds.size.x;

    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }
        // Update is called once per frame
        void Update () {
		
	}
}
