using System.Collections;
using UnityEngine;

public class Fossit : MonoBehaviour
{
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private Transform _particlesSpawnPosition;
    private IEnumerator _currentCourutine;
    private FluidParticle _currentParticle;

 //   public void Open()
 //   {
 //       if (_currentCourutine != null)
 //           return;

 //       StartCoroutine(SpawnFluid());
 //   }
 //   private IEnumerator SpawnFluid()
 //   {
 //       while (true)
 //       {
	//		_currentParticle = objectPooler.GetPoolObject("Fluid");
 //           _currentParticle.SetFall(_particlesSpawnPosition.position);
	//	}
	//}
}
