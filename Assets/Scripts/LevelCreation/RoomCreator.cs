using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomCreator : MonoBehaviour {

    public GameObject TileA, TileB, Wall;
    public int Width, Height;
    public string Name;
    [Header("Wall Directions")]
    public bool Right;
    public bool Left;
    public bool Up;
    public bool Down;
    private GameObject _room;

    float _step;
    void Start () {
        CreateRoom();
    }

    ///<summary>
    ///Creates a room filled with tiles, use Inspector for configurations
    ///</summary>
	public void CreateRoom() {
        bool aTurn = true;
        _step = GameModel.Instance.Step;
        if (_room != null)
            Destroy(_room);
        _room = new GameObject(Name);
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                GameObject tile;
                if (aTurn)
                {
                    tile = (GameObject)Instantiate(TileA);
                }
                else
                {
                    tile = (GameObject)Instantiate(TileB);
                }
                aTurn = !aTurn;
                tile.transform.position = new Vector3(j*_step, i*_step, 0);
                tile.transform.parent = _room.transform;
            }
            aTurn = (Width % 2 == 0) ? !aTurn : aTurn;
        }
        CreateWalls();
    }
    ///<summary>
    ///Creates walls for the Room
    ///Directions: if true: means there is a Wall
    ///</summary>
    void CreateWalls()
    {
        GameObject tile;
        if (Down)
        {
            for(int i=0; i < Width; i++) {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(_step * i, -_step , 0);
                tile.transform.parent = _room.transform;
            }
            if (Left)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(-_step, -_step, 0);
                tile.transform.parent = _room.transform;
            }
            if (Right)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(_step* Width, -_step, 0);
                tile.transform.parent = _room.transform;
            }
        }
        if (Left)
        {
            for (int i = 0; i < Height; i++)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(-_step, _step * i, 0);
                tile.transform.parent = _room.transform;
            }
        }
        if (Up)
        {
            for (int i = 0; i < Width; i++)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(_step * i, _step * Height, 0);
                tile.transform.parent = _room.transform;
            }
            if (Left)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(-_step, _step * Height, 0);
                tile.transform.parent = _room.transform;
            }
            if (Right)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(_step * Width, _step * Height, 0);
                tile.transform.parent = _room.transform;
            }
        }
        if (Right)
        {
            for (int i = 0; i < Height; i++)
            {
                tile = (GameObject)Instantiate(Wall);
                tile.transform.position = new Vector3(_step * Width, _step * i, 0);
                tile.transform.parent = _room.transform;
            }
        }
    }

    ///<summary>
    ///When the Create Button is pressed, Room will be saved as Prefab
    ///</summary>
    public void CreatePrefab()
    {
        Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Prefabs/Rooms/"+ Name+ ".prefab");
        PrefabUtility.ReplacePrefab(_room, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }

    
	// Update is called once per frame
	void Update () {
		
	}
}
