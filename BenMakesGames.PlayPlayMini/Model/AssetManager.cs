using Microsoft.Xna.Framework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenMakesGames.PlayPlayMini.Model;

public class AssetManager : IEnumerable<ILoadable>
{
    private readonly Dictionary<Type, List<ILoadable>> _assets = [];

    public void Add<T>(T asset) where T : ILoadable
    {
        if (!_assets.TryAdd(typeof(T), [asset]))
        {
            _assets[typeof(T)].Add(asset);
        }
    }

    public void Clear<T>() where T : ILoadable
    {
        _assets.Remove(typeof(T));
    }

    public List<T> GetAll<T>() where T : ILoadable
    {
        return _assets[typeof(T)].Cast<T>().ToList();
    }

    public void LoadContent(ContentManager content, Action<ILoadable, IAsset> action, Action fullyLoadedCallback)
    {
        var deferred = new List<ILoadable>();

        foreach (var loadable in this)
        {
            if (loadable.PreLoaded)
            {
                loadable.Load(content, asset => action.Invoke(loadable, asset));
            }
            else
            {
                deferred.Add(loadable);
            }
        }

        Task.Run(() =>
        {
            foreach (var loadable in deferred)
            {
                loadable.Load(content, asset => action.Invoke(loadable, asset));
            }

            fullyLoadedCallback();
        });
    }

    public IEnumerator<ILoadable> GetEnumerator() => _assets.Values.SelectMany(x => x).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
