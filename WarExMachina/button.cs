using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace The_world_wars_Game
{
    class button : sprite
    {
        bool isclicked;
        bool ishovered;
        protected int _cost;
        public int cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }
        MouseState ms;
        public button(Texture2D image, Vector2 position, Color color, int cost)
            : base(image, position, color)
        {
            _cost = cost;
        }
        public void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            if (_hitbox.Contains(ms.X, ms.Y))
            {
                ishovered = true;
                _color = Color.Red;
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    _color = Color.Maroon;
                    isclicked = true;
                }
                else
                {
                    isclicked = false;
                }
                //you clicked on this
            }
            else
            {
                _color = Color.White;
                ishovered = false;
            }
            
        }
    }
}
