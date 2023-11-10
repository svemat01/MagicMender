using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private List<InventoryItemData> items = new List<InventoryItemData>();

    public CraftingRecipe[] recipes;

    public float maxDistance = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < maxDistance)
        {
            // Get the selected item and add it to the furnace
            InventoryItemData selectedItem = HotbarController.Instance.GetActiveItem();
            if (selectedItem != null && items.Count < 10)
            {
                items.Add(selectedItem);

                // Remove the item from the hotbar
                HotbarController.Instance.ClearSlot(HotbarController.Instance.selectedSlot);
            } else if (selectedItem == null && items.Count > 0)
            {
                // If no item is selected, try to add the item from the furnace to the hotbar
                if (HotbarController.Instance.AddItem(items[0]))
                {
                    // Remove the item from the furnace
                    items.RemoveAt(0);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < maxDistance)
        {
            // Try to craft the items in the furnace
            var success = TryCraft();
            if (!success)
            {
                Debug.Log("No recipe found");
            }
        }
    }

    [CanBeNull]
    public CraftingRecipe FindCraftingRecipe() {
        // loop thru all recipes and find one that matches the items in the furnace
        // the recipies have an unknown amount of ingredients, so we need to loop thru them as well. the order may also be different in items than ingredients so we need to check both ways
        // if a recipe is found, return it
        // if no recipe is found, return null

        if (items.Count == 0) return null;

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
                return recipe;
            }
        }

        return null;
    }

    public bool TryCraft()
    {
        // foreach (var recipe in recipes)
        // {
        //     if (recipe.ingredients.Length != 2) continue;

        //     if ((recipe.ingredients[0].id == item1.id && recipe.ingredients[1].id == item2.id) ||
        //         (recipe.ingredients[0].id == item2.id && recipe.ingredients[1].id == item1.id))
        //     {
        //         // Remove the ingredient items
        //         items.Remove(item1);
        //         items.Remove(item2);

        //         // Instantiate the result items
        //         foreach (var result in recipe.results)
        //         {
        //             Instantiate(result, transform.position, Quaternion.identity);
        //         }
        //         break;
        //     }
        // }
        var recipe = FindCraftingRecipe();

        // No recipe found, return false
        if (recipe == null) return false;

        // Crafting recipe found, remove the items from the furnace
        items.Clear();

        // Add the result items to hotbar, if full drop them on the ground
        foreach (var result in recipe.results)
        {
            if (!HotbarController.Instance.AddItem(result))
            {
                Instantiate(result, transform.position, Quaternion.identity);
            }
        }

        // Future code idea
        // RecipieControler.Instance.MarkCrafted(recipe)

        return true;
    }
}