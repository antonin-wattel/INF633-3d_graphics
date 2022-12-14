using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GeneticAlgo : MonoBehaviour
{

    [Header("Genetic Algorithm parameters")]
    public int popSize = 100;
    public GameObject animalPrefab;

    [Header("Dynamic elements")]
    public float vegetationGrowthRate = 1.0f;
    public float currentGrowth;

    private List<GameObject> animals;
    protected Terrain terrain;
    protected CustomTerrain customTerrain;
    protected float width;
    protected float height;

    void Start()
    {
        // Retrieve terrain.
        terrain = Terrain.activeTerrain;
        customTerrain = GetComponent<CustomTerrain>();
        width = terrain.terrainData.size.x;
        height = terrain.terrainData.size.z;

        // Initialize terrain growth.
        currentGrowth = 0.0f;

        // Initialize animals array.
        animals = new List<GameObject>();
        for (int i = 0; i < popSize; i++)
        {
            GameObject animal = makeAnimal();
            animals.Add(animal);
        }
    }

    void Update()
    {
        // Keeps animal to a minimum.
        while (animals.Count < popSize / 2)
        {
            animals.Add(makeAnimal());
        }
        customTerrain.debug.text = "N� animals: " + animals.Count.ToString();

        // Update grass elements/food resources.
        updateResources();
    }

    /// <summary>
    /// Method to place grass or other resource in the terrain.
    /// </summary>
    public void updateResources()
    {
        Vector2 detail_sz = customTerrain.detailSize();
        int[,] details = customTerrain.getDetails();
        currentGrowth += vegetationGrowthRate;
        while (currentGrowth > 1.0f)
        {
            int x = (int)(UnityEngine.Random.value * detail_sz.x);
            int y = (int)(UnityEngine.Random.value * detail_sz.y);

            // Converting to world coordinates
            float xx = (x * width) / detail_sz.x;
            float yy = (y * height) / detail_sz.y;

            if((customTerrain.get(xx, yy) < 30f) && (customTerrain.getSpecificTexture(xx, yy) != 2)) {
                details[y, x] = 1;
                currentGrowth -= 1.0f;

            }
        }
        customTerrain.saveDetails();

    }

    /// <summary>
    /// Method to instantiate an animal prefab. It must contain the animal.cs class attached.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject makeAnimal(Vector3 position)
    {
        GameObject animal = Instantiate(animalPrefab, transform);
        animal.GetComponent<Animal>().Setup(customTerrain, this); // there might be an issue with this ...
        animal.transform.position = position;
        animal.transform.Rotate(0.0f, UnityEngine.Random.value * 360.0f, 0.0f);

        // might want to have a separate genetic algo script for each animal
        // if the animal is goal directed, let us setup the goal position
        // if there is a goal inside the scene, that is what we will use
        // if there is a goal variable as a child of the animal

        // if (animal.Find("Goal") != null)
        // try to find a goal in the children of animal 
        // if (GameObject.Find("Goal") != null)

        // look for a goal component in the children of animalprefab

        // this migh work (not testes yet)
        // Transform[] ts = animal.transform.GetComponentsInChildren();
        // foreach (Transform t in ts) if (t.gameObject.name == "Goal"){
        //     print("found goal in children !!");
        //     //set the position of the goal
        //     t.position = new Vector3(UnityEngine.Random.value * width, 0.0f, UnityEngine.Random.value * height);
        // }

        //set the goal value as the GoalCopy
        
        

        // if there is a goal component in the children of animalPrefab
        // if (animalPrefab.gameObject.GetComponentInChildren<Goal>() != null)
        // {
        //     // setup the position of the goal in front of the head
        //     print("setting up goal !!");
        //     animal.transform.Find("Goal").position = animal.transform.position + animal.transform.forward * 5.0f;
        // }

        // unpack animal prefab to avoid overriding stuff later (e.g footsteppers)
        // (doesnt work)
        // PrefabUtility.UnpackPrefabInstance(animalPrefab,PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        // PrefabUtility.DisconnectPrefabInstance(animalPrefab);

        return animal;
    }

    /// <summary>
    /// If makeAnimal() is called without position, we randomize it on the terrain.
    /// </summary>
    /// <returns></returns>
    public GameObject makeAnimal()
    {
        Vector3 scale = terrain.terrainData.heightmapScale;
        float x = UnityEngine.Random.value * width;
        float z = UnityEngine.Random.value * height;
        float y = customTerrain.getInterp(x / scale.x, z / scale.z);
        return makeAnimal(new Vector3(x, y, z));
    }

    /// <summary>
    /// Method to add an animal inherited from anothed. It spawns where the parent was.
    /// </summary>
    /// <param name="parent"></param>
    public void addOffspring(Animal parent)
    {
        GameObject animal = makeAnimal(parent.transform.position);
        animal.GetComponent<Animal>().InheritBrain(parent.GetBrain(), true);
        animals.Add(animal);
    }

    /// <summary>
    /// Remove instance of an animal.
    /// </summary>
    /// <param name="animal"></param>
    public void removeAnimal(Animal animal)
    {
        animals.Remove(animal.transform.gameObject);
        Destroy(animal.transform.gameObject);
    }

}
