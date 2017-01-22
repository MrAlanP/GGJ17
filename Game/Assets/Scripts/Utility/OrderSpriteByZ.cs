using UnityEngine;
using System.Collections;

public class OrderSpriteByZ : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    public bool updateOrder = false;
    public int orderOffset = 0;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetOrder();
        //spriteRenderer.sortingOrder = (int)(gameObject.transform.position.z * -1000) + 10000;
    }
	
	// Update is called once per frame
	void Update () {
        if (updateOrder)
        {
            SetOrder();
        }
	}

    void SetOrder()
    {
        spriteRenderer.sortingOrder = (int)(gameObject.transform.position.z * -100) + 1000 + orderOffset;
    }
}
