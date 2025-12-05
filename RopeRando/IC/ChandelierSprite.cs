using ItemChanger;
using Newtonsoft.Json;
using UnityEngine;
using System.Reflection;
using Satchel;
using ItemChanger.Internal;

namespace RopeRando.IC
{
    internal class ChandelierSprite : ISprite
    {
        [JsonIgnore] private Sprite? _value;
        [JsonIgnore] public Sprite Value { get => _value ??= LoadSprite("chandelier.png"); }
        
        public ISprite Clone() => (ISprite)MemberwiseClone();

        private static Sprite LoadSprite(string name)
        {
            byte[] img = Assembly.GetExecutingAssembly().GetBytesFromResources(name);
            return SpriteManager.Load(img, FilterMode.Bilinear);
        }
    }
}
