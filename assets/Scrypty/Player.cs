using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : ScriptableObject {

    public enum InfluenceType
    {
        Church,
        Folk,
        Court,
        Council
    };

    public int HandSize;
    public List<CardClass> Hand;
    public int MaxHandSize;

    //kosiola, ludu, dworu, rady
    public float[] Influences;

    public int Gold;

    public bool hasPatron;

    private Canvas canvasRef;
    private bool needUpdate = true;

    Player()
    {
        HandSize = 0;
        MaxHandSize = 5;
        hasPatron = false;
        Gold = 100;
        Influences = new float[] { 1f, 1f, 1f, 1f };
        Hand = new List<CardClass>();
    }


	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
    public void Update ()
    {        
        if (canvasRef == null)
        {
            canvasRef = FindObjectOfType<Canvas>();
        }

        if (canvasRef != null && needUpdate)
        {
            #region Set influance sliders
            float SumedInf = SumInfluances();
            GameObject sliderParent = canvasRef.transform.FindChild("Church").gameObject;
            GameObject sliderObject;
            if (sliderParent != null)
            {
                sliderObject = sliderParent.transform.FindChild("SliderChurch").gameObject;
                sliderObject.GetComponent<Slider>().value = Influences[0] / SumedInf;                
            }

            sliderParent = canvasRef.transform.FindChild("Folk").gameObject;
            if (sliderParent != null)
            {
                sliderObject = sliderParent.transform.FindChild("SliderLud").gameObject;
                sliderObject.GetComponent<Slider>().value = Influences[1] / SumedInf;                
            }

            sliderParent = canvasRef.transform.FindChild("Court").gameObject;
            if (sliderParent != null)
            {
                sliderObject = sliderParent.transform.FindChild("SliderDwór").gameObject;
                sliderObject.GetComponent<Slider>().value = Influences[2] / SumedInf;
            }
            #endregion

            #region setup cards
            int i = 0;
            for (; i < Hand.Count; ++i )
            {
                Hand[i].SetCardInstance(i);
            }
            string nazwa;
            for (; i < 5; ++i)
            {
                nazwa = "PanelForCards/Karta" + i.ToString();
                GameObject karta = canvasRef.transform.FindChild(nazwa).gameObject;
                karta.SetActive(false);
            }
            #endregion

            needUpdate = false;
        }
    }
	
    public void ChangeInfluences(InfluenceType t, int value)
    {
        switch(t)
        {
            case InfluenceType.Church:
                Influences[0] += value;
                break;
            case InfluenceType.Council:
                Influences[3] += value;
                break;
            case InfluenceType.Court:
                Influences[2] += value;
                break;
            case InfluenceType.Folk:
                Influences[1] += value;
                break;
        };
    }

    public void ChangeGold(int value)
    {
        Gold += value;
    }	   

    public void AddCard(CardClass newC)
    {
        Hand.Add(newC);
        newC.Owner = this;
        needUpdate = true;
    }

    public CardClass RemoveCard(int idx)
    {
        CardClass temp = Hand[idx];
        temp.Owner = null;
        Hand.RemoveAt(idx);        
        needUpdate = true;
        return temp;
    }

    private float SumInfluances()
    {
        float sum = 0;

        for (int i = 0; i < 4; ++i)
        {
            sum += Influences[i];
        }        
        return sum;
    }
}
