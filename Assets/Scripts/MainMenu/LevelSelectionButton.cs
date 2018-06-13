using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{


    private int _level;

    public void Initialize(int level)
    {

        _level = level;
        Image[] children = GetComponentsInChildren<Image>();
        children[0].sprite = (Resources.Load<Sprite>("Sprites/UI/ScrollWhiteLevel" + level) as Sprite);

        children[1].sprite = (Resources.Load<Sprite>("Sprites/UI/ScrollGreenLevel" + level) as Sprite);
        
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameModel.Instance.LoadLevel(_level);
    }
}
