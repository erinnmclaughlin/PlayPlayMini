using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BenMakesGames.PlayPlayMini.Model;

/// <param name="Key">Name that uniquely identifies this font</param>
/// <param name="Path">Relative path to image, excluding file extension (ex: "Fonts/Consolas")</param>
/// <param name="Width">Width of an individual character</param>
/// <param name="Height">Height of an individual character</param>
/// <param name="PreLoaded">Whether or not to load this resource BEFORE entering the first GameState</param>
public sealed record FontMeta(string Key, string Path, int Width, int Height, bool PreLoaded = false) : IAsset, ILoadable<Font>
{
    public char FirstCharacter { get; init; } = ' ';
    public int HorizontalSpacing { get; init; } = 1;
    public int VerticalSpacing { get; init; } = 1;

    public void Load(ContentManager content, Action<IAsset> loadAction)
    {
        loadAction(Load(content));
    }

    public Font Load(ContentManager content)  => new(
        Texture: content.Load<Texture2D>(Path),
        CharacterWidth: Width,
        CharacterHeight: Height,
        HorizontalSpacing: HorizontalSpacing,
        VerticalSpacing: VerticalSpacing,
        FirstCharacter: FirstCharacter
    );
}
