
using System.Collections.Generic;

namespace MyGame;

public class SceneManager
{
    private Stack<IScene> _sceneStack;

    public SceneManager()
    {
        _sceneStack = new Stack<IScene>();
    }

    public void AddScene(IScene scene)
    {
        scene.Load();
        _sceneStack.Push(scene);
    }

    public void RemoveScene()
    {
        _sceneStack.Pop();
    }

    public IScene GetCurrentScene()
    {
        return _sceneStack.Peek();
    }
}
