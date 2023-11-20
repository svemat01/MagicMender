using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecipeBook : MonoBehaviour
{
    public GameObject recipePagePrefab;
    public Transform recipePagesContainer;
    public GameObject buttonContainer;
    public CraftingRecipe[] recipes;

    public int currentPageIndex = 0;

    void Awake()
    {
        recipes = Resources.LoadAll<CraftingRecipe>("Recipes");

        // Check if the number of recipes is odd and add an empty recipe if needed
        if (recipes.Length % 2 != 0)
        {
            Debug.LogWarning("Number of recipes is odd. Adding an empty recipe.");
            List<CraftingRecipe> tempList = new List<CraftingRecipe>(recipes);
            tempList.Add(new CraftingRecipe()); // Assuming CraftingRecipe has a default constructor
            recipes = tempList.ToArray();
        }
    }

    void Start()
    {
        recipePagesContainer.gameObject.SetActive(false);
        buttonContainer.SetActive(false);
        if (recipes != null && recipes.Length > 0)
        {
            CreateRecipePages();
            ShowCurrentPages();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
            ToggleRecipeBook();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftPage();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightPage();
        }

        // You can add code here to handle page navigation, e.g., using arrow keys.
    }


    public void RightPage()

    {
        if (currentPageIndex + 2 >= recipePagesContainer.childCount)
        {
            currentPageIndex += 0;
            Debug.Log("currentPageIndex = " + currentPageIndex);
        }
        else
        currentPageIndex += 2;
        ShowCurrentPages();
        Debug.Log("currentPageIndex = " + currentPageIndex);
    }

    public void LeftPage()
    {
        if (currentPageIndex - 2 < 0)
        {
            currentPageIndex +=0;
            Debug.Log("currentPageIndex = " + currentPageIndex);
        }
        else
        currentPageIndex -= 2;
        ShowCurrentPages();
        Debug.Log("currentPageIndex = " + currentPageIndex);
    }


    void CreateRecipePages()
    {
        for (int i = 0; i < recipes.Length; i += 2)
        {
            GameObject recipePage1 = Instantiate(recipePagePrefab, recipePagesContainer);
            recipePage1.SetActive(false);

            RecipePage pageScript1 = recipePage1.GetComponent<RecipePage>();
            if (pageScript1 != null)
            {
                pageScript1.SetRecipe(recipes[i]);
            }

            if (i + 1 < recipes.Length)
            {
                GameObject recipePage2 = Instantiate(recipePagePrefab, recipePagesContainer);
                recipePage2.SetActive(false);

                RecipePage pageScript2 = recipePage2.GetComponent<RecipePage>();
                if (pageScript2 != null)
                {
                    pageScript2.SetRecipe(recipes[i + 1]);
                }
                else
                {
                    Debug.LogError("pageScript2 is null");
                }

                // Move the second recipe page to the right of the first one
                RectTransform rectTransform1 = recipePage1.GetComponent<RectTransform>();
                RectTransform rectTransform2 = recipePage2.GetComponent<RectTransform>();
                rectTransform2.anchoredPosition = new Vector2(rectTransform1.anchoredPosition.x + rectTransform1.rect.width, rectTransform1.anchoredPosition.y);
            }
        }
    }

    void ShowCurrentPages()
    {
        int startIndex = Mathf.Clamp(currentPageIndex, 0, recipePagesContainer.childCount - 2);

        for (int i = 0; i < recipePagesContainer.childCount; i++)
        {
            recipePagesContainer.GetChild(i).gameObject.SetActive(i >= startIndex && i < startIndex + 2);
        }
    }

    void ToggleRecipeBook()
    {

         buttonContainer.SetActive(!buttonContainer.activeSelf);
        recipePagesContainer.gameObject.SetActive(!recipePagesContainer.gameObject.activeSelf);
    }
}
