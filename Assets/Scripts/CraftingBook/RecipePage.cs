using UnityEngine;
using UnityEngine.UI;




public class RecipePage : MonoBehaviour
{
    public Transform ingredientContainer;
    public Transform resultContainer;
    public GameObject imagePrefab; // Assign your Image UI prefab here
    public GameObject CraftMarker;
    public GameObject howto;
    public Text pagenumber;
    public void SetRecipe(CraftingRecipe recipe)
    {

        if (recipe != null)
        {
            UpdateRecipeImages(recipe.ingredients, ingredientContainer);
            UpdateRecipeImages(recipe.results, resultContainer);
            pagenumber.text = recipe.name.ToString(); // Add this line to display the recipe number as the page number
            howto.SetActive(false);
        }
        

        if (recipe.ingredients == null || recipe.ingredients.Length == 0)
        {
            Debug.LogWarning("Recipe is null.");
            CraftMarker.SetActive(false);
            howto.SetActive(true);
        }
    }


    void UpdateRecipeImages(InventoryItemData[] items, Transform container)
    {

        if (items != null && container != null)
        {
            // Clear existing children
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }

            // Instantiate and set up images with a GridLayoutGroup
            GridLayoutGroup gridLayout = container.gameObject.AddComponent<GridLayoutGroup>();
            gridLayout.cellSize = new Vector2(75f, 75f); // Set the size of each cell
            gridLayout.spacing = new Vector2(175f, 175f); // Set the spacing between cells
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount; // Set the layout constraint to a fixed number of columns
            gridLayout.constraintCount = 3; // Set the number of columns to 4
            gridLayout.childAlignment = TextAnchor.MiddleCenter; // Set the alignment of the children to the center of the cell

            // Instantiate and set up images with a HorizontalLayoutGroup
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    GameObject imageObject = Instantiate(imagePrefab, container);
                    Image image = imageObject.GetComponent<Image>();
                    image.sprite = items[i].icon;

                }
            }
        }
    }
}
