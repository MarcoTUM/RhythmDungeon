using UnityEngine;
using UnityEngine.UI;

public class LoadMainMenuButton : MonoBehaviour {
    void Start()
    {
        GetComponent<Button>().onClick.AddListener( () => {
            GameModel.Instance.LoadMainMenu();
        });
    }
}
