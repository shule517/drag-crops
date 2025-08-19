using Godot;

public partial class Player : Node2D
{
    private bool _isDragging = false;
    private Vector2 _dragStartPlayerPosition = Vector2.Zero;
    private Vector2 _dragStartMousePosition = Vector2.Zero;
    private Camera2D _camera2D;
    private Vector2 _lastMousePosition = Vector2.Zero;

    public override void _Ready()
    {
        _camera2D = GetNode<Camera2D>("Camera2D");
    }

    public override void _Process(double delta)
    {
        if (_isDragging)
        {
            // var mouseNow = GetLocalMousePosition();
            // var deltaMove = _lastMousePosition - mouseNow;
            // Position += deltaMove;
            // _lastMousePosition = mouseNow;

            Position = _dragStartPlayerPosition + (_dragStartMousePosition - GetLocalMousePosition());
        }
    }

    public override void _Input(InputEvent @event)
    {
        // キーボードで移動する
        // if (@event is InputEventKey keyEvent)
        // {
        //     if (keyEvent.Pressed && keyEvent.Keycode == Key.A)
        //     {
        //         Position -= new Vector2(10, 0);
        //     }
        //     else if (keyEvent.Pressed && keyEvent.Keycode == Key.D)
        //     {
        //         Position += new Vector2(10, 0);
        //     }
        //     else if (keyEvent.Pressed && keyEvent.Keycode == Key.W)
        //     {
        //         Position -= new Vector2(0, 10);
        //     }
        //     else if (keyEvent.Pressed && keyEvent.Keycode == Key.S)
        //     {
        //         Position += new Vector2(0, 10);
        //     }
        // }

        // スペース押している間ドラッグ
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Pressed && keyEvent.Keycode == Key.Space)
            {
                _isDragging = true;
                _dragStartMousePosition = GetLocalMousePosition();
                _dragStartPlayerPosition = Position;
                _lastMousePosition = GetLocalMousePosition();
            }
        
            if (!keyEvent.Pressed)
            {
                _isDragging = false;
                _dragStartMousePosition = Vector2.Zero;
            }
        }

        // マウスドラッグ
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                _isDragging = true;
                _dragStartMousePosition = GetLocalMousePosition();
                _dragStartPlayerPosition = Position;
                _lastMousePosition = GetLocalMousePosition();
            }
        
            if (!mouseEvent.Pressed)
            {
                _isDragging = false;
            }
            
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp && mouseEvent.Pressed)
            {
                Position += new Vector2(0, -10);
            }
            else if (mouseEvent.ButtonIndex == MouseButton.WheelDown && mouseEvent.Pressed)
            {
                Position += new Vector2(0, 10);
            }
        }
    }
}
