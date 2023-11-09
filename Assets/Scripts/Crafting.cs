    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Crafting : MonoBehaviour
    {
        private List<InventoryItemData> items = new List<InventoryItemData>();

        public CraftingRecipe[] recipes;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Get the selected item and add it to the furnace
                InventoryItemData selectedItem = HotbarController.Instance.GetActiveItem();
                if (selectedItem != null)
                {
                    items.Add(selectedItem);

                    // Remove the item from the hotbar
                    HotbarController.Instance.ClearSlot(HotbarController.Instance.selectedSlot);
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && items.Count >= 2)
            {
                // Try to craft the items in the furnace
                TryCraft(items[0], items[1]);
                items.Clear();
            }
        }

        public void TryCraft(InventoryItemData item1, InventoryItemData item2)
        {
            foreach (var recipe in recipes)
            {
                // Compare the ingredients in the recipe to the items in the workstation
                // Same amount of ingredients as items in the workstation

                HotbarController.Instance.AddItem(item1);

                // if (recipe.ingredients.Length != 2) continue;

                // if ((recipe.ingredients[0].id == item1.id && recipe.ingredients[1].id == item2.id) ||
                //     (recipe.ingredients[0].id == item2.id && recipe.ingredients[1].id == item1.id))
                // {
                //     // Destroy the ingredient items
                //     // Destroy(item1.gameObject);
                //     // Destroy(item2.gameObject);

                //     // Instantiate the result items
                //     foreach (var result in recipe.results)
                //     {
                //         Instantiate(result, transform.position, Quaternion.identity);
                //     }
                //     break;
                // }
            }
        }

        private InventoryItemData GetSelectedItem()
        {
            // Implement this method to return the currently selected item
            // This will depend on how you've implemented item selection in your game
            return null;
        }
    }