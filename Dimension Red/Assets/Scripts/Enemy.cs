using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float aggroRange;
    GameObject aggro;
    Map map;
    Moving movement;

    Vector2 aggroDist;

    float onTileX, onTileY;
    Damageable HP;
    bool damagedOnce = false;

    // Use this for initialization
    void Start () {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        aggroRange *= map.GetTileSize();
        movement = GetComponent<Moving>();
        HP = GetComponent<Damageable>();
        HP.SetHealth(100);
    }
	
	// Update is called once per frame
	void Update () {
        if (aggro == null)
            GetAggro();
		else
        {
            aggroDist = transform.localPosition - aggro.transform.localPosition;
            if (Vector2.Distance(transform.localPosition, aggro.transform.localPosition) > aggroRange)
                GetAggro();

            onTileX = Round2Places(transform.localPosition.x % map.GetTileSize());
            onTileY = Round2Places(transform.localPosition.y % map.GetTileSize());
            if (onTileX == 0 && onTileY == 0)
            {
                if (aggroDist.x > 0)
                {                      
                    movement.MoveWest();
                    damagedOnce = false;
                }
                else if (aggroDist.x < 0)
                {
                    movement.MoveEast();
                    damagedOnce = false;
                }               
                else if (aggroDist.y < 0)
                {
                    movement.MoveNorth();
                    damagedOnce = false;
                }
                else if (aggroDist.y > 0)
                {
                    movement.MoveSouth();
                    damagedOnce = false;
                }
                else if (movement.GetCurrTile().GetID() == aggro.GetComponent<Moving>().GetCurrTile().GetID() && !damagedOnce)
                {
                    aggro.GetComponent<Damageable>().Damage(20);
                    damagedOnce = true;
                }
            }

            
        }
	}

    void GetAggro()
    {
        List<GameObject> characters = map.GetCharacters();
        aggro = null;
        foreach(GameObject hero in characters) {
            if(Vector2.Distance(transform.localPosition, hero.transform.localPosition) <= aggroRange)
            {
                aggro = hero;
                break;
            }
        }
    }

    float Round2Places(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }
}
