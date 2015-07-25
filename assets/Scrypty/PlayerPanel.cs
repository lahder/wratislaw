using UnityEngine;
using System.Collections;

public class PlayerPanel : MonoBehaviour {

    public Player myPlayer;
    public CardManager CardManRef; // referencja do menadżera kart

	// Use this for initialization
	void Start () {
        myPlayer = Player.CreateInstance<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTargetPlayer()
    {
        CardManRef.otherOne = myPlayer;
    }
}
