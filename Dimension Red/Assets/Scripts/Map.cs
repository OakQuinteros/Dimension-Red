using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Map: MonoBehaviour {

    Tile[] tiles;
    public GameObject wall_prefab;
    public List<GameObject> char_prefabs, enemy_prefabs, item_prefabs;
    List<GameObject> characters, items, enemies;
    static int charID, tileID;
    public float tileSize;
    public GameObject tileOpen;
    public GameObject tileLoot;
    public GameObject enemy;
    int rows, cols;

	// Use this for initialization
	void Start () {
        characters = new List<GameObject>();
        enemies = new List<GameObject>();
        items = new List<GameObject>();
        tileSize = tileOpen.GetComponent<Renderer>().bounds.size.x;
        ReadMap();     
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject hero in characters)
        {
            if (hero == null)
            {
                characters.Remove(hero);
                break;
            }
        }
    }

    public void ReadMap()
    {       
        StreamReader reader = File.OpenText(Application.dataPath + "\\map.txt");
        string line;
        int r = 0;
        int c = 0;
        tileID = 0;
        bool endOfFile = false;
        int tempRows, tempCols;

        while ((line = reader.ReadLine()) != null && !endOfFile)
        {
            string[] spaces = line.Split(' ');

            if (spaces[0].Equals("Legend:"))
                endOfFile = true;
            else if (int.TryParse(spaces[0], out tempRows) && int.TryParse(spaces[1], out tempCols))
            {
                rows = tempRows - 2;
                cols = tempCols - 2;          
                tiles = new Tile[rows * cols];
            }
            else {
                foreach (string s_tile in spaces)
                {
                    char tile = s_tile[0];
                    switch (tile)
                    {
                        case 'O':
                            CreateTile(tileOpen, r, c);
                            break;
                        case 'W':
                            CreateWall(r, c);
                            break;
                        case 'R':
                            CreateTile(tileOpen, r, c);
                            PlaceCharAt(char_prefabs[0], r, c);
                            break;
                        case 'E':
                            CreateTile(tileOpen, r, c);
                            PlaceEnemyAt(enemy_prefabs[0], r, c);
                            break;
                        case 'L':
                            CreateTile(tileLoot, r, c);
                            PlaceItemAt(item_prefabs[0], r, c);
                            break;
                    }
                    c++;
                }
                r++;
                c = 0;
            }
        }
    }

    public Tile GetTile(int tID)
    {
        return tiles[tID];
    }

    public Tile GetTile(int r, int c)
    {
        return tiles[r * cols + c];
    }

    /*  Get Tile Coordinate with tileID */
    public Tile GetCoord(int tID)
    {
        return tiles[tID];
    }

    public Tile GetSouthCoord(int tID)
    {
        if (tID + cols < rows * cols)
            return tiles[tID + cols];
        else
            return tiles[tID];
    }               

    public Tile GetNorthCoord(int tID)
    {
        if (tID - cols >= 0)
            return tiles[tID - cols];
        else
            return tiles[tID];
    }

    public Tile GetWestCoord(int tID)
    {
        if (tID % cols != 0)
            return tiles[tID - 1];
        else
            return tiles[tID];
    }

    public Tile GetEastCoord(int tID)
    {
        if (tID % cols != cols - 1)
            return tiles[tID + 1];
        else
            return tiles[tID];
    }

    /*  Get Tile Coordinate with row and column */
    public Tile GetCoord(int r, int c)
    {
        return tiles[r * cols + c];
    }

    public Tile GetSouthCoord(int r, int c)
    {
        int tID = r * cols + c;
        if (tID + cols < rows * cols)
            return tiles[tID + cols];
        else
            return tiles[tID];
    }

    public Tile GetNorthCoord(int r, int c)
    {
        int tID = r * cols + c;
        if (tID - cols >= 0)
            return tiles[tID - cols];
        else
            return tiles[tID];
    }

    public Tile GetWestCoord(int r, int c)
    {
        int tID = r * cols + c;
        if (tID % cols != 0)
            return tiles[tID - 1];
        else
            return tiles[tID];
    }

    public Tile GetEastCoord(int r, int c)
    {
        int tID = r * cols + c;
        if (tID % cols != cols - 1)
            return tiles[tID + 1];
        else
            return tiles[tID];
    }

    public float GetTileSize()
    {
        return tileSize;
    }

    public void CreateTile(GameObject tile, int row, int col)
    {
        GameObject tileObject = Instantiate(tile);
        tileObject.GetComponent<Tile>().SetID(tileID);
        tileObject.transform.SetParent(transform);

        float x = Mathf.Round(col * tileSize * 100f) / 100f;
        float y = Mathf.Round(-row * tileSize * 100f) / 100f;
        tileObject.transform.localPosition = new Vector2(x, y);
        //tileObject.transform.localScale = new Vector3(1, 1, 1);
        tiles[tileID] = (tileObject.GetComponent<Tile>());
        tileID++;
    }

    public void CreateWall(int row, int col)
    {
        GameObject newWall = Instantiate(wall_prefab);
        newWall.transform.SetParent(transform);

        float x = Mathf.Round(col * tileSize * 100f) / 100f;
        float y = Mathf.Round(-row * tileSize * 100f) / 100f;
        newWall.transform.localPosition = new Vector2(x, y);
        //newWall.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void PlaceCharAt(GameObject character, int row, int col)
    {
        GameObject newChar = Instantiate(character);
        newChar.transform.SetParent(transform);
        newChar.transform.localPosition = GetCoord(tileID - 1).transform.localPosition;
        newChar.GetComponent<Moving>().SetTile(tiles[tileID-1]);      
        characters.Add(newChar);
    }

    public void PlaceEnemyAt(GameObject enemy, int row, int col)
    {
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.SetParent(transform);
        newEnemy.transform.localPosition = GetCoord(tileID - 1).transform.localPosition;
        newEnemy.GetComponent<Moving>().SetTile(tiles[tileID - 1]);
        enemies.Add(newEnemy);
    }

    public void PlaceItemAt(GameObject item, int row, int col)
    {
        GameObject newItem = Instantiate(item);
        newItem.transform.SetParent(GetTile(tileID - 1).transform);
        newItem.transform.localPosition = new Vector2(0, 0);
        items.Add(newItem);
    }

    public List<GameObject> GetCharacters()
    {
        return characters;
    }
}
