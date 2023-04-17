using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupShopItemData : MonoBehaviour
{
    public TextMeshProUGUI childCardName;
    public TextMeshProUGUI childCardPrice;
    //public Sprite childCardSprite;
    public Image childCardImage;
    //private string spritePath = "Assets/Sprites/bun"; // Example path, assuming sprite is located in a "Sprites" folder



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetShopItemMetadata(string cardName, float cardPrice, string spritePath)
    {
        if (childCardName != null)
        {          
            childCardName.text = cardName;
        }

        if (childCardPrice != null)
        {
            childCardPrice.text = "$"+cardPrice.ToString();
        }

        if (childCardImage != null)
        {
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            if (sprite != null)
            {
                childCardImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("Failed to load sprite at path: " + spritePath);
            }
        }
    }
}
