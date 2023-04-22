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
    public Dictionary<string, int> quantityCounter = new Dictionary<string, int>();



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
            childCardName.text = shopItemData.childCardName.text;
        }

        PopulateBillPanel billItemParent = FindObjectOfType<PopulateBillPanel>();
        if (billItemParent != null)
        {
            Debug.Log("NAME:"+billItemParent.name);
            if (!quantityCounter.ContainsKey(childCardName.text))
            {
                GameObject billItem = Instantiate(billItemPrefab);
                billItem.name = childCardName.text;
                Debug.Log(billItem.name);
                billItem.transform.SetParent(billItemParent.gameObject.transform);
            }
            billItemParent.incrementCount(shopItemToConvert,quantityCounter);
        }
    }

    public void decrementBillItem(GameObject cardToProcess)
    {
        //get card name from prefab
        string cardName = cardToProcess.GetComponent<SetupShopItemData>().childCardName.text;
        Debug.Log("dec for"+cardName);
        if (quantityCounter.ContainsKey(cardName))
        {
            quantityCounter[cardName] = quantityCounter[cardName] - 1;
            if (quantityCounter[cardName] == 0)
            {
                Debug.Log("removing key for" + cardName);
                quantityCounter.Remove(cardName);
                GameObject objectToDelete = GameObject.Find(cardName);
                Destroy(objectToDelete);
            }
        }
        else
        {
            Debug.Log("can not decrement count because entry does not exist");
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
