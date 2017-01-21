using UnityEngine;
using System.Collections;

public class OrderSpriteByZ : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    public bool updateOrder = false;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)(gameObject.transform.position.z * -1000);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
