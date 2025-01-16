using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles interactions when the player picks up items, 
/// including health, mana, and damage effects.
/// </summary>
public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private int healthAmount; // Amount of health to restore
    [SerializeField]
    private int manaAmount; // Amount of mana to restore
    [SerializeField]
    private int damageToGive; // Amount of damage to inflict

    /// <summary>
    /// Handles interactions with the player when entering the trigger zone.
    /// </summary>
    /// <param name="collision">Collider of the object that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("HP"))
            {
                PlayerHealthManager.instance.AddPlayerHealth(healthAmount);
                AudioManager.instance.Play("BonusHP");
            }

            if (gameObject.CompareTag("MP"))
            {
                PlayerHealthManager.instance.AddPlayerMana(manaAmount);
                AudioManager.instance.Play("BonusMP");
            }

            if (gameObject.CompareTag("fullHP"))
            {
                PlayerHealthManager.instance.SetMaxHP();
                AudioManager.instance.Play("MaxHP");
            }

            if (gameObject.CompareTag("fullMana"))
            {
                PlayerHealthManager.instance.SetMaxMP();
                AudioManager.instance.Play("MaxMP");
            }

            if (gameObject.CompareTag("damageHP"))
            {
                PlayerHealthManager.instance.HurtPlayer(damageToGive);
                AudioManager.instance.Play("MinusHP");
            }

            if (gameObject.CompareTag("damageMP"))
            {
                PlayerHealthManager.instance.HurtPlayerMana(damageToGive);
                AudioManager.instance.Play("MinusMP");
            }
        }

        Destroy(gameObject); // Remove the item from the scene
    }
}
