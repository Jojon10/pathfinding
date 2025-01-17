using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSpeachBuble : MonoBehaviour
{
	public Image carakterImage;

	public string[] caraktersName;
	private Sprite[] carakterSprits;
	public int carakter;

	public Text charakterNameText;
	public string textValue;
	public Text speach;
	
	//private int count = 0;

	// Start is called before the first frame update
	void Start()
	{
        carakterSprits = Resources.LoadAll<Sprite>("Characters");

        UpdateCarakter(carakter);

    }

	// Update is called once per frame
	void Update()
	{
		//zum testen Sp�ter sollte CarakterUpdate nur ausgef�hrt werden wenn neuer text angezeigt werden soll
		UpdateCarakter(carakter);

        speach.text = textValue;
        //ShowTextSlowly();
    }

	/*
	private void ShowTextSlowly()
	{
        if (count % 20 == 0)
            speach.text += textValue[count / 20];
        if (count == textValue.Length * 20)
            count = 0;
        count++;
    }
	*/
	

    private void UpdateCarakter(int carakter)
    {
		if (caraktersName.Length != carakterSprits.Length)
			throw new System.Exception("No name or Picture for every Caraktor");
		if (carakter == 0 || carakter <= carakterSprits.Length)
		{
			charakterNameText.text = caraktersName[carakter - 1];
			carakterImage.sprite = carakterSprits[carakter - 1];
		}
		else
			throw new System.Exception("Tried to akses undefined Carakter");
    }
	
	public string[] Texte = new string[7];

	Texte[0] = "x";
	Texte[1] = "y";
	Texte[2] = "z";


	public void CangeText(string text)
	{
		textValue = Texte[(int) text];
	}

}