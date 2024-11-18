using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>, ISpawneble
{
    [SerializeField] private Transform _spawnArea;
    [SerializeField] protected float _spawnOffsetY = 5f;
    [SerializeField] private float _spawnDelay;

    public event Action<ISpawneble> Spawned;
    public event Action<Cube> CubeReleased;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(SpawnRepeated());
    }

    public override int GetActiveCount()
    {
        return _objectPool.CountActive;
    }

    protected override Vector3 SetPosition()
    {
        return GetRandomPosition();
    }

    protected override void OnGetFromPool(Cube cube)
    {
        Spawned?.Invoke(this);

        base.OnGetFromPool(cube);
    }

    protected override void OnReleaseToPool(Cube cube)
    {
        CubeReleased?.Invoke(cube);

        base.OnReleaseToPool(cube);
    }

    private IEnumerator SpawnRepeated()
    {
        while (true)
        {
            var wait = new WaitForSeconds(_spawnDelay);

            yield return wait;

            SpawnCube();
        }
    }

    private Vector3 GetRandomPosition()
    {
        Renderer platformRenderer = _spawnArea.GetComponent<Renderer>();
        Bounds bounds = platformRenderer.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y + _spawnOffsetY;
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}