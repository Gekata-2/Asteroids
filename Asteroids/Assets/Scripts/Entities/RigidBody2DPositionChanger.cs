using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Entities
{
    class RigidBody2DPositionChanger
    {
        private readonly Queue<Action> _actions;

        public RigidBody2DPositionChanger(Vector3 position, Rigidbody2D rb)
        {
            _actions = new Queue<Action>();
            _actions.Enqueue(() => rb.interpolation = RigidbodyInterpolation2D.None);
            _actions.Enqueue(() => rb.position = position);
            _actions.Enqueue(() => rb.interpolation = RigidbodyInterpolation2D.Interpolate);
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