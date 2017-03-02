using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

    public float speed;
    Tile currTile, moveToTile;
    Map map;
    Vector2 moveTo;
    bool moveN, moveS, moveW, moveE = false;
    float onTileX, onTileY;

    enum Directions { NORTH, SOUTH, EAST, WEST };
    Directions direction;
    // Use this for initialization
    void Start () {
        direction = Directions.WEST;
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
    }
	
	// Update is called once per frame
	void Update () {
        if (moveN)
        {
            moveTo = moveToTile.transform.localPosition;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * speed);
            if (transform.localPosition.y >= moveTo.y)
            {
                moveN = false;
                transform.localPosition = moveTo;
                currTile = moveToTile;
            }
        }
        if (moveS)
        {
            moveTo = moveToTile.transform.localPosition;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * speed);
            if (transform.localPosition.y <= moveTo.y)
            {
                moveS = false;
                transform.localPosition = moveTo;
                currTile = moveToTile;
            }
        }
        if (moveE)
        {
            moveTo = moveToTile.transform.localPosition;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * speed);
            if (transform.localPosition.x >= moveTo.x)
            {
                moveE = false;
                transform.localPosition = moveTo;
                currTile = moveToTile;
            }
        }
        if (moveW)
        {
            moveTo = moveToTile.transform.localPosition;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * speed);
            if (transform.localPosition.x < moveTo.x)
            {
                moveW = false;
                transform.localPosition = moveTo;
                currTile = moveToTile;
            }
        }      
    }

    public void MoveNorth()
    {
        moveToTile = map.GetNorthCoord(currTile.GetID());
        moveN = true;
    }
    public void MoveSouth()
    {
        moveToTile = map.GetSouthCoord(currTile.GetID());
        moveS = true;
    }
    public void MoveEast()
    {
        moveToTile = map.GetEastCoord(currTile.GetID());
        moveE = true;
    }
    public void MoveWest()
    {
        moveToTile = map.GetWestCoord(currTile.GetID());
        moveW = true;
    }

    public void SetTile(Tile newTile)
    {
        currTile = newTile;
    }

    public Tile GetCurrTile()
    {
        return currTile;
    }
}
