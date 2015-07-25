using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInstance : MonoBehaviour {

    List<Player> Players;

	// Use this for initialization
	void Start () {
        Players = new List<Player>();
        Players.Add(Player.CreateInstance<Player>());
        for (int i = 0; i < 3; ++i)
        { 
            Players.Add(AIPlayer.CreateInstance<AIPlayer>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
