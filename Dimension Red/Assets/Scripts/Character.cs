using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    Map map;
    //Animator charAnim;
    int charID;
    bool selected;
    Tile moveToTile;
    Vector2 moveTo;
    Damageable HP;
    Camera cam;

    public int attackRange;

    bool move = false;
    float onTileX, onTileY;
    float speed;
    Moving movement;
    public float lootTimer = 6;
    float lootCountdown = 6;

    // Use this for initialization
    void Start () {
        selected = true;
        charID = 0;
        //currDest = new Vector2(-100.0f, -100.0f);
        HP = GetComponent<Damageable>();
        HP.SetHealth(100);
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        speed = GetComponent<Moving>().speed;
        movement = GetComponent<Moving>();
    }
	
	// Update is called once per frame
	void Update () {
        onTileX = Round2Places(transform.localPosition.x % map.GetTileSize());
        onTileY = Round2Places(transform.localPosition.y % map.GetTileSize());
        if (onTileX == 0 && onTileY == 0 && Input.GetButtonDown("Fire1"))
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mousePos = cam.ScreenToWorldPoint(mousePos);
            Vector2 origin = map.transform.position;
            mousePos -= origin;

            int col = Mathf.RoundToInt(Mathf.Abs(mousePos.x / map.GetTileSize()));
            int row = Mathf.RoundToInt(Mathf.Abs(mousePos.y / map.GetTileSize()));
            moveToTile = map.GetTile(row - 1, col - 1);
            move = true;
        }

        if (move)
        {
            moveTo = moveToTile.transform.localPosition;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, moveTo, Time.deltaTime * speed);
            if (Round2Places(transform.localPosition.x) == Round2Places(moveTo.x) && Round2Places(transform.localPosition.y) == Round2Places(moveTo.y))
            {
                move = false;
                transform.localPosition = moveTo;
                GetComponent<Moving>().SetTile(moveToTile);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HP.Damage(25);
        }

        if (Input.GetKey(KeyCode.X))
        {
            Attack();
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            lootCountdown = lootTimer;
        }
    }

    public bool IsSelected()
    {
        return selected;
    }

    public void Attack()
    {
        Tile checkTileForLoot = movement.GetCurrTile();
        if (checkTileForLoot.GetTileType() == Tile.TileTypes.Loot)
        {
            lootCountdown -= Time.deltaTime;
            if (lootCountdown <= 0)
            {
                checkTileForLoot.SetTileType(Tile.TileTypes.Normal);
                Loot loot = checkTileForLoot.transform.GetChild(0).GetComponent<Loot>();
                StartCoroutine(loot.LootToShinyBit());              
            }
        }
    }

    

    float Round2Places(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }
}
