using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardClass : ScriptableObject {

    //ID:;Il kart:;Tytuł:;Opis:;Działanie:;
    //Dodatkowy warunek:;Koszt Kościół:;Koszt Lud:;Koszt Dwór:;
    //Koszt Rada:;Koszt Kasa:;Zysk Kościół:;Zysk Lud:;Zysk Dwór:;Zysk Rada:;Zysk Kasa:;E;F

    public enum CardType
    {
        Church,
        Folk,
        Court,
        Council,
        Events,
        Unkown
    };

    public int ID;
    public int CopyCount;
    public string Title;
    public string Description;
    public string Action;
    public string AdditionalCondition;
    public CardType MyType;

    //kosiola, ludu, dworu, rady, kasa
    public float[] Costs;
    public float[] Benefits;

    public bool requiresTargetPlayer;
    public bool requiresTargetBuilding;
    public bool requiresTargetAgent;

    private GameObject karta;

    public Player Owner;

    public void CreateFromLine(string[] sourceInfo)
    {
        Costs = new float[5];
        Benefits = new float[5];	        

        ID = int.Parse(sourceInfo[0]);
        CopyCount = int.Parse(sourceInfo[1]);
        Title = sourceInfo[2];
        Description = sourceInfo[3];
        Action = sourceInfo[4];
        AdditionalCondition = sourceInfo[5];
        MyType = CardType.Church;
        //kosiola, ludu, dworu, rady, kasa
        for (int i = 0; i < 5; ++i )
        {
            Costs[i] = float.Parse(sourceInfo[6 + i]);
            Benefits[i] = float.Parse(sourceInfo[10 + i]);
        }       

        if (sourceInfo[16].Contains("A"))
            requiresTargetAgent = true;
        if (sourceInfo[16].Contains("B"))
            requiresTargetBuilding = true;
        if (sourceInfo[16].Contains("G"))
            requiresTargetPlayer = true;
    }

    public CardClass()
    {
        Costs = new float[5];
        Benefits = new float[5];
        MyType = CardType.Unkown;
    }

    public void SetCardInstance(int idx)
    {        
        Canvas canvasRef = FindObjectOfType<Canvas>();
        if (canvasRef == null)
        {
            //dupa
            return;
        }
        string nazwa = "PanelForCards/Karta" + idx.ToString();
        karta = canvasRef.transform.FindChild(nazwa).gameObject;        
        karta.SetActive(true);
        GameObject temp = karta.transform.FindChild("Root/Tytuł").gameObject;
        if (temp != null)
        {
            temp.GetComponent<Text>().text = Title;
        }

        temp = karta.transform.FindChild("Root/Opis").gameObject;
        if (temp != null)
        {
            temp.GetComponent<Text>().text = Description;
        }
        
        temp = karta.transform.FindChild("Root/Działanie").gameObject;
        if (temp != null)
        {
            temp.GetComponent<Text>().text = Action;
        }

        string []nazwy = {"K%","L%","D%"};

        for (int i = 0; i < 3; ++i)
        {
            nazwa = "Root/Wymagania/" + nazwy[i];
            temp = karta.transform.FindChild(nazwa).gameObject;
            if (temp != null)
            {
                temp.GetComponent<Text>().text = Costs[i].ToString() + "%";
            }
            nazwa = "Root/Efekty/" + nazwy[i];
            temp = karta.transform.FindChild(nazwa).gameObject;
            if (temp != null)
            {
                temp.GetComponent<Text>().text = Benefits[i].ToString() + "%";
            }
        }
        


        //var text = Karta.GetComponent<Text>();        
    }

    public override string ToString ()
    {
        return Title;
    }

    public void markInUse(bool revertToNormal = false)
    {
        Color tempColor = karta.GetComponent<Image>().color;
        if (revertToNormal) tempColor = tempColor * 2f;
        else tempColor = tempColor * 0.5f;       
        karta.GetComponent<Image>().color = tempColor;
    }
}
