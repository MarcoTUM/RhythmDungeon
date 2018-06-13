using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RestartLevelButton : MonoBehaviour {
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameModel.Instance.RestartLevel();
        });
    }
}
