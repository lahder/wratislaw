using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    public CardManager CardManRef; // referencja do menadżera kart 
    private Player myOwner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        SetTargetBuilding();
    }

    public void SetTargetBuilding()
    {
        CardManRef.targetBuilding = this;
     }

    public void SetOwnership(Player newOwner)
    {
        myOwner = newOwner;
    }
}
