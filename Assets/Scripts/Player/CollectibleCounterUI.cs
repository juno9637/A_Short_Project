using TMPro;
using UnityEngine;

public class CollectibleCounterUI : MonoBehaviour
{
    [SerializeField] PlayerCollector collector;
    [SerializeField] TMP_Text label;

    void OnEnable()
    {
        collector.CountChanged += UpdateLabel;
        UpdateLabel(collector.Count);
    }

    void OnDisable()
    {
        collector.CountChanged -= UpdateLabel;
    }

    void UpdateLabel(int count) => label.text = count.ToString();
}
