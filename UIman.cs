using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class UIman : MonoBehaviour {
	public RectTransform ListMeuble;
	public RectTransform cuisine;
	public RectTransform chambre;
	public RectTransform signin;
	public RectTransform Feature;
	public RectTransform Login;
	public RectTransform Favoris,Panier,Achats;
	bool cuisineIsMoved=false,salonIsMoved=false,ChambreIsMoved=false;
	

	// Use this for initializausition
	public void gotoSalon()
	{
		ListMeuble.DOAnchorPos(new Vector2(0,0),0.5f);
		salonIsMoved=true;
			if(cuisineIsMoved){
				cuisine.DOAnchorPos(new Vector2(-640,0),0.5f);
				cuisineIsMoved=false;
				}
				if(ChambreIsMoved){
					chambre.DOAnchorPos(new Vector2(640,0),0.5f);
					ChambreIsMoved=false;
					}
				
		
	}
	public void gobackTOhome()
	{
				ListMeuble.DOAnchorPos(new Vector2(-1301,0),0.5f);
				
				cuisine.DOAnchorPos(new Vector2(-640,0),0.5f);
				
				chambre.DOAnchorPos(new Vector2(640,0),0.5f);
				ChambreIsMoved=false;
				cuisineIsMoved=false;
				salonIsMoved=false;

	}
		public void Cuisine()
	{ 
				cuisine.DOAnchorPos(new Vector2(0,0),0.5f);
				cuisineIsMoved=true;
				if(ChambreIsMoved){
					chambre.DOAnchorPos(new Vector2(640,0),0.5f);
					ChambreIsMoved=false;
					}
				if(salonIsMoved)
				{
					ListMeuble.DOAnchorPos(new Vector2(-1301,0),0.5f);
					salonIsMoved=false;
					}

			
	}
		public void Chambre()
	{
				chambre.DOAnchorPos(new Vector2(0,0),0.5f);
				ChambreIsMoved=true;
				if(salonIsMoved)
				{
					ListMeuble.DOAnchorPos(new Vector2(-1301,0),0.5f);
					salonIsMoved=false;
					}
					if(cuisineIsMoved){
				cuisine.DOAnchorPos(new Vector2(-640,0),0.5f);
				cuisineIsMoved=false;
				}
				
				


	}
	public void GoTOSigngin()
	{ Invoke("dosomthing",2);
		signin.DOAnchorPos(new Vector2(0,0),0.5f);
		
	}
	public void GotoLogin()

	{
		signin.DOAnchorPos(new Vector2(0,-1250),0.5f);
		Login.DOAnchorPos(new Vector2(0,0),0.5f);


	}
	public void GOtoFeature()
	{
		Feature.DOAnchorPos(new Vector2(-130,0),0.4f);
	}
	public void gobackFromFeature()
	{
		Feature.DOAnchorPos(new Vector2(-1941,0),0.4f);
	}
	public void gotoApp()
	{ Login.DOAnchorPos(new Vector2(0,1200),0.5f);

	}
	public void favoris()
	{
		Favoris.DOAnchorPos(new Vector2(0,0), 0.3f);
	}
	public void panier()
	{
		Panier.DOAnchorPos(new Vector2(0,0), 0.3f);
	}
	public void achats()
	{
		Achats.DOAnchorPos(new Vector2(0,0), 0.3f);
	}
	public void favorisTOhome()
	{
		Favoris.DOAnchorPos(new Vector2(0,-2394.6f),0.3f);
	}
	public void panierTOhome()
	{
		Panier.DOAnchorPos(new Vector2(0,-3034.6f),0.3f);
	}
	public void achatsTOhome()
	{
		Achats.DOAnchorPos(new Vector2(0,-3674.6f),0.3f);
	}
	
}
