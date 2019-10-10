using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;

public class featureScript : MonoBehaviour {
    public GameObject meubleFavoris,meublePanier,meubleAchats;
	public GameObject FavorisContainer;
	public GameObject PanierContainer;
	public GameObject Achatscontainer;
	


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
    // Use this for initialization
    // Update is called once per frame
    public void AjouterFavoris()
	{ if(GameObject.Find("meubleFavori"+int.Parse((GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/ID").GetComponent<Text>().text)))==null)
	   {GameObject Fav=Instantiate(meubleFavoris);
	  Fav.transform.SetParent(FavorisContainer.transform);
	  Fav.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
	  Fav.GetComponent<RectTransform>().localPosition=new Vector3(320,-117.35f-((FavorisContainer.transform.childCount-1)*200),0);
	  Fav.name="meubleFavori"+GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/ID").GetComponent<Text>().text;
	  GameObject.Find(Fav.name+"/Description").GetComponent<Text>().text=GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/Description").GetComponent<Text>().text;
	  GameObject.Find(Fav.name+"/ImageDuMeuble").GetComponent<Image>().sprite=GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/ImageDuMeuble").GetComponent<Image>().sprite;
	  GameObject.Find(Fav.name+"/Prix").GetComponent<Text>().text=  GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/Prix").GetComponent<Text>().text;
	   GameObject.Find(Fav.name+"/ID").GetComponent<Text>().text=GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/ID").GetComponent<Text>().text;
	  StartCoroutine(chargerFavoris(Fav));
	 
	   
	   
	   
	   
	   }
	   else
	   {
		  GameObject.Find("snakbar/Text").GetComponent<Text>().text="Existe Deja dans votre Liste Favoris";
		  GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-440),0.3f);
		  Invoke("goback",2.5f);

	   }
	}

    IEnumerator chargerFavoris(GameObject Fav)
    {WWWForm form=new WWWForm();
	form.AddField("username",GameObject.Find("USERNAME").GetComponent<Text>().text);
	form.AddField("idmeuble",GameObject.Find(Fav.name+"/ID").GetComponent<Text>().text);

	using (UnityWebRequest www=UnityWebRequest.Post("http:// 192.168.43.50 /unity/chargerfavoris.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		 
		}
		}

        
		}

    public void AjouterPanier( )
	{ GameObject Fav=Instantiate(meublePanier);
	  Fav.transform.SetParent(PanierContainer.transform);
	  Fav.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
	  Fav.GetComponent<RectTransform>().localPosition=new Vector3(320,-117.35f-((PanierContainer.transform.childCount-1)*200),0);
	  Fav.name="meublePanier"+PanierContainer.transform.childCount;
	   GameObject.Find("meublePanier"+PanierContainer.transform.childCount+"/Description").GetComponent<Text>().text= GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/Description").GetComponent<Text>().text;
	  GameObject.Find("meublePanier"+PanierContainer.transform.childCount+"/ImageDuMeuble").GetComponent<Image>().sprite=  GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/ImageDuMeuble").GetComponent<Image>().sprite;
	  GameObject.Find("meublePanier"+PanierContainer.transform.childCount+"/Prix").GetComponent<Text>().text=  GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/Prix").GetComponent<Text>().text;
	   
	  GameObject.Find(Fav.name+"/ID").GetComponent<Text>().text=GameObject.Find(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.name+"/ID").GetComponent<Text>().text;

	
    float prixtotal=0;
for	(int i=1;i<=PanierContainer.transform.childCount;i++){
       prixtotal+=float.Parse(  GameObject.Find("meublePanier"+i+"/Prix").GetComponent<Text>().text.Substring(10));
		 
	 }
	   GameObject.Find("prixTotal").GetComponent<Text>().text=prixtotal+"$";

	}
	public void enleverFavoris()
	{
		 StartCoroutine(deleteFavoris(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject));
	  Destroy( EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
	 
	}

    IEnumerator deleteFavoris(GameObject Fav)
    {
        WWWForm form=new WWWForm();
	form.AddField("username",GameObject.Find("USERNAME").GetComponent<Text>().text);
	form.AddField("idmeuble",GameObject.Find(Fav.name+"/ID").GetComponent<Text>().text);

	using (UnityWebRequest www=UnityWebRequest.Post("http:// 192.168.43.50/unity/enleverfavoris.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		 
		}
		}

        
		}

    

    public void enleverPanier()
	{
	  Destroy( EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
	}
	public void enleverToutDuPanier()
	{for (int r = 0; r < EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(2).GetChild(0).GetChild(0).childCount; r++)
	{Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(2).GetChild(0).GetChild(0).GetChild(r).gameObject);
		
	}
	
	}
	public void goback()
	 {
		 GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-690),0.3f);
	 }
	 public void ValiderCommande()
	 { string s="";
		 GameObject a=EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetChild(0).GetChild(0).gameObject ;
		 for(int i=0; i<EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetChild(0).GetChild(0).childCount;i++)
		 {   
			 s=s+GameObject.Find( a.transform.GetChild(i).name+"/ID").GetComponent<Text>().text+"|";
		  Destroy(GameObject.Find( a.transform.GetChild(i).name));
             
		 }
		
if(EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetChild(0).GetChild(0).childCount!=0)
		 {GameObject.Find("prixTotal").GetComponent<Text>().text="0$";
		 GameObject.Find("snakbar").transform.GetChild(0).GetComponent<Text>().text="Commande validé!!";
		 GameObject.Find("snakbar").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,-475),0.3f);
		 Invoke("goback",2.5f);}
		 StartCoroutine(remplirCommende(s));
		  
		  
		 
	 }
	 IEnumerator remplirCommende(string listeID)
	 {
		     WWWForm form=new WWWForm();
	form.AddField("listeID",listeID);
	form.AddField("client",GameObject.Find("USERNAME").GetComponent<Text>().text);

	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/commande.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		 
		
		}
		} 
		 string[] words=listeID.Split('|');
		 for(int i=0 ;i<words.Length-1;i++)
		 {
			 StartCoroutine(ajouterListAchat(int.Parse(words[i])));
		 }
		 
		 for(int i=0;i<Achatscontainer.transform.childCount;i++)
		  {
			  Destroy(Achatscontainer.transform.GetChild(i).gameObject);
		  }
		  StartCoroutine(chargerAchats());
		 

    
	 }
	public IEnumerator chargerAchats()
	 {   WWWForm form=new WWWForm();
	
	form.AddField("username",GameObject.Find("USERNAME").GetComponent<Text>().text);

	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/listeAchats.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		
		 string[] Words=www.downloadHandler.text.Split('|');
		 for(int i=0;i<Words.Length-1;i++)
		 { WWWForm form2=new WWWForm();
	
		form2.AddField("idmeuble",Words[i]);
		form2.AddField("username",GameObject.Find("USERNAME").GetComponent<Text>().text);

	using (UnityWebRequest www2=UnityWebRequest.Post("http://192.168.43.50/unity/achats.php",form2))
	{
		yield return www2.SendWebRequest();	
		if(www2.isNetworkError||www2.isHttpError)
		      {Debug.Log(www2.error);
		}
		else
		{
			 Debug.Log(www2.downloadHandler.text);
			string[] words2= www2.downloadHandler.text.Split('|');
			 GameObject Fav=Instantiate(meubleAchats);
	  Fav.transform.SetParent(Achatscontainer.transform);
	  Fav.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
	  Fav.GetComponent<RectTransform>().localPosition=new Vector3(320,-117.35f-((Achatscontainer.transform.childCount-1)*200),0);
	  Fav.name="meubleAchats"+Achatscontainer.transform.childCount;
	   GameObject.Find("meubleAchats"+Achatscontainer.transform.childCount+"/Description").GetComponent<Text>().text=words2[1];
	   GameObject.Find("meubleAchats"+Achatscontainer.transform.childCount+"/Prix").GetComponent<Text>().text="PRIX:    "+words2[2];
	GameObject.Find("meubleAchats"+Achatscontainer.transform.childCount+"/qte").GetComponent<Text>().text="X"+words2[0];
	GameObject.Find("meubleAchats"+Achatscontainer.transform.childCount+"/ID").GetComponent<Text>().text=Words[i];
	GameObject.Find("meubleAchats"+Achatscontainer.transform.childCount+"/ImageDuMeuble").GetComponent<Image>().sprite=GameObject.Find("meuble"+GameObject.Find("meubleAchats"+Achatscontainer.transform.childCount+"/ID").GetComponent<Text>().text+"/ImageDuMeuble").GetComponent<Image>().sprite;

		}

		 }
		}
		}
		 
		 	 }
}


IEnumerator ajouterListAchat(int idmeuble)

{ WWWForm form=new WWWForm();
	
	form.AddField("username",GameObject.Find("USERNAME").GetComponent<Text>().text);
	form.AddField("idmeuble",idmeuble);

	using (UnityWebRequest www=UnityWebRequest.Post("http://192.168.43.50/unity/ajouterAchats.php",form))
	{
		yield return www.SendWebRequest();	
		if(www.isNetworkError||www.isHttpError)
		{
			Debug.Log(www.error);
		
		}
		else
		{
		 Debug.Log(www.downloadHandler.text);
		 
		
		}
		
		
	}

}
}
