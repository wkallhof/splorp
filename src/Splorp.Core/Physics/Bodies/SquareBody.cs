using Splorp.Core.Primitives;

namespace Splorp.Core.Physics.Bodies
{
    public class SquareBody : RigidBody
    {
        public List<Vector2> Vertices { get; }

        public SquareBody(GameObject gameObject, List<Vector2> vertices, float mass) : base(gameObject, mass)
        {
            Vertices = vertices;
        }
    }
}