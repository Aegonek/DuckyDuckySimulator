using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public List<GameObject> spawnableObjects;
    public List<int> initialSpawnQuantities;
    public List<int> spawnChanceWeight;
    public Vector3 spawnCenter;
    public Collider spawnAreaCollider;
    public int spawnQuantity = 12;
    public int spawnQuantityDecay = 1;
    //public int maxDistance = 20;
    public float maxForceApplied = 10.0f;
    public DuckBehaviour duck;
    public float satiationDecay = 1.6f;
    public float nourishmentDecay = 0.8f;
    public float healthRegen = 0.8f;

    private void Start()
    {
        spawnCenter = spawnAreaCollider.gameObject.transform.position;
        SpawnInitialObjects();
        StartCoroutine(SpawnObjectsCoroutine(15.0f));
        StartCoroutine(HungerOverTime());
    }
    protected void SpawnInitialObjects()
    {
        for (int i = 0; i < spawnableObjects.Count; i++)
        {
            for (int j = 0; j < initialSpawnQuantities[i]; j++)
            {
                SpawnObjectInArea(spawnableObjects[i]);
            }
        }
    }

    protected void SpawnObjectInArea(Object prefab)
    {
        RaycastHit hit;
        Vector3 instantiatePoint;
        do
        {
            int xOffset = Random.Range(-250, 250);
            int zOffset = Random.Range(-250, 250);
            instantiatePoint = new Vector3(spawnCenter.x + xOffset, 200, spawnCenter.z + zOffset);
        }
        while (!Physics.Raycast(instantiatePoint, Vector3.down, out hit));

        Instantiate(prefab, hit.point, Quaternion.identity);
    }

    protected IEnumerator SpawnObjectsCoroutine(float delay)
    {
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        while (true)
        {
            if (spawnQuantity > 0)
            {
                var sum = spawnChanceWeight.Sum();
                for (int h = 0; h < spawnQuantity; h++)
                {
                    int selectionPoint = Random.Range(0, sum);
                    int currentSelectionPoint = 0;
                    for (int i = 0; i < spawnChanceWeight.Count; i++)
                    {
                        if (currentSelectionPoint < selectionPoint)
                        {
                            currentSelectionPoint += spawnChanceWeight[i];
                        }
                        else
                        {
                            SpawnObjectInArea(spawnableObjects[i]);
                        }
                    }
                    spawnQuantity = spawnQuantity - spawnQuantityDecay;
                }
            }
            else { break; }
        }
        yield return new WaitForSeconds(15.0f);
    }

    protected IEnumerator HungerOverTime() 
    {
        while (true) {
            print("Decaying.");
            duck.Satiation -= satiationDecay;
            duck.Nourishment -= nourishmentDecay;
            duck.Health += healthRegen;

            yield return new WaitForSeconds(1.0f);
        }
    }
}
