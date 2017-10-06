
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace The_world_wars_Game
{
    class label
    {
        protected Vector2 _location;
        protected Color _color;
        protected string _words;
        protected SpriteFont _font;
        public Vector2 Location
        {
            get
            {
                
                return _location;
                
            }
            set
            {
                _location = value;
            }
        }
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public string words
        {
            get
            {
                return _words;
            }
            set
            {
                _words = value;
            }
        }
        public SpriteFont font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }
        public label(Vector2 position, Color color, string words, SpriteFont font)
        {
            _location = position;
            _color = color;
            _words = words;
            _font = font;
        }
        public void draw(SpriteBatch spbt)
        {
            spbt.DrawString(_font, _words, _location, _color);
        }
    }
}