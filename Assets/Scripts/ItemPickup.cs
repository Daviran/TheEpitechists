using UnityEngine;

public class ItemPickup : Interactable
{

	public Item item;   // Item to put in the inventory if picked up
    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.CompareTag("Player"))
        {
            //Add coin to counter
            PickUp();
            //Test: Print total number of coins
            Debug.Log("You have picked up a coin");
            //Destroy coin
            Destroy(gameObject);
        }
    }
	// Pick up the item
	void PickUp()
	{
		Debug.Log("Picking up " + item.name);
		Inventory.instance.Add(item);   // Add to inventory

		Destroy(gameObject);    // Destroy item from scene
	}

}
