using Splorp.Core.Primitives;

namespace Splorp.Core;

public abstract class GameObject
{
    public Guid Id { get; init; }
    public Transform Transform { get; set; }
    public GameObject? Parent { get; set; }
    public List<GameObject> Children { get; set; } = new();

    public GameObject()
    {
        Transform = new();
        Id = Guid.NewGuid();
    }

    public GameObject(Transform transform)
    {
        Transform = transform;
        Id = Guid.NewGuid();
    }

    public GameObject(Vector2 position, float scale = 1, float rotation = 0)
    {
        Transform = new Transform(position, scale, rotation);
        Id = Guid.NewGuid();
    }

    public void AddChild(GameObject child)
    {
        Children.Add(child);
        child.Parent = this;
    }

    public abstract void Render(ICanvas canvas);
}