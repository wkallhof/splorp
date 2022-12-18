using Splorp.Core.Primitives;

namespace Splorp.Core.Physics.Bodies
{
    public abstract class RigidBody
    {
        protected float Mass { get; set; }
        public Vector2? Velocity { get; set; }
        protected GameObject _gameObject { get; set; }

        public Vector2 Position { get => _gameObject.Transform.Position; set { _gameObject.Transform.Position = value; } }

        public RigidBody(GameObject gameObject, float mass, Vector2? velocity = null)
        {
            Mass = mass;
            Velocity = velocity ?? Vector2.Zero;
            _gameObject = gameObject;
        }

        public virtual void Update()
        {
            if(Velocity is not null && Velocity != Vector2.Zero)
                Position += Velocity;
        }
    }
}