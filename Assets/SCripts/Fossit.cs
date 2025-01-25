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

    public bool IsOpen {  get; private set; }

    private void Start()
    {
        _spawnDelaySeconds = new WaitForSeconds(_spawnDelay);
        IsOpen = false;
    }

    public void Open()
    {
        if (_currentCourutine != null)
            return;

        _break = false;
        StartCoroutine(SpawnFluid());
        IsOpen = true;
        Debug.Log("OpEN");
    }
    public void Close()
    {
        _break = true;
        StopAllCoroutines();
        _currentCourutine = null;
        IsOpen = false;
        Debug.Log("CLOSE");
    }

    private IEnumerator SpawnFluid()
    {
        while (true)
        {
            yield return _spawnDelaySeconds;
            _currentParticle = objectPooler.GetPoolObject("Fluid1");
            _currentParticle.SetFall(_particlesSpawnPosition.position);
            if(_break)
                break;
        }
    }
}
