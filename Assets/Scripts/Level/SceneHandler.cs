using UnityEngine;
using System.Collections.Generic;

public class SceneHandler : MonoBehaviour
{
 [Header("What")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform container;
 
    [Header("Where")]
    [SerializeField] Vector3 areaCenter = Vector3.zero;
    [SerializeField] Vector3 areaSize = new Vector3(50f, 20f, 50f);
    [SerializeField] bool centerOnCamera = false;
    [SerializeField] bool snapToGround = true;
    [SerializeField] LayerMask groundMask = ~0;
 
    [Header("When")]
    [SerializeField] int spawnOnStart = 10;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] int maxAlive = 50;
 
    readonly List<GameObject> spawned = new List<GameObject>();
    float timer;
 
    Vector3 Center => centerOnCamera ? transform.position : areaCenter;
 
    void Start()
    {
        for (int i = 0; i < spawnOnStart; i++) TrySpawn();
    }
 
    void Update()
    {
        if (spawnInterval <= 0f) return;
 
        timer += Time.deltaTime;
        if (timer < spawnInterval) return;
 
        timer = 0f;
        TrySpawn();
    }
 
    public bool TrySpawn()
    {
        if (prefab == null) return false;
 
        spawned.RemoveAll(g => g == null);
        if (spawned.Count >= maxAlive) return false;
 
        for (int attempt = 0; attempt < 10; attempt++)
        {
            if (!TryGetRandomPoint(out Vector3 position)) continue;
 
            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            spawned.Add(Instantiate(prefab, position, rotation, container));
            return true;
        }
        return false;
    }
 
    bool TryGetRandomPoint(out Vector3 position)
    {
        Vector3 half = areaSize * 0.5f;
        Vector3 center = Center;
 
        position = center + new Vector3(
            Random.Range(-half.x, half.x),
            Random.Range(-half.y, half.y),
            Random.Range(-half.z, half.z));
 
        if (!snapToGround) return true;
 
        Vector3 top = new Vector3(position.x, center.y + half.y, position.z);
        if (Physics.Raycast(top, Vector3.down, out RaycastHit hit, areaSize.y,
                            groundMask, QueryTriggerInteraction.Ignore))
        {
            position = hit.point;
            return true;
        }
        return false;
    }
 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0.5f, 0.6f);
        Gizmos.DrawWireCube(Center, areaSize);
    }
}
