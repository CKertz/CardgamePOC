using Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CardSpawnHandler : MonoBehaviour
{
    public GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnFirstHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnFirstHand()
    {
        for(int i = 0; i < 5; i++) 
        {
            //get child object of prefab named CardName
            Transform cardNameTransform = cardPrefab.transform.Find("CardName");
            if (cardNameTransform != null)
            {
                TextMeshPro textMeshPro = cardNameTransform.GetComponent<TextMeshPro>();

                if (textMeshPro != null)
                {
                    textMeshPro.text = DataManager.Instance.deck.CardList[i].CardName;
                }
                else
                {
                    Debug.Log("Child Transform does not have a TextMeshPro component.");
                }
            }
            else
            {
                Debug.Log("Child GameObject not found.");
            }

            //get child object of prefab named CardSprite
            Transform cardSpriteTransform = cardPrefab.transform.Find("CardSprite");
            if (cardSpriteTransform != null)
            {
                SpriteRenderer spriteRenderer = cardSpriteTransform.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    Sprite sprite = Resources.Load<Sprite>(DataManager.Instance.deck.CardList[i].CardSpritePath);

                    spriteRenderer.sprite = sprite;
                }
                else
                {
                    Debug.Log("Child Transform does not have a SpriteRenderer component.");
                }
            }
            else
            {
                Debug.Log("Child GameObject not found.");
            }
            Instantiate(cardPrefab);

        }
    }

}
