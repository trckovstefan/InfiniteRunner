using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private const float SECTOR_LENGTH = 90f;

    [SerializeField] List<GameObject> sectorPrefabs;
    private float nextPositionZ;

    public void GenerateNextSector()
    {
        Instantiate(sectorPrefabs[Random.Range(0, sectorPrefabs.Count)], new Vector3(0, 0, nextPositionZ), Quaternion.identity);
        nextPositionZ += SECTOR_LENGTH;
    }
}
