using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Shop : MonoBehaviour
{

	[System.Serializable] public class ShopItem
	{
		public Sprite Image;
		public int Price;
		public bool IsPurchased = false;
	}

	public List<ShopItem> ShopItemsList;

	[SerializeField] GameObject ItemTemplate;
	[SerializeField] Animator NoCoinsAnim;
	[SerializeField] Text CoinsText;
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	Button buyBtn;

	void Start ()
	{
		int len = ShopItemsList.Count;
		for (int i = 0; i < len; i++) {
			g = Instantiate (ItemTemplate, ShopScrollView);
			g.transform.GetChild (0).GetComponent <Image> ().sprite = ShopItemsList [i].Image;
			g.transform.GetChild (1).GetChild (0).GetComponent <Text> ().text = ShopItemsList [i].Price.ToString ();
			buyBtn = g.transform.GetChild (2).GetComponent <Button> ();
			buyBtn.interactable = !ShopItemsList[i].IsPurchased;
			buyBtn.AddEventListener (i, OnShopItemBtnClicked);
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
