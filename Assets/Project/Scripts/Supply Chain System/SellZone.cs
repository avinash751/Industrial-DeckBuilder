using UnityEngine;

public class SellZone : MonoBehaviour
{
    string audioKey = "Selling";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get the ISellable interface from the colliding object (or its parent)
        ISellable sellableObject = collision.GetComponent<ISellable>();
        if (sellableObject == null)
        {
            sellableObject = collision.GetComponentInParent<ISellable>(); // Check parent as well, in case ISellable is on a parent GameObject
        }

        // If the object implements ISellable, call SellObject()
        if (sellableObject != null)
        {
            Debug.Log($"Sellable object detected in SellZone: {collision.gameObject.name}");
            if(sellableObject.SellObject())
            {
                AudioManager.Instance?.PlayAudio(audioKey);
            }
           
        }
    }
}