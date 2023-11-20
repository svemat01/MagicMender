using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private List<InventoryItemData> items = new List<InventoryItemData>();
    public float craftingTime = 2f;
    public CraftingRecipe[] recipes;
    private Animator animator;
    private AudioSource audioSource; // Added AudioSource variable
    [CanBeNull]
    public CraftingRecipe CachedRecipe { get; private set; }
    [CanBeNull]
    public CraftingPopup craftingPopup;

    public float maxDistance = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Initialize AudioSource
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < maxDistance)
        {
            // Get the selected item and add it to the furnace
            InventoryItemData selectedItem = HotbarController.Instance.GetActiveItem();
            if (selectedItem != null && items.Count < 10)
            {
                items.Add(selectedItem);
                CachedRecipe = null; // Clear the cached recipe

                if (craftingPopup != null)
                {
                    craftingPopup.UpdateItems(items.ToArray());
                }

                // Remove the item from the hotbar
                HotbarController.Instance.ClearSlot(HotbarController.Instance.selectedSlot);

            }
            else if (selectedItem == null && items.Count > 0)
            {
                // If no item is selected, try to add the item from the furnace to the hotbar
                if (HotbarController.Instance.AddItem(items[0]))
                {
                    // Remove the item from the furnace
                    items.RemoveAt(0);
                    CachedRecipe = null; // Clear the cached recipe

                    if (craftingPopup != null)
                    {
                        craftingPopup.UpdateItems(items.ToArray());
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < maxDistance)
        {
            if (HasAnimator() && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsCrafting"))
            {
                animator.SetBool("IsCrafting", true);
            }

            // Play the audio when crafting starts
            audioSource.Play();

            Invoke("crafting", craftingTime);
        }
    }

    void crafting()
    {
        var success = TryCraft();
        if (!success)
        {
            Debug.Log("No recipe found");
            if (HasAnimator() && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsCrafting"))
            {
                animator.SetBool("IsCrafting", false);
            }

            // Stop the audio when crafting finishes
            audioSource.Stop();
        }
    }

    [CanBeNull]
    public CraftingRecipe FindCraftingRecipe()
    {
        // loop thru all recipes and find one that matches the items in the furnace
        // the recipies have an unknown amount of ingredients, so we need to loop thru them as well. the order may also be different in items than ingredients so we need to check both ways
        // if a recipe is found, return it
        // if no recipe is found, return null

        if (items.Count == 0) return null;

        // Check if we have a cached recipe
        if (CachedRecipe != null)
        {
            return CachedRecipe;
        }

        foreach (var recipe in recipes)
        {
            Debug.Log("Checking recipe: " + recipe.name);
            if (recipe.ingredients.Length != items.Count) continue;

            bool recipeFound = true;
            foreach (var ingredient in recipe.ingredients)
            {
                bool ingredientFound = false;
                foreach (var item in items)
                {
                    if (ingredient.id == item.id)
                    {
                        ingredientFound = true;
                        break;
                    }
                }
                if (!ingredientFound)
                {
                    recipeFound = false;
                    break;
                }
            }
            if (recipeFound)
            {
                CachedRecipe = recipe;
                return recipe;
            }
        }

        return null;
    }

    public bool TryCraft()
    {
        var recipe = FindCraftingRecipe();

        // No recipe found, return false
        if (recipe == null) return false;

        // Crafting recipe found, remove the items from the furnace
        items.Clear();
        CachedRecipe = null; // Clear the cached recipe

        if (craftingPopup != null)
        {
            craftingPopup.UpdateItems(items.ToArray());
        }

        // Add the result items to hotbar, if full drop them on the ground
        foreach (var result in recipe.results)
        {
            if (!HotbarController.Instance.AddItem(result))
            {
                Instantiate(result, transform.position, Quaternion.identity);
            }
        }

        if (HasAnimator() && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsCrafting"))
        {
            animator.SetBool("IsCrafting", false);
            // Try to craft the items in the furnace
        }

        // Future code idea
        // RecipieControler.Instance.MarkCrafted(recipe)

        return true;
    }

    private bool HasAnimator()
    {
        // Check if an animator component is attached to the game object
        return animator != null;
    }
}
