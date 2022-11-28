namespace Splorp.Core.Input;

public class KeyboardState{
    public Key Left {get; set;} = new();
    public Key Right {get; set;} = new();
    public Key Up {get; set;} = new();
    public Key Down {get; set;} = new();
    public Key Spacebar {get; set;} = new();
    public Key A {get; set;} = new();
    public Key W {get; set;} = new();
    public Key S {get; set;} = new();
    public Key D {get; set;} = new();
    public Key R {get; set;} = new();

    private List<Key> _keys = new();

    public KeyboardState(){
        _keys = new List<Key>{Left, Right, Up, Down, Spacebar, R, A, W, S, D, R};
    }

    public class Key{
        public bool Down {get; set;}
        public bool Up => !Down;
        public bool Pressed {get; set;}

        public bool StateChecked {get; set;}
    }

    public void Update()
    {
        _keys.ForEach(x => {
            if(x.StateChecked && x.Pressed && x.Up)
                x.Pressed = false;
            
            x.StateChecked = true;
        });
    }
}