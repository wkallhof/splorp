namespace Splorp.Core.Physics.Bodies
{
    public class CircleBody : RigidBody
    {
        public float Radius { get; set; }

        public CircleBody(GameObject gameObject, float mass, float radius) : base(gameObject, mass)
        {
            Radius = radius;
        }
    }
}