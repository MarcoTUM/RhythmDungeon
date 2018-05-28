using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditor : MonoBehaviour {

    public GameObject TileSelect, StuffSelect;
    public GameObject Empty;
    public GameObject TileA, TileB, Wall, Goal;
    public GameObject Key, Door;
    public int Width, Height;
    private float _step;
    int _tileNmb = 0, _stuffNmb = 0, _mode = 0; 
    string _name;
    GameObject _level;
    GameObject _floor, _start;
    GameObject _stuff; // Layer above Floor for Items/Enemies
    GameObject _con;
    // Use this for initialization
    void Start () {
        _step = GameModel.Instance.Step;
        _name = "cur";
        LoadPrefab();
        _name = null;

        setMode(0);
    }

    // Update is called once per frame
    void Update () {
        _start = GameObject.FindGameObjectWithTag("Start");
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero);
            
            if(hit.collider != null)
            {
             
                switch (_mode)
                {
                    case 0:
                        Debug.Log(hit.transform.tag);
                        HandleTiles(hit);
                        break;
                    case 1:

                        if (Input.GetMouseButtonDown(0))
                            HandleStuff(hit);
                        break;
                }
                
            }
        }
	}

    ///<param name="nmb">0=Floor, 1=Wall, 2=Start, 3=Goal, 4=Empty</param>
    public void setTile(int nmb)
    {
        _tileNmb = nmb;
    }

    ///<param name="nmb">0=Key</param>
    public void setStuff(int nmb)
    {
        _stuffNmb = nmb;
    }

    ///<param name="nmb">0=Tile, 1=Stuff</param>
    public void setMode(int nmb)
    {
        _mode = nmb;
        bool tile, stuff;
        switch (nmb) {
            case 0:
            default:
                tile = true;
                stuff = false;
                break;
            case 1:
                tile = false;
                stuff = true;
                break;
        }

        TileSelect.SetActive(tile);
        StuffSelect.SetActive(stuff);
        setChildCol(_floor, tile);
        setChildCol(_stuff, stuff);
    }

    public void setName(string name)
    {
        _name = name;
    }

    ///<summary>
    ///When the Create Button is pressed, Room will be saved as Prefab
    ///</summary>
    public void SavePrefab()
    {
        if (_name != null)
        {
            setChildCol(_floor, true);
            setChildCol(_stuff, true);
            Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Prefabs/Rooms/" + _name + ".prefab");
            PrefabUtility.ReplacePrefab(_level, prefab, ReplacePrefabOptions.ConnectToPrefab);
        }
    }

    ///<summary>
    ///Load Prefab from Rooms folder, if it does not load check Debug log
    ///</summary>
    public void LoadPrefab()
    {
        if (_name != null)
        {
            Object pref = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Rooms/" + _name + ".prefab", typeof(GameObject));
            if (pref != null)
            {
                Destroy(_level);
                _level = (GameObject)Instantiate(pref);
                Transform[] children = _level.GetComponentsInChildren<Transform>();
                foreach(Transform child in children)
                {
                    if (child.name.Equals("Stuff"))
                        _stuff = child.gameObject;
                    else if (child.name.Equals("Floor"))
                        _floor = child.gameObject;
                }

                setMode(_mode);
            }
            else
                Debug.Log("No prefab found with name: " + _name);
        }
        else
            Debug.Log("No name");
    }

    ///<summary>
    ///When Level is finished, use finalize to remove all empty tiles
    ///You can only finalize when StartTile is present
    ///</summary>
    public void Finalize()
    {
        if (_name != null)
        {
            if (_start != null)
            {
                SavePrefab();
                GameObject[] empties = GameObject.FindGameObjectsWithTag("Empty");
                _start.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                string name = _name;
                _name = _name + "_final";
                Transform[] children = _level.GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    if (child.name.Equals("Empty(Clone)"))
                    {
                        Destroy(child.gameObject);
                    }
                }
                StartCoroutine(SaveAfterDestroy(name));
            }
            else
                Debug.Log("StartTile does not Exist");
            
        }
        else
            Debug.Log("Name is empty!");
        
    }

      ///<summary>
    ///delete current Level and start from scratch
    ///</summary>
    public void Reset()
    {
        Destroy(_level);
        _level = new GameObject("Level");
        _floor = new GameObject("Floor");
        _stuff = new GameObject("Stuff");
        _floor.transform.parent = _level.transform;
        _stuff.transform.parent = _level.transform;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                GameObject tile = (GameObject)Instantiate(Empty);
                tile.transform.position = new Vector3(j * _step, i * _step, 0);
                tile.transform.parent = _floor.transform;

                GameObject tile2 = (GameObject)Instantiate(Empty);
                tile2.transform.position = new Vector3(j * _step, i * _step, 0);
                tile2.transform.parent = _stuff.transform;
            }
        }

        setMode(_mode);
    }

    void HandleStuff(RaycastHit2D hit)
    {
        switch (_stuffNmb)
        {
            case 0:
                replace(hit.transform.gameObject, Key);
                break;
            case 1:
                replace(hit.transform.gameObject, Door);
                break;
            case 2:
                connect(hit.transform.gameObject);
                break;
            case 3:
                replace(hit.transform.gameObject, Empty);
                break;

        }
    }

    void connect(GameObject obj)
    {
        if (!obj.tag.Equals("Empty"))
        {
            if(_con == null && obj.transform.tag.Equals("Key"))
            {
                _con = obj;
                Debug.Log("connect step 1");
            }
            else if(obj.transform.tag.Equals("Door")) {
                _con.GetComponent<Key>().door = obj.GetComponent<Door>();
                _con = null;
                Debug.Log("Connect finish");
            }
        }
    }
    void HandleTiles(RaycastHit2D hit)
    {
        switch (_tileNmb)
        {
            case 0:
                addFloorTile(hit);
                break;
            case 1:
                replace(hit.transform.gameObject, Wall);
                break;
            case 2:
                if (_start != null)
                {
                    _start.transform.tag = "Floor";
                    _start.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                }
                _start = addFloorTile(hit);
                _start.transform.tag = "Start";
                _start.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                break;
            case 3:
                replace(hit.transform.gameObject, Goal);
                break;
            default:
                replace(hit.transform.gameObject, Empty);
                break;
        }
    }

    ///<summary>
    ///You have to wait 1 frame until gameObjects are destroyed
    ///</summary>
    IEnumerator SaveAfterDestroy(string name)
    {
        yield return new WaitForEndOfFrame();
        SavePrefab();
        _name = name;
        LoadPrefab();
    }

    /// <summary>
    /// adds Floor depending on Position
    /// </summary>
    /// <returns>created FloorTile</returns>
    GameObject addFloorTile(RaycastHit2D hit) {
        Vector2 gridPos = toGridPos(hit.transform.localPosition);
        if ((int)gridPos.x != gridPos.x || (int)gridPos.y != gridPos.y)
        {
            Debug.LogError("Conversion Error gridPos");
        }
        if ((gridPos.x + gridPos.y) % 2 == 0)
        {
            return replace(hit.transform.gameObject, TileA);
        }
        else
        {
            return replace(hit.transform.gameObject, TileB);
        }
    }


    ///<summary>
    ///Used to determine position of tile in grid
    ///</summary>
    ///<param name="realPos">transform.localPosition of a tile object</param>
    Vector2 toGridPos(Vector3 realPos)
    {
        return new Vector2(realPos.x / GameModel.Instance.Step, realPos.y / GameModel.Instance.Step);
    }

    ///<summary>
    ///replaces one tile with prefab of chosen new tile
    ///</summary>
    /// <returns>created FloorTile</returns>
    GameObject replace(GameObject old, GameObject pref)
    {
        GameObject cur = (GameObject)Instantiate(pref);
        cur.transform.position = old.transform.position;
        Destroy(old);
        switch (_mode)
        {
            case 0: cur.transform.parent = _floor.transform; break;
            case 1: cur.transform.parent = _stuff.transform; break;
        }
        
        return cur;
    }

    void setChildCol(GameObject obj, bool mode)
    {
        Collider2D[] floorCols = obj.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in floorCols)
            col.enabled = mode;
    }
    ///<summary>
    ///auto save current editor progress when closed
    ///</summary>
    void OnApplicationQuit()
    {
        _name = "cur";
        SavePrefab();
    }
}
