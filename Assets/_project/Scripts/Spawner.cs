using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour, IActiveObjectCounter where T : MonoBehaviour, IDeathEvent
{
    [SerializeField] private T _objectPrefab;

    protected ObjectPool<T> _objectPool;

    protected virtual void Start()
    {
        _objectPool = new ObjectPool<T>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyPoolObject, true, 10, 15);
    }

    private void OnDisable()
    {
        _objectPool.Clear();
    }

    protected abstract Vector3 SetPosition();

    private T CreateObject()
    {
        float maxAngleRotate = 360f;

        float angleX = Random.Range(0f, maxAngleRotate);
        float angleY = Random.Range(0f, maxAngleRotate);
        float angleZ = Random.Range(0f, maxAngleRotate);

        Quaternion randomRotation = Quaternion.Euler(angleX, angleY, angleZ);

        T obj = Instantiate(_objectPrefab, SetPosition(), randomRotation);

        return obj;
    }

    public virtual int GetActiveCount()
    {
        return _objectPool.CountActive;
    }

    private void HandleObjectDeath(IDeathEvent deadObject)
    {
        _objectPool.Release((T)deadObject);
    }

    private void OnDestroyPoolObject(T obj)
    {
        if (obj != null)
        {
            Destroy(obj.gameObject);
        }
    }

    protected virtual void OnGetFromPool(T obj)
    {
        obj.Dead += HandleObjectDeath;
        obj.gameObject.SetActive(true);
    }

    protected virtual void OnReleaseToPool(T obj)
    {
        obj.Dead -= HandleObjectDeath;
        obj.gameObject.SetActive(false);
    }

    protected virtual void SpawnCube()
    {
        T obj = _objectPool.Get();
        obj.transform.position = SetPosition();
    }
}