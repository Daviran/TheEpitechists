using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 12;  // Amount of item spaces

	// Our current list of items in the inventory
	public List<Item> items = new List<Item>();

	// Add a new item if enough room
	public void Add(Item item)
	{
		if (item.showInInventory)
		{
			if (items.Count >= space)
			{
				return;
			}

			items.Add(item);

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
	}

	// Remove an item
	public void Remove(Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

}
