using UnityEngine;
using System.Collections;

public class CardHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CanUse(CardClass cc, Player P)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (P.Influences[i] < cc.Costs[i])
                return false;
        }

        if (P.Gold < cc.Costs[4]) return false;

        if (cc.AdditionalCondition == "Cech posiada patrona.")
        {
                return P.hasPatron;
        }

        if (cc.AdditionalCondition == "Nie istnieje jeszcze żadna kaplica w Ratuszu")
        {
            //znajdź obiekt planszy
            //sprawdźczy w ratuszu jest kaplica
        }

        return true;
    }

    public void Handle(CardClass cc, Player P)
    {
        if (cc.ID == 2002)
        {
            P.hasPatron = true;
        }

        for (int i = 0; i < 4; ++i)
        {
            P.Influences[i] -= cc.Costs[i];
            P.Influences[i] += cc.Benefits[i];
        }
    }
}
