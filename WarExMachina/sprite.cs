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
    class sprite
    {
        protected Vector2 _location;
        protected Color _color;
        protected Texture2D _image;
        protected Rectangle _hitbox;
        protected SpriteEffects _effects;



        public Vector2 Location
        {
            get
            {
                
                return _location;
                
            }
            set
            {
                _hitbox.X = (int)_location.X;
                _hitbox.Y = (int)_location.Y;
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
        public Rectangle Hitbox
        {
            get
            {
                return _hitbox;
            }
            set
            {
                _hitbox = value;
            }
        }
        public Texture2D Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public sprite(Texture2D image, Vector2 position, Color color)
        {
            _image = image;
            _location = position;
            _color = color;
            _hitbox = new Rectangle((int)_location.X, (int)_location.Y, _image.Width, _image.Height);
        }
        public virtual void draw(SpriteBatch spbt)
        {
            spbt.Draw(_image, _location, null, _color, 0f, Vector2.Zero, 1f, _effects, 1f);
        }
        public void debugDraw(SpriteBatch batch, Texture2D pixel)
        {
            batch.Draw(pixel, _hitbox, Color.Red);
        }
    }
}
