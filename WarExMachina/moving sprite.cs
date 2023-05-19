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
    class moving_sprite : sprite
    {
        protected Vector2 speed;
        protected bool isDead;
        protected int _health;
        protected int _attack;
        protected int _reward;
        public bool istank;
        public bool isbomber;
        public TimeSpan untidead;
        public TimeSpan elapseddead;
        protected TimeSpan _tillshoot;
        protected TimeSpan _wait;
        protected Rectangle _range;
        public bool stopped = false;
        public bool ifbomb = false;
        public bool inshootzone = false;
        public bool isshoot = false;
        public Vector2 _speed
        {
            get
            {
                if (stopped)
                {
                    return Vector2.Zero;
                }
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public bool IsHurt
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
            }
        }
        public int health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }
        public int reward
        {
            get
            {
                return _reward;
            }
            set
            {
                _reward = value;
            }
        }
        public int attack
        {
            get
            {
                return _attack;
            }
            set
            {
                _attack = value;
            }
        }
        public Rectangle range
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value;
            }
        }
        public TimeSpan tillshoot
        {
            get
            {
                return _tillshoot;
            }
            set
            {
                _tillshoot = value;
            }
        }
        public TimeSpan wait
        {
            get
            {
                return _wait;
            }
            set
            {
                _wait = value;
            }
        }
        public moving_sprite(Texture2D image, Vector2 position, Color color, Vector2 _speed, int health, int attack, int reward, TimeSpan tillshoot, bool isbomb)
            : base(image, position, color)
        {
            speed = _speed;
            _health = health;
            _attack = attack;
            _reward = reward;
            _tillshoot = tillshoot;
            isbomber = isbomb;
            
        }
        public virtual void updaterange()
        {
            _range.X = (int)_location.X;
            _range.Y = (int)_location.Y;
            if (_location.X >= -_hitbox.Width)
            {
                inshootzone = true;
            }
            else
            {
                inshootzone = false;
            }
        }

        public override void draw(SpriteBatch spbt)
        {
            base.draw(spbt);
            spbt.DrawString(Game1.font, health.ToString(), _location - Vector2.UnitY * 10, Color.Black);
        }

        public virtual void DrawRange(SpriteBatch batch)
        {
            batch.Draw(Game1.pixel, range, Color.Lerp(Color.OrangeRed, Color.Transparent, .5f));
        }
        public virtual void Drawhitbox(SpriteBatch batch)
        {
            batch.Draw(Game1.pixel, _hitbox, Color.Red);
        }
    }
}
