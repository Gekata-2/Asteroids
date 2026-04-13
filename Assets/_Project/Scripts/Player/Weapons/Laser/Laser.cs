using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class Laser : MonoBehaviour, IDamageVisitor
    {
        [SerializeField] private Transform _laserObject;
        [SerializeField] private LaserCollider _laserCollider;

        public bool IsEnabled { get; private set; }

        private void Start()
        {
            _laserCollider.TriggerEntered += OnTriggerEntered;
        }

        private void OnDestroy()
        {
            _laserCollider.TriggerEntered -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageVisitable visitable))
                visitable.Accept(this);
        }

        public void SetLength(float value)
        {
            Vector3 localScale = _laserObject.localScale;
            localScale = new Vector3(localScale.x, value, localScale.z);
            _laserObject.localScale = localScale;
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

        public void Visit(Player player)
        {
        }

        public void Visit(Asteroid asteroid)
            => asteroid.HandleLaser();

        public void Visit(Ufo ufo)
            => ufo.HandleLaser();
    }
}