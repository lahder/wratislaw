using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{    
    public string debugInfo;
    public List<CardClass> CardList;
    public List<CardClass> UsedCardList;
    public Player Player1;
    public Player otherOne = null;
    public Building targetBuilding = null;

    private CardClass CardToHandle;
    private int CardToHandleIndex;


	// Use this for initialization
	void Start () {
        debugInfo = "start";
        ReadCardsFromFiles();
        Player1 = Player.CreateInstance<Player>(); 
	}

    public GameObject Karta;

	// Update is called once per frame
	void Update ()    
    {
        Player1.Update();

        if (CardToHandle != null)
        {
            if ((CardToHandle.requiresTargetBuilding && targetBuilding)
              || (CardToHandle.requiresTargetPlayer && otherOne))
            {
                Handle(CardToHandle, Player1, otherOne);
                UsedCardList.Add(Player1.RemoveCard(CardToHandleIndex));
                CardToHandle = null;
            }
        }
    }

    public void ReadCardsFromFiles()
    {
        debugInfo = "zaczynam czytac";
        StreamReader CardSource = new StreamReader(@"D:\Wracislaw\Wracislaw\Assets\KARTY_CSV.csv");        
        string[] CardSourceLines = CardSource.ReadToEnd().Split('\n');        
        string[] lineSp;
        CardClass newCard;        
        CardSource.Close();
        CardList = new List<CardClass>();

        int debugCount = 0;

        foreach (string line in CardSourceLines)
        {
            debugCount++;
            lineSp = line.Split(';');
            if (lineSp[0].Length == 0 || lineSp[0] == "ID:" || lineSp[0] == "0")
                continue;

            debugInfo = lineSp[2];

    //ID:;Il kart:;Tytuł:;Opis:;Działanie:;
    //Dodatkowy warunek:;Koszt Kościół:;Koszt Lud:;Koszt Dwór:;
    //Koszt Rada:;Koszt Kasa:;Zysk Kościół:;Zysk Lud:;Zysk Dwór:;Zysk Rada:;Zysk Kasa:;E;F
            int amount = int.Parse(lineSp[1]);
            for (int i = 0; i < amount; ++i)
            {
                newCard = CardClass.CreateInstance<CardClass>();
                newCard.CreateFromLine(lineSp);
                CardList.Add(newCard);
            }
        }       
    }

    public void CreateCard()
    {               
        if (Player1.Hand.Count < Player1.MaxHandSize)
        {
            CardClass temp = GetRandomCard();
            Player1.AddCard(temp);            
            // tutaj odrazu mozna sprawdzic czy karty da sie uzyc
            // jak nie mozna ja 'zacieniowac' na czerwono
            // ew. mozna sie bawic w szczegoly i zakolorwac brakujace statsy
        }
    }

    public void GiveCardForPlayer(Player P)
    {
        if (P.Hand.Count < P.MaxHandSize)
        {
            CardClass temp = GetRandomCard();
            P.AddCard(temp);
        }
    }

    public void TryUseCard(int idx)
    {
        Debug.LogFormat("przed uzyciem karty: {0}", idx);

        if (CardToHandle != null)
            return;

        if (true)//CanUse(Player1.Hand[idx], Player1))
        {
            if (Player1.Hand[idx].requiresTargetBuilding
                || Player1.Hand[idx].requiresTargetPlayer
                || Player1.Hand[idx].requiresTargetAgent)
            {
                CardToHandle = Player1.Hand[idx];
                CardToHandle.markInUse();
                CardToHandleIndex = idx;
            }
            else 
            {
                otherOne = Player1;
                Handle(Player1.Hand[idx], Player1, otherOne);
                UsedCardList.Add(Player1.RemoveCard(idx));
            }
        }
    }

    public CardClass GetRandomCard()
    {
        if (UsedCardList == null)
            UsedCardList = new List<CardClass>();

        if (CardList.Count == 0)
        {
            CardList = UsedCardList;
            UsedCardList = new List<CardClass>();
        }

        // TODO: nie ma sprawdzenia czy jest coś do wylosowania

        int randInt = Random.Range(0, CardList.Count - 1);
        CardClass toReturn = CardList[randInt];
        CardList.RemoveAt(randInt);        

        return toReturn;
    }

    public bool CanUse(CardClass cc, Player P)
    {
        for (int i = 0; i < 4; ++i)
        {
            //if (P.Influences[i] < cc.Costs[i])
            //    return false;
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

    public void Handle(CardClass cc, Player owner, Player target)
    {
        if (target == null) target = owner;

        if (cc.ID == 2002)
        {
            owner.hasPatron = true;
        }

        for (int i = 0; i < 4; ++i)
        {
            owner.Influences[i] -= cc.Costs[i];       
            target.Influences[i] += cc.Benefits[i];
        }
        cc.markInUse(true);
        otherOne = null;
        targetBuilding = null;        
    }

    public void SetTargetPlayer(Player other)
    {
        otherOne = other;
    }

    public void SetTargetBuilding(Building target)
    {
        targetBuilding = target;
    }
}