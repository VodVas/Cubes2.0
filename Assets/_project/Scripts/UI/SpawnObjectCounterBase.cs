using TMPro;
using UnityEngine;

public abstract class SpawnObjectCounterBase : MonoBehaviour
{
    [SerializeField] protected MonoBehaviour _spawnerMonoBehaviour;

    protected IActiveObjectCounter _spawner;
    protected TextMeshProUGUI _text;
    protected int _totalCount;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();

        _spawner = _spawnerMonoBehaviour as IActiveObjectCounter;
    }

    private void OnEnable()
    {
        if (_spawner is ISpawneble spawnable)
        {
            spawnable.Spawned += OnObjectSpawned;
        }
    }

    private void OnDisable()
    {
        if (_spawner is ISpawneble spawnable)
        {
            spawnable.Spawned -= OnObjectSpawned;
        }
    }

    private void OnObjectSpawned(ISpawneble spawner)
    {
        _totalCount++;

        UpdateDisplay();
    }

    protected abstract void UpdateDisplay();
}