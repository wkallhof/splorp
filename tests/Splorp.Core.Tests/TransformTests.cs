namespace Splorp.Core.Tests;
using Bogus;
using Shouldly;
using Splorp.Core.Primitives;
using static Splorp.Core.SplorpMath;
using Matrix4 = System.Numerics.Matrix4x4;

public class TransformTests
{
    private readonly Faker _faker;

    public TransformTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public void DegreesToRadiansIsCorrect()
    {
        float degrees = 180;
        var expectedRadians = (float)Math.PI;

        degrees.DegreesToRadians().ShouldBe(expectedRadians);
    }

    [Fact]
    public void RadiansToDegreesIsCorrect()
    {
        float radians = (float)Math.PI;
        float expectedDegrees = 180;

        radians.RadiansToDegrees().ShouldBe(expectedDegrees);
    }

    [Fact]
    public void InitialRotationSetCorrectly()
    {

        var matrix = Matrix4.Identity * Matrix4.CreateRotationX(30f.DegreesToRadians()) ;
        Matrix4.Decompose(matrix, out var scale, out var rotation, out var translation);

        rotation.X.ShouldBe(30f.DegreesToRadians());

        // var rotation = 90f.DegreesToRadians();
        // var transform = new Transform(Vector2.Zero, rotation: rotation);
        // transform.Rotation.ShouldBe(rotation, 0.001);
    }

    [Fact]
    public void InitialPositionSetCorrectly()
    {
        var position = new Vector2(_faker.Random.Float(-1000, 1000), _faker.Random.Float(-1000, 1000));

        var transform = new Transform(position);

        transform.Position.X.ShouldBe(position.X);
        transform.Position.Y.ShouldBe(position.Y);
    }

    [Fact]
    public void InitialScaleSetCorrectly()
    {
        var scale = _faker.Random.Float(0, 100);

        var transform = new Transform(Vector2.Zero, scale: scale);
        transform.Scale.ShouldBe(scale, 0.001);
    }

    [Fact]
    public void ComplexInitializationResultsInValidProperties()
    {
        var position = new Vector2(_faker.Random.Float(-1000, 1000), _faker.Random.Float(-1000, 1000));
        var scale = _faker.Random.Float(0, 100);
        var rotation = _faker.Random.Float(0, 360);

        var transform = new Transform(position, scale, rotation);

        transform.Position.X.ShouldBe(position.X);
        transform.Position.Y.ShouldBe(position.Y);

        transform.Scale.ShouldBe(scale, 0.001);

        transform.Rotation.ShouldBe(rotation, 0.001);
    }

    [Fact]
    public void RotationUpdatesPropertyCorrectly()
    {
        var transform = new Transform(Vector2.Zero, rotation: 90f);
        transform.Rotation.ShouldBe(90f, 0.001);

        transform.Rotation += 90f;
        transform.Rotation.ShouldBe(180f, 0.001);
    }

    [Fact]
    public void PositionUpdatesPropertyCorrectly()
    {
        var position = new Vector2(_faker.Random.Float(-1000, 1000), _faker.Random.Float(-1000, 1000));
        var transform = new Transform(position);

        transform.Position.ShouldBe(position);

        var offset = new Vector2(_faker.Random.Float(-1000, 1000), _faker.Random.Float(-1000, 1000));
        transform.Position += offset;

        transform.Position.ShouldBe(position + offset);
    }

}