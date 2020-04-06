using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creation : MonoBehaviour
{
    public int numberOfTreesToPlant = 1000;
    public GameObject treePrefab;
    public int numberOfCloudsToFly = 200;
    public GameObject cloudPrefab;
    public int numberOfMale = 10;
    public int numberOfFemale = 1000;
    public GameObject personPrefab;

    // Start is called before the first frame update
    void Start()
    {
        createItemsRandomPlacement(treePrefab, transform.lossyScale.x / 2.0f, numberOfTreesToPlant);
        createItemsRandomPlacement(cloudPrefab, transform.lossyScale.x / 2.0f * 1.5f, numberOfCloudsToFly);
        createRandomFemalePlacement(personPrefab, numberOfFemale);
    }

    public void createItemsRandomPlacement(GameObject item, float radius, int count)
    {
        for (int i = 0; i < count; i++)
            PlacePrefabRandomlyOnPlanet(item, radius);        
    }
    public void createRandomFemalePlacement(GameObject item, int count)
    {
        for (int i=0; i < count; i++)
        {
            var (birthYear, endYear, isAlive) = RandomBirthDateAndDeathDatesWithAliveFlag();
            var newGameObject = PlacePrefabRandomlyWithPlanet(item, (float) birthYear/10, useNorthPole: false);
            newGameObject.transform.localScale = new Vector3(1.0f, (float)(endYear - birthYear) / 10f, 1.0f);
            newGameObject.GetComponent<PlanetBody>().birthYear = birthYear;
            newGameObject.GetComponent<PlanetBody>().endYear = endYear;
            newGameObject.GetComponent<PlanetBody>().isAlive = isAlive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject PlacePrefabRandomlyWithPlanet(GameObject prefabToPlace, float radiusOfPlanet, bool useNorthPole = false, bool makePlanetMyParent = false) =>
        PlacePrefabRandomlyOnPlanet(prefabToPlace, radiusOfPlanet, useNorthPole, makePlanetMyParent);
    public GameObject PlacePrefabRandomlyOnPlanet(GameObject prefabToPlace, float radiusOfPlanet, bool useNorthPole = false, bool makePlanetMyParent = true)
    {
        Vector3 myPosition = GetRandomPolarPosition(radiusOfPlanet, useNorthPole);
        Vector3 targetDirection = (myPosition - transform.position).normalized;

        var gameObjectInstance = Instantiate<GameObject>(prefabToPlace, myPosition, new Quaternion());
        if (makePlanetMyParent)
            gameObjectInstance.transform.parent = gameObject.transform;

        Vector3 bodyUp = gameObjectInstance.transform.up;

        gameObjectInstance.transform.rotation = Quaternion.FromToRotation(bodyUp, targetDirection) * gameObjectInstance.transform.rotation;

        return gameObjectInstance;
    }
    public (int,int, bool) RandomBirthDateAndDeathDatesWithAliveFlag()
    {
        bool alive = false;
        int currentYear = System.DateTime.Now.Year;
        int ageAtDeath = 100;  Random.Range(0, 100);
        int birthYear = Random.Range(1650, currentYear);
        int deathYear = birthYear + ageAtDeath;
        if (deathYear > currentYear)
        {
            alive = true;
            deathYear = currentYear;
        }
        return (birthYear, deathYear, alive);        
    }
    public Vector3 GetRandomPolarPosition(float polarRadius, bool useNorthPole)
    {
        if (useNorthPole)
            return PolarToVector3(polarRadius, Mathf.PI/2.0f, Mathf.PI / 2.0f);
        return PolarToVector3(polarRadius, Random.Range(-Mathf.PI, Mathf.PI), Random.Range(-Mathf.PI, Mathf.PI));
    }

    public Vector3 PolarToVector3(float radius, float polar, float elevation)
    {
        float a = radius * Mathf.Cos(elevation);
        return new Vector3(
            a * Mathf.Cos(polar),
            radius * Mathf.Sin(elevation),
            a * Mathf.Sin(polar)
        );        
    }
}
