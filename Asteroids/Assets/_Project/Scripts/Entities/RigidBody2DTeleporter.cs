using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Entities
{
    class RigidBody2DTeleporter
    {
        private readonly Queue<Action> _actions;

        public RigidBody2DTeleporter(Vector3 position, Rigidbody2D rb,
            RigidbodyInterpolation2D finalInterpolation = RigidbodyInterpolation2D.Interpolate)
        {
            _actions = new Queue<Action>();
            _actions.Enqueue(() => rb.interpolation = RigidbodyInterpolation2D.None);
            _actions.Enqueue(() => rb.position = position);
            _actions.Enqueue(() => rb.interpolation = finalInterpolation);
        }

        public void PerformNext()
        {
            if (_actions.IsEmpty())
                return;

            _actions.Dequeue().Invoke();
        }

        public bool IsFinished
            => _actions.IsEmpty();
    }
}