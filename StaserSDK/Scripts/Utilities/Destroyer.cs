using UnityEngine;

namespace StaserSDK.Utilities
{
    public class Destroyer : MonoBehaviour
    {
        [SerializeField] private float _delay;
        private void OnEnable()
        {
            Destroy(gameObject, _delay);
        }
    }
}