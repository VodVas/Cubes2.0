using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>, ISpawneble
{
    public event Action<ISpawneble> Spawned;

    protected override Vector3 SetPosition()
    {
        return Vector3.zero;
    }

    public override int GetActiveCount()
    {
        return _objectPool.CountActive;
    }

    public void SpawnBomb(Vector3 position)
    {
        Spawned?.Invoke(this);

        Bomb bomb = _objectPool.Get();
        bomb.transform.position = position;
    }
}