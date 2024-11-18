public class AllTimeSpawnedObjectCounter : SpawnObjectCounterBase
{
    protected override void UpdateDisplay()
    {
        _text.text = $"{_totalCount}";
    }
}