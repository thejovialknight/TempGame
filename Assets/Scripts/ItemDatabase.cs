using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    public List<Item> items = new List<Item>();

    void Awake()
    {
        // Sets up singleton
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Item GetItemFromID(string id) {
        foreach(Item item in items) {
            if(id == item.id) {
                return item;
            }
        }
        
        return null;
    }

    public List<Item> GetItemsFromIDs(string[] ids) {
        List<Item> matches = new List<Item>();

        foreach(Item item in items) {
            foreach(string id in ids) {
                if(id == item.id) {
                    matches.Add(item);
                }
            }
        }
        
        return matches;
    }
}
