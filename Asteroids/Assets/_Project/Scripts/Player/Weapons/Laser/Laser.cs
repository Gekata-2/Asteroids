using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private Transform laserObject;
        [SerializeField] private LaserCollider laserCollider;

        public bool IsEnabled { get; private set; }

        private void Start()
        {
            laserCollider.TriggerEntered += OnTriggerEntered;
        }

        private void OnDestroy()
        {
            laserCollider.TriggerEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageble damageble))
                damageble.TakeDamage(new Damage(this));
        }

        public void SetLength(float value)
        {
            Vector3 localScale = laserObject.localScale;
            localScale = new Vector3(localScale.x, value, localScale.z);
            laserObject.localScale = localScale;
        }
        
        public void Enable()
        {
            gameObject.SetActive(true);
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
            gameObject.SetActive(false);
        }
    }
}