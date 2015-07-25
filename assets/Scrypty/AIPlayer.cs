using UnityEngine;
using System.Collections;

public class AIPlayer : Player {

    CardManager CardManRef;    
	// Use this for initialization
	void Start () {
        CardManRef = FindObjectOfType<CardManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeTurn()
    {        
        CardManRef.GiveCardForPlayer(this);

        float gain = 0f;

        foreach (float f in Hand[0].Benefits)
        {
            gain += f;
        }

        if(gain > 0)
        {
            
        }
        else
        {

        }
    }
}
