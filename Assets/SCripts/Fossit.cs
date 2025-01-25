using System.Collections;
using UnityEngine;

public class Fossit : MonoBehaviour
{
    [SerializeField] private FluidPooler objectPooler;
    [SerializeField] private Transform _particlesSpawnPosition;
    [SerializeField] private float _spawnDelay;
    private WaitForSeconds _spawnDelaySeconds;

    private IEnumerator _currentCourutine;
    private FluidParticle _currentParticle;
    private bool _break;

    private void Start()
    {
        _spawnDelaySeconds = new WaitForSeconds(_spawnDelay);
        Open();
    }

    public void Open()
    {
        if (_currentCourutine != null)
            return;

        StartCoroutine(SpawnFluid());
    }
    public void Close()
    {
        _break = true;
    }

    private IEnumerator SpawnFluid()
    {
        while (true)
        {
            yield return _spawnDelaySeconds;
            Debug.Log("SpawnFluid");
            _currentParticle = objectPooler.GetPoolObject("Fluid1");
            _currentParticle.SetFall(_particlesSpawnPosition.position);
            if(_break)
                break;
        }
    }
}
