using Microsoft.Xna.Framework.Content;
using System;

namespace BenMakesGames.PlayPlayMini.Model;

public interface ILoadable
{
    string Path { get; }
    bool PreLoaded { get; }
    void Load(ContentManager content, Action<IAsset> loadAction);
}

public interface ILoadable<T> where T : IAsset
{
    T Load(ContentManager content);
}
