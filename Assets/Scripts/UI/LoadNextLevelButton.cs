using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNextLevelButton : MonoBehaviour {
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameModel.Instance.LoadNextLevel();
        });
    }
}
