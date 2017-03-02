using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    bool occupied;
	public int row, column;
    public int id;
    public enum TileTypes { Normal, Impassable, Escape, Loot };
    public TileTypes tileType;
    public Map map;

    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }

    public Tile()
    {
        occupied = true;
        id = 0;
        tileType = TileTypes.Normal;
    }

    public Tile(int newID)
    {
        occupied = true;
        id = newID;
        tileType = TileTypes.Normal;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool occ)
    {
        occupied = occ;
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int newID)
    {
        id = newID;
    }

    public Vector2 GetCoord()	//Need for A* Euc distance
    {
        return transform.position;
    }

    public TileTypes GetTileType()
    {
        return tileType;
    }

    public void SetTileType(TileTypes newType)
    {
        tileType = newType;
    }
    /*public void SendToMap()
    {
        map.Move(id);
    }*/
}
