using UnityEngine;

[CreateAssetMenu(fileName = "RECIPE", menuName = "Crafting Recipe", order = 0)]
public class CraftingRecipe : ScriptableObject
{
    public string id;
    public InventoryItemData[] ingredients;
    public InventoryItemData[] results;
    public bool canBeOrdered;
}