using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using DG.Tweening;
using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using UnityEngine.EventSystems;

public class signUp : MonoBehaviour {
    private const string Url = "localhost/index.php";
    public InputField username;
	
    public InputField Loginusername;
	
    public InputField Loginpassword;
	public InputField Nom;
	public InputField prenom;
	public InputField numero;
	public InputField adresse;
	public InputField Password;
	public RectTransform message;
	public Text messageText;
	public GameObject uIman;
	public GameObject controller;
	public GameObject meuble;
	public GameObject Meublecontainercuisine;
	public GameObject FavorisContainer;
    public GameObject meubleFavoris;
	public GameObject featureScript;
  

   
    public static string GetIPAddress()
    {
        IPHostEntry host;
        string localIP = "?";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
            }
        }
        return localIP;
    }

    public static bool IsPhoneNumber(string number)
    {   int i=0;
        return (int.TryParse(number,out i) && number.Length==10 &&(number.Substring(0,1).CompareTo("0")==0)&&(number.Substring(1,1).CompareTo("0")!=0));
    }

    // Use this for initialization
    void Start () {
	    
		Password.contentType=InputField.ContentType.Password;
		Debug.Log( GetIPAddress());
		
		
	}
	public void callRegister()
	{
		if(username.text.Length!=0 &&  Nom.text.Length!=0 && prenom.text.Length!=0 && Password.text.Length!=0 && numero.text.Length!=0 && adresse.text.Length!=0)
	 		{if(!IsPhoneNumber(numero.text))
			 {GameObject.Find("snakbar").transform.GetChild(0).GetComponent<Text>().text="numero de telephone invalide";
			GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-475),0.3f);
			Invoke("gobacksnake", 2);

			 }
			 else
			 {
				 StartCoroutine(Register());
			 uIman.GetComponent<UIman>().GotoLogin();
			 }

				 }
	 else 
		{   GameObject.Find("snakbar").transform.GetChild(0).GetComponent<Text>().text="veuillez remplire tout les champs";
			GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-475),0.3f);
			Invoke("gobacksnake", 2);
		}
	}
	public void callGetMeuble()
	{
	 StartCoroutine(GetMeuble());
	}
	public void callLogin()
	{   StartCoroutine(Login());
		
	}
	IEnumerator Register()
	{ WWWForm form=new WWWForm();
	form.AddField("username",username.text);
	form.AddField("Nom",Nom.text);
	form.AddField("Prenom",prenom.text);
	form.AddField("password",Password.text);
	form.AddField("numTel",numero.text);
	form.AddField("adresse",adresse.text);
	Debug.Log(username.text + Password.text);
	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/r.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		 if(www.downloadHandler.text.CompareTo("erreur utilisateur existe")==0)
		   {
			    GameObject.Find("snakbar").transform.GetChild(0).GetComponent<Text>().text="username deja utilisé ";
			GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-475),0.3f);
			Invoke("gobacksnake", 2);
		   }

		 
		}
		}

    }

	IEnumerator Login()
	{ WWWForm form=new WWWForm();
	form.AddField("username",Loginusername.text);
	
	form.AddField("password",Loginpassword.text);
	
	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/login.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		  messageText.text=www.downloadHandler.text;
		 message.DOAnchorPos(new Vector2(0,-465),0.4f);
		 Invoke("goback",3);
		 if(messageText.text.CompareTo("login success")==0)
		    {
				
				uIman.GetComponent<UIman>().gotoApp();
				GameObject.Find("USERNAME").GetComponent<Text>().text=Loginusername.text;
				StartCoroutine (chargerfavoris(Loginusername.text));
				
				StartCoroutine(featureScript.GetComponent<featureScript>().chargerAchats());
			
			}
		
		 
		}
		}

    }

    IEnumerator chargerfavoris(string text)
    {
       WWWForm form=new WWWForm();
	form.AddField("username",text);
	

	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/favoris.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		  string[] words= www.downloadHandler.text.Split('|');
		  
		  if(www.downloadHandler.text.CompareTo("0")!=0)
		  for (int i=0;i<words.Length-1 ; i++)
		  {Debug.Log(words[i]);
			  GameObject Fav=Instantiate(meubleFavoris);
	  Fav.transform.SetParent(FavorisContainer.transform);
	  Fav.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
	  Fav.GetComponent<RectTransform>().localPosition=new Vector3(320,-117.35f-((FavorisContainer.transform.childCount-1)*200),0);
	  Fav.name="meubleFavori"+words[i];
	  GameObject.Find(Fav.name+"/Description").GetComponent<Text>().text= GameObject.Find("meuble"+words[i]+"/Description").GetComponent<Text>().text;
	  GameObject.Find(Fav.name+"/ImageDuMeuble").GetComponent<Image>().sprite=  GameObject.Find("meuble"+words[i]+"/ImageDuMeuble").GetComponent<Image>().sprite;
	  GameObject.Find(Fav.name+"/Prix").GetComponent<Text>().text=  GameObject.Find("meuble"+words[i]+"/Prix").GetComponent<Text>().text;
	   GameObject.Find(Fav.name+"/ID").GetComponent<Text>().text=GameObject.Find("meuble"+words[i]+"/ID").GetComponent<Text>().text;
			  
		  }

		 
		}
		}

    }

    IEnumerator GetMeuble()
	{ WWWForm form=new WWWForm();
	 int i=0;
	
	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/meuble.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		  string[] words= www.downloadHandler.text.Split('|');
		  int c=0,s=0,ch=0;
		  for(i=1;i<words.Length;i=i+5)
		  {   GameObject meub=Instantiate(meuble);
			  
			  meub.name="meuble"+words[i-1];
		
		   if(words[i].CompareTo("cuisin,hge")==0)
		    { meub.transform.SetParent(Meublecontainercuisine.transform);
			  meub.GetComponent<RectTransform>().localPosition=new Vector3(320,-150-c*220,0);
			  c++;
			  meub.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
			    GameObject.Find("meuble"+words[i-1]+"/Description").GetComponent<Text>().text=words[i+1];
				 GameObject.Find("meuble"+words[i-1]+"/Prix").GetComponent<Text>().text="PRIX:      "+words[i+2];
				 GameObject.Find("meuble"+words[i-1]+"/ID").GetComponent<Text>().text=words[i-1];
				
			
				 WWW wwwloader=new WWW(words[i+3]);
				 yield return wwwloader;
				 Debug.Log(words[i+3]+ "hellpqsdqsds");
				 
				  GameObject.Find("meuble"+words[i-1]+"/ImageDuMeuble").GetComponent<Image>().sprite=Sprite.Create(wwwloader.texture, new Rect(0, 0, wwwloader.texture.width, wwwloader.texture.height), new Vector2(0, 0));
			}
		}
		 
		}
		}

    }

   
    public void goback()
	{
	message.DOAnchorPos(new Vector2(0,-725),0.4f);
	}
	public void gobacksnake()
	{
		GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-725),0.3f);
	}
	
	// Update is called once per frame
	
}
