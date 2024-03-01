using MathNet.Numerics.LinearAlgebra;
using Splorp.Core.Primitives;
using static Splorp.Core.SplorpMath;

namespace Splorp.Core
{
    public class Transform
    {
        public Matrix<float> Matrix;

        private float a { get => Matrix[0, 0]; set => Matrix[0, 0] = value; }
        private float b { get => Matrix[0, 1]; set => Matrix[0, 1] = value; }
        private float c { get => Matrix[0, 2]; set => Matrix[0, 2] = value; }
        private float d { get => Matrix[1, 0]; set => Matrix[1, 0] = value; }
        private float e { get => Matrix[1, 1]; set => Matrix[1, 1] = value; }
        private float f { get => Matrix[1, 2]; set => Matrix[1, 2] = value; }

        public Vector2 Forward => new Vector2((float)Math.Sin(Rotation), -(float)Math.Cos(Rotation)).Normalize();
        public Vector2 Backwards => Forward * -1;
        public Vector2 Left => Forward.PerpLeft();
        public Vector2 Right => Forward.PerpRight();

        public Transform(Vector2? position = null, float scale = 1, float rotation = 0)
        {
            Matrix = IdentityMatrix;
            Position = position ?? Vector2.Zero;
            Scale = scale;
            Rotation = rotation;
        }

        public Transform(Matrix<float> matrix)
        {
            Position = matrix.GetTranslation();
            Scale = matrix.GetScale();
            Rotation = matrix.GetRotation();
        }

        private Vector2 _position;
        public Vector2 Position {
            get => _position;
            set{
                Matrix = IdentityMatrix.Translate(value).Rotate(Rotation).Scale(Scale);
                _position = value;
            }
        }

        private float _rotation;
        public float Rotation {
            get => _rotation;
            set{
                Matrix = IdentityMatrix.Translate(Position).Rotate(value.DegreesToRadians()).Scale(Scale);
                _rotation = value;
            }
        }

        private float _scale;
        public float Scale {
            get => _scale;
            set{
                Matrix = IdentityMatrix.Translate(Position).Rotate(Rotation).Scale(value);
                _scale = value;
            }
        }

        private void ToIdentity()
            => SetMatrix(1, 0, 0, 0, 1, 0);

        private void SetMatrix(float a, float b, float c, float d, float e, float f)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }

        public static Transform operator *(Transform transform1, Transform transform2)
            => new (transform1.Matrix * transform2.Matrix);
    }
}