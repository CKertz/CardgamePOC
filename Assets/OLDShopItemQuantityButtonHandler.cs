using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class OLDShopItemQuantityButtonHandler : MonoBehaviour
{
    public GameObject billItemPrefab; // Reference to the prefab to instantiate
    //public GameObject billPanel;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>(); // Get the Button component attached to this object
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        // Instantiate the prefab, set parent so that it isn't floating in ether
        PopulateBillPanel billItemParent = FindObjectOfType<PopulateBillPanel>();
        if (billItemParent != null)
        {
            GameObject billItem = Instantiate(billItemPrefab);
            // Access the parent script's properties or methods
            Debug.Log("Parent script component found: " + billItemParent.gameObject.name);


            billItem.transform.SetParent(billItemParent.gameObject.transform);
        }
        else
        {
            Debug.Log("Parent script component not found.");
        }

        // Access the script on the new bill item to pass the data
        SetupBillItemData billItemScript = billItemPrefab.GetComponent<SetupBillItemData>();

        // extract data from the shop. we get the grandparent because the plus button is in a container,
        // and the container's parent has the card data
        GameObject grandParentObject = transform.parent.parent.gameObject;
        string cardName = grandParentObject.GetComponent<SetupShopItemData>().childCardName.text;

        if (billItemScript != null)
        {
            //billItemScript.childCardName.text = cardName;
            //billItemScript.EditBillItemText(cardName);
        }
    }
}
