using UnityEngine;

namespace _Project.Scripts.Vfx
{
    [RequireComponent(typeof(Animator))]
    public class VfxPlayable : MonoBehaviour
    {
        private VfxPool _pool;

        public void Initialize(VfxPool pool)
            => _pool = pool;

        /// <summary>
        /// For Animation Event
        /// </summary>
        public void OnAnimationEnded()
        {
            gameObject.SetActive(false);
            _pool?.Release(this);
        }
    }
}