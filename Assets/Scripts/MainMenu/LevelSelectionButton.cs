using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{

    [SerializeField]
    private Text _text;

    private int _level;

    public void Initialize(int level)
    {
        _level = level;
        _text.text = "Level " + level; // maybe change with names


        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameModel.Instance.LoadLevel(_level);
    }
}
