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

    public float ReactionTime, WaitTime;
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
        SoundManager.Instance.BGM.Stop();
        SceneManager.LoadScene("Scenes/Levels/Level " + level);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);
        if (currentLevel < MaxLevel)
            LoadLevel(currentLevel + 1);
        else
            LoadMainMenu();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Split(' ')[1]);
        LoadLevel(currentLevel);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

   
}
