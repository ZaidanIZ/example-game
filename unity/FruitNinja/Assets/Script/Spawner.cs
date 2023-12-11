using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minforce = 18f;
    public float maxforce = 22f;

    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator spawn()
    {
        yield return new WaitForSeconds(2f);
        while (enabled)
        {
            GameObject fruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
        
    }
}
