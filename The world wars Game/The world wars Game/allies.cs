using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace The_world_wars_Game
{
    class allies : moving_sprite
    {
        protected bool _flip = false;
        public bool willflip = false;
        public bool makebabies;
        public bool ifmoto;
        public bool ifmech;
        public bool istank;
        public bool makemotos;
        public bool flip
        {
            get
            {
                return _flip;
            }
            set
            {
                _flip = value;
            }
        }
        public override void updaterange()
        {
            if (_flip == true)
            {
                _effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                _effects = SpriteEffects.None;
            }
            if (_location.X <= Game1.graphics.GraphicsDevice.Viewport.Width)
            {
                inshootzone = true;
            }
            else
            {
                inshootzone = false;
            }
            _range.X = (int)Location.X - range.Width;
            if (ifmoto == false)
            {
                _range.Y = (int)Location.Y;
            }
            else
            {
                _range.Y = (int)Location.Y - 60;
            }
            if (ifmech == false)
            {
                _range.Y = (int)Location.Y;
            }
            else
            {
                _range.Y = (int)Location.Y + 130;
            }
        }
        public allies(Texture2D image, Vector2 position, Color color, Vector2 _speed, int health, int attack, int reward, TimeSpan tillshoot, bool isbomb)
            : base(image, position, color, _speed, health, attack, reward, tillshoot, isbomb)
        {

        }
    }
}
