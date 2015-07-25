using UnityEngine;
using System.Collections;

public class CardStack : MonoBehaviour {

    public CardManager CM_ref;
    public bool discardedPile;

	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
	    if (CM_ref != null)
        {
            Vector3 scale = transform.localScale;
            if (discardedPile)
                scale.y = CM_ref.UsedCardList.Count / 5f;
            else
                scale.y = CM_ref.CardList.Count / 5f;
            transform.localScale = scale;
        }
	}

    void OnMouseDown()
    {
        if (CM_ref != null && !discardedPile)
        {
            CM_ref.CreateCard();
        }
    }
}
