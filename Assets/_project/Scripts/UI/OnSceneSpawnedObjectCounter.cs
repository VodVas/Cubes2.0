public class OnSceneSpawnedObjectCounter : SpawnObjectCounterBase
{
    protected override void UpdateDisplay()
    {
        int activeCount = _spawner.GetActiveCount();

        _text.text = $"{activeCount}";
    }
}