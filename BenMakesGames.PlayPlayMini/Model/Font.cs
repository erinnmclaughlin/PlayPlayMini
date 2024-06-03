using Microsoft.Xna.Framework.Graphics;

namespace BenMakesGames.PlayPlayMini.Model;

public sealed record Font(Texture2D Texture, int CharacterWidth, int CharacterHeight, int HorizontalSpacing, int VerticalSpacing, char FirstCharacter) : IAsset
{
    public int Columns { get; } = Texture.Width / CharacterWidth;
    public int Rows { get; } = Texture.Height / CharacterHeight;
}
