using Assets.Models;
using Assets.Scripts.CardSceneScripts;
using Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;
    public int startingHandCount = 5;
    private float spacing = 0.5f;


    void Start()
    {
        var testingDataManager = FindObjectOfType<DataManager>(true);

        if (testingDataManager != null && testingDataManager.gameObject.name == "TestingDataManager")
        {
            // this should only be hit when we're skipping straight into cardscene for testing
            Debug.Log("datamanager found with name:" + testingDataManager.gameObject.name);
            SetupDummyGame();
        }
        for (int i = 0; i < startingHandCount; i++)
        {
            //Debug.Log(i * spacing);
            //TODO: this position formula is terrible but will get the job done for now
            Vector3 position = transform.position + Vector3.right * (i * spacing) + Vector3.down * 0.8f + Vector3.left * 1.75f; 
            spawnNextCard(position);

        }
    }

    void Update()
    {

    }

    private void spawnNextCard(Vector3 position)
    {
        //get child object of prefab named CardName
        Transform cardNameTransform = cardPrefab.transform.Find("CardName");
        if (cardNameTransform != null)
        {
            TextMeshPro textMeshPro = cardNameTransform.GetComponent<TextMeshPro>();

            if (textMeshPro != null)
            {
                textMeshPro.text = DataManager.Instance.deck.CardList.Last().CardName;
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
                Sprite sprite = Resources.Load<Sprite>(DataManager.Instance.deck.CardList.Last().CardSpritePath);

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
        GameObject instance = Instantiate(cardPrefab, position, Quaternion.identity);
        instance.transform.parent = transform;

        DataManager.Instance.deck.CardList.Remove(DataManager.Instance.deck.CardList.Last());


    }

    private void spawnFirstHand()
    {
        for(int i = 0; i < startingHandCount; i++) 
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

    //used to help testing cardscene by filling deck with arbitrary cards
    private void SetupDummyGame()
    {
        var testingDataManager = FindObjectOfType<DataManager>(true);
        
        if (testingDataManager != null)
        {
            Debug.Log("TestingDataManager found, setting to active");
            testingDataManager.gameObject.SetActive(true);
            DeckHandler deckHandler = new DeckHandler();
            for (int i = 0; i < startingHandCount; i++)
            {
                Card card = new Card();
                card.CardName = "DummyCard";
                card.CardSpritePath = deckHandler.lookupCardSpritePathByName("Bun");
                DataManager.Instance.deck.CardList.Add(card);
            }
            testingDataManager.gameObject.SetActive(false);

            //Debug.Log("Empty menu, adding MenuHandler.InitializeTodayMenuItems to DataManager");
            //MenuHandler menuHandler = new MenuHandler();
            //menuHandler.InitializeTodayMenuItems();
        }
    }

}
