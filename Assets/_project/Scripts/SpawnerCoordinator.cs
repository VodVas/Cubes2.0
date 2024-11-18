using UnityEngine;

public class SpawnerCoordinator : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeReleased += OnCubeReleased;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeReleased -= OnCubeReleased;
    }

    private void OnCubeReleased(Cube cube)
    {
        _bombSpawner.SpawnBomb(cube.transform.position);
    }
}