using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorController : MonoBehaviour
{
    [Header("Templates")]
    [SerializeField] private List<TerrainTemplateController> terrainTemplates;
    [SerializeField] private float terrainTemplateWidth;

    [Header("Force Early Template")]
    [SerializeField] private List<TerrainTemplateController> earlyTerrainTemplates;

    [Header("Generator Area")]
    [SerializeField] private Camera gameCamera;
    [SerializeField] private float areaStartOffset;
    [SerializeField] private float areaEndOffset;

    private List<GameObject> spawnedTerrain;

    private float lastGeneratedPositionX;
    private float lastRemovedPositionX;

    private const float debugLineHeight = 10.0f;

    private ObjectPooling OP;

    private void Start()
    {
        OP = ObjectPooling.SharedInstance;

        spawnedTerrain = new List<GameObject>();

        lastGeneratedPositionX = GetHorizontalPositionStart();
        lastRemovedPositionX = lastGeneratedPositionX - terrainTemplateWidth;

        foreach (TerrainTemplateController terrain in earlyTerrainTemplates)
        {
            GenerateTerrain(lastGeneratedPositionX, terrain);
            lastGeneratedPositionX += terrainTemplateWidth;
        }

        while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
            GenerateTerrain(lastGeneratedPositionX);
            lastGeneratedPositionX += terrainTemplateWidth;
        }
    }

    private void Update()
    {
        while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
            GenerateTerrain(lastGeneratedPositionX);
            lastGeneratedPositionX += terrainTemplateWidth;
        }

        while (lastRemovedPositionX + terrainTemplateWidth < GetHorizontalPositionStart())
        {
            lastRemovedPositionX += terrainTemplateWidth;
            RemoveTerrain(lastRemovedPositionX);
        }
    }

    private float GetHorizontalPositionStart()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(0f, 0f)).x + areaStartOffset;
    }

    private float GetHorizontalPositionEnd()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(1f, 0f)).x + areaEndOffset;
    }

    private void GenerateTerrain(float posX, TerrainTemplateController forceTerrain = null)
    {
        GameObject newTerrain;
        if (forceTerrain == null)
        {
            newTerrain = OP.GetItemFromPool(terrainTemplates[Random.Range(0, terrainTemplates.Count)].gameObject, transform);
        }
        else
        {
            newTerrain = OP.GetItemFromPool(forceTerrain.gameObject, transform);
        }

        newTerrain.transform.position = new Vector2(posX, transform.position.y);

        spawnedTerrain.Add(newTerrain);
    }

    private void RemoveTerrain(float posX)
    {
        GameObject terrainToRemove = null;

        foreach (GameObject item in spawnedTerrain)
        {
            if (item.transform.position.x == posX)
            {
                terrainToRemove = item;
                break;
            }
        }

        if (terrainToRemove != null)
        {
            spawnedTerrain.Remove(terrainToRemove);
            OP.ReturnToPool(terrainToRemove);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 areaStartPosition = transform.position;
        Vector3 areaEndPosition = transform.position;

        areaStartPosition.x = GetHorizontalPositionStart();
        areaEndPosition.x = GetHorizontalPositionEnd();

        Debug.DrawLine(areaStartPosition + Vector3.up * debugLineHeight / 2, areaStartPosition + Vector3.down * debugLineHeight / 2, Color.red);
        Debug.DrawLine(areaEndPosition + Vector3.up * debugLineHeight / 2, areaEndPosition + Vector3.down * debugLineHeight / 2, Color.red);
    }
}
