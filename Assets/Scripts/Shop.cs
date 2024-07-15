using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Shop : MonoBehaviour
{

	[System.Serializable] public class ShopItem
	{
		public CrackedCardData Data;
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

		GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();

		for (int i = 0; i < CardNumber; i++) {
			int RandomChoice = Random.Range(0, ShopItemsList.Count);
			g = Instantiate (ItemTemplate, ShopScrollView);
			CardStore CardObject = g.transform.GetChild (0).GetComponent<CardStore>();
            CardObject.piece = ShopItemsList[RandomChoice].Data;
			Transform[] cardChildren = new Transform[g.transform.GetChild (0).childCount];
            
            for (int j = 0; j < g.transform.GetChild (0).childCount; j++)
            {
				Debug.Log(1);
                cardChildren[j] = g.transform.GetChild (0).GetChild(j);
            }
			CrackedCardData cardData = ShopItemsList[RandomChoice].Data;
			for (int j = 0; j < 4; j++)
            {
                Image fragmentImage = cardChildren[j].GetComponent<Image>();
                fragmentImage.sprite = cardData.card_pieces[j].sprite;
                
                PieceStore pieceStore = cardChildren[j].GetComponent<PieceStore>();
                if (pieceStore != null)
                {
                    pieceStore.piece = cardData.card_pieces[j];
                }
            }
			g.transform.GetChild (1).GetChild (0).GetComponent <Text> ().text = ShopItemsList [RandomChoice].Price.ToString ();
			buyBtn = g.transform.GetChild (2).GetComponent <Button> ();
			buyBtn.interactable = !ShopItemsList[RandomChoice].IsPurchased;
			buyBtn.AddEventListener (i, OnShopItemBtnClicked);
		}
		Destroy(ItemTemplate);
		SetCoinsUI();
	}

	void OnShopItemBtnClicked (int itemIndex)
	{
		if (Game.Instance.HasEnoughCoins (ShopItemsList [itemIndex].Price)) {
			GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();
			Game.Instance.UseCoins(ShopItemsList [itemIndex].Price);
			//purchase Item
			ShopItemsList [itemIndex].IsPurchased = true;

            //disable the button
            buyBtn = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
			buyBtn.interactable = false;
			buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED!";
			SetCoinsUI();
			globalManager.addCard(ShopItemsList [itemIndex].Data);
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
