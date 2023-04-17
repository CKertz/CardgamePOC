using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupBillItemData : MonoBehaviour
{
    //public Button ShopItemPlusButton;
    //public Button ShopItemMinusButton;

    [SerializeField] TextMeshProUGUI childCardName; // I think all card props can be serialized later, just testing with name for now
    public TextMeshProUGUI childCardQuantity;
    public Image childCardImage;
    private string spritePath = "Assets/Sprites/bun"; // Example path, assuming sprite is located in a "Sprites" folder

    ShopItemQuantityButtonController shopItemQuantityButtonController;
    public GameObject billItemPrefab; // Reference to the prefab to instantiate



    // Start is called before the first frame update
    void Start()
    {
        shopItemQuantityButtonController = GameObject.FindGameObjectWithTag("ShopPlusButton").GetComponent<ShopItemQuantityButtonController>();
        //ShopItemQuantityButtonClickHandler clickHandler = GetComponent<ShopItemQuantityButtonClickHandler>();
        //ShopItemQuantityButtonClickHandler clickHandler = FindObjectOfType<ShopItemQuantityButtonClickHandler>();
        //if (clickHandler!= null)
        //{
        //    clickHandler.OnPlusClick += OnButtonPressAddShopItemToBill;
        //    Debug.Log("subscribe succeed");
        //}
        //else
        //{
        //    Debug.Log("subscribe failed");
        //}
        //EditBillItemText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnButtonPressAddShopItemToBill(object sender, EventArgs e)
    //{
    //    Debug.Log("subscriber listen confirmed");
    //}


    public void CreateBillItem(GameObject shopItemToConvert)
    {
        // NOTE: no idea why, but the object needs shaped before instantiating
        // otherwise the prefab is made with the previous click data
        SetupShopItemData shopItemData = shopItemToConvert.GetComponent<SetupShopItemData>();
        childCardQuantity.text = shopItemData.childCardPrice.text;
        if (childCardName != null)
        {
            Debug.Log("test worked");
            childCardName.text = shopItemData.childCardName.text;
        }
        //

        PopulateBillPanel billItemParent = FindObjectOfType<PopulateBillPanel>();
        if (billItemParent != null)
        {        
            GameObject billItem = Instantiate(billItemPrefab);
            // Access the parent script's properties or methods
            Debug.Log("Parent script component found: " + billItemParent.gameObject.name);

            billItem.transform.SetParent(billItemParent.gameObject.transform);
        }

        if (childCardName != null)
        {
            Debug.Log("test worked");
            //childCardName.SetText(shopItemData.childCardName.text); 
        }


    }
    public void EditBillItemData()
    {
        if (childCardQuantity != null)
        {
            childCardQuantity.text = "x3";
        }

        if (childCardImage != null)
        {
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            if (sprite != null)
            {
                // Sprite loaded successfully
                // Set the Image component's sprite property
                childCardImage.sprite = sprite;
            }
            else
            {
                //Debug.LogError("Failed to load sprite at path: " + "Path/To/Your/Sprite");
            }
        }
    }
}
