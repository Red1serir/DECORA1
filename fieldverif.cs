using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class fieldverif : MonoBehaviour {
	public InputField username;
	public InputField password;
	public RectTransform message;
	public Text messageText;
	public  void Start() {
		password.contentType=InputField.ContentType.Password;
	}

	// Use this for initialization
public void snakBar()
{
	if(username.text=="")
	{if(password.text=="")
	  {
		messageText.text="Veuillez remplir les 2 champs";
	  }
	  else
	  {messageText.text="veuillez introduire un username";
		  
	  }
	
       message.DOAnchorPos(new Vector2(0,-467.26f),0.4f);
		 Invoke("goback",3); 
	}
	else
	{ if(password.text=="")
	 {
		 messageText.text="Veuillez introduire un Password";
		 message.DOAnchorPos(new Vector2(0,-467.26f),0.4f);
		 Invoke("goback",3); 
	 }
	 
		
	}
}
public void goback()
{
	message.DOAnchorPos(new Vector2(0,-7200),0.4f);

	
}
}
