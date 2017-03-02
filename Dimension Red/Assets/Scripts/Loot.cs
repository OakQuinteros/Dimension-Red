using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    public Sprite[] lootOptions;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator LootToShinyBit()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Living";
        GetComponent<SpriteRenderer>().sprite = lootOptions[0];

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
