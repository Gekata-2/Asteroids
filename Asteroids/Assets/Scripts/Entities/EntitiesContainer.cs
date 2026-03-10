using System;
using System.Collections.Generic;
using Services;

namespace Entities
{
    class EntitiesContainer : IPausable,IDisposable
    {
        private readonly List<Entity> _entities;

        public List<Entity> Entities
            => _entities;

        public EntitiesContainer(List<Entity> entities = null)
        {
            _entities = entities;
        }


        public void AddEntity(Entity entity)
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            if (_entities.Contains(entity))
                _entities.Remove(entity);
        }

        public void AddEntities(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                AddEntity(entity);
        }

        public void RemoveEntities(List<Entity> entities)
        {
            foreach (Entity entity in entities)
                RemoveEntity(entity);
        }

        public void Clear()
            => _entities.Clear();

        public void Pause()
        {
            foreach (Entity entity in _entities)
                entity.Pause();
        }

        public void Resume()
        {
            foreach (Entity entity in _entities)
                entity.Resume();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}