using System;

public interface ISpawneble
{
    public event Action<ISpawneble> Spawned;
}