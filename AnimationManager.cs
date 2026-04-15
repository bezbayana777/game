
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace  MyGame;


internal class AnimationManager
{
    private int _numFrames;
    private int _numColumns;
    private Vector2 _size;
    
    private int _counter;
    private int _activeFrame;
    private int _interval;

    public AnimationManager(int numFrames, int numColumns, Vector2 size)
    {
        _numFrames = numFrames;
        _numColumns = numColumns;
        _size = size;

        _counter = 0;
        _activeFrame = 0;
        _interval = 15;
    }

    public void Update()
    {
        _counter++;
        if (_counter > _interval)
        {
            _counter = 0;
            NextFrame();
        }
    }

    public void NextFrame()
    {
        _activeFrame++;
        if (_activeFrame >= _numFrames)
        {
            _activeFrame = 0;
        }
    }

    public Rectangle GetFrame()
    {
        var x = _activeFrame * ((int)_size.X + 63);
        
        return new Rectangle(x, 0, (int)_size.X, (int)_size.Y);
    }
}