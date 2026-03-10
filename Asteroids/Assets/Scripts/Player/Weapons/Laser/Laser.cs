using Entities;
using UnityEngine;

namespace Player.Weapons.Laser
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private Transform laserObject;
        [SerializeField] private LaserCollider laserCollider;

        public bool IsEnabled { get; private set; }

        private void Start()
        {
            laserCollider.TriggerEnter += LaserCollider_OnTriggerEnter;
        }

        private void OnDestroy()
        {
            laserCollider.TriggerEnter += LaserCollider_OnTriggerEnter;
        }

        private void LaserCollider_OnTriggerEnter(Collider2D other)
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