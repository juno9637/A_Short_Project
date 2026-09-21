using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] string collectibleTag = "Collectible";

    public int Count { get; private set; }
    public event Action<int> CountChanged;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(collectibleTag)) return;

        other.enabled = false;
        Destroy(other.gameObject);

        Count++;
        CountChanged?.Invoke(Count);
    }
}
