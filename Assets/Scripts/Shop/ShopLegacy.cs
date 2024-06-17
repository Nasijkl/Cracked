﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ShopLegacy : MonoBehaviour
{

	[System.Serializable] public class ShopItem
	{
		public Sprite Image;
		public int Price;
		public bool IsPurchased = false;
	}

	public List<ShopItem> ShopItemsList;
	public int CardNumber;

	[SerializeField] GameObject ItemTemplate;
	[SerializeField] Animator NoCoinsAnim;
	[SerializeField] Text CoinsText;
	GameObject g;
	int RandomChoice;
	[SerializeField] Transform ShopScrollView;
	Button buyBtn;

	void Start ()
	{
		int len = ShopItemsList.Count;
		int[] Array = new int[len];
		int sum = 0;
		while (sum<4) {
			RandomChoice = Random.Range(0, len);
			if (Array[RandomChoice] == 0)
			{
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[RandomChoice].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[RandomChoice].Price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !ShopItemsList[RandomChoice].IsPurchased;
                buyBtn.AddEventListener(sum, OnShopItemBtnClicked);
				Array[RandomChoice]++;
				sum++;
            }
			
		}
		Destroy(ItemTemplate);
		SetCoinsUI();
	}

	void OnShopItemBtnClicked (int itemIndex)
	{
		if (Game.Instance.HasEnoughCoins (ShopItemsList [itemIndex].Price)) {
			Game.Instance.UseCoins(ShopItemsList [itemIndex].Price);
			//purchase Item
			ShopItemsList [itemIndex].IsPurchased = true;

			//disable the button
			buyBtn = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
			buyBtn.interactable = false;
			buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED!";
			SetCoinsUI();
		} else {
			NoCoinsAnim.SetTrigger ("NoCoins");
			Debug.Log ("You don't have enough coins!!");
		}

	}

	void SetCoinsUI()
	{
		CoinsText.text = Game.Instance.Coins.ToString();
	}


}
