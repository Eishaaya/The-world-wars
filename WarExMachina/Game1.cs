using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace The_world_wars_Game
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Texture2D pixel;
        public static SpriteFont font;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TimeSpan elapsedgerman;
        TimeSpan elapsedgrenade;
        TimeSpan elapsedlighttank;
        TimeSpan untilgerman = new TimeSpan(0, 0, 0, 0, 40);
        TimeSpan elapsedair;
        TimeSpan untilair = new TimeSpan(0, 0, 0, 1, 0);
        TimeSpan funding = new TimeSpan(0, 0, 0, 0, 1);
        TimeSpan waitfund;
        TimeSpan untilsuper = new TimeSpan(0, 0, 0, 40, 0);
        TimeSpan waitsuper;
        TimeSpan elapseditaly;
        TimeSpan elapsedcarmor;
        TimeSpan elapsedlong;
        TimeSpan elapsedpanzer;
        TimeSpan elapsedfokkerbi;
        button slowbutt;
        TimeSpan untilitaly = new TimeSpan(0, 0, 0, 0, 400);
        TimeSpan untilgrenade = new TimeSpan(0, 0, 0, 0, 140);
        TimeSpan untillighttank = new TimeSpan(0, 0, 0, 0, 350);
        TimeSpan untilpanzer = new TimeSpan(0, 0, 0, 0, 550);
        TimeSpan untillong = new TimeSpan(0, 0, 0, 1, 0);
        TimeSpan untilcarmor = new TimeSpan(0, 0, 0, 0, 400);
        TimeSpan untilfokkerbi = new TimeSpan(0, 0, 0, 0, 200);
        moving_sprite lighttank;
        moving_sprite german;
        moving_sprite grenade;
        moving_sprite italy;
        moving_sprite carmor;
        moving_sprite panzer;
        moving_sprite Long;
        moving_sprite cursor;
        List<allies> allies;
        button mechbutt;
        sprite lose;
        sprite brown;
        sprite clouds;
        button jeepbutton;
        button soldierbutton;
        button halobutton;
        button officerbutton;
        button medicbutton;
        button werdbut1;
        button werdbutt2;
        button werdbutt3;
        button werdbutt4;
        button werdbutt5;
        button uspbutt;
        button armorcarbutton;
        button lightbutton;
        button firstbutton;
        button wwibutton;
        button cruisebutton;
        button lancasterbutton;
        button wallbutton;
        button truckbutton3;
        button tommybutt;
        button truckbutton;
        button truckbutton2;
        button jeepbutton2;
        button cavalrybutt;
        button fairleybutt;
        button mouscitobutt;
        button helibutt;
        label score;
        int scoree = 0;
        MouseState ms;
        MouseState lastMs;
        SoundEffectInstance tankSound;
        SoundEffectInstance marchsound;
        SoundEffectInstance gunsound;
        Song music;
        SoundEffect gun;
        SoundEffect tankmove;
        SoundEffect marching;
        TimeSpan elapsedmarch;
        bool dead;
        int camplife = 2000;
        label basehealth;
        label enemybasehealth;
        List<moving_sprite> medics;
        label money;
        int Money = 200000;
        int enemycamplife = 2000;
        List<moving_sprite> enemies;
        Random rand = new Random();
        sprite win;
        bool ifwin;
        bool iflose;
        sprite blood;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1900;
            graphics.PreferredBackBufferHeight = 1060;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = Content.Load<Texture2D>("pixel");
            font = Content.Load<SpriteFont>("SpriteFont1");
            allies = new List<allies>();
            blood = new sprite(Content.Load<Texture2D>("blood"), Vector2.Zero, Color.Lerp(Color.White, Color.Transparent, .05f));
            marching = Content.Load<SoundEffect>(/*"Marching"*/"Marching_Boots_Sound_Effect");
            music = Content.Load<Song>(/*"Music"*/ "Wargame_European_Escalation_Music-_Campaign_Brief");
            gun = Content.Load<SoundEffect>("gun");
            gunsound = gun.CreateInstance();
            tankmove = Content.Load<SoundEffect>(/*"Tankmoving"*/ "Tank_moving_sound_effect_2");
            tankSound = tankmove.CreateInstance();
            marchsound = marching.CreateInstance();
            medics = new List<moving_sprite>();
            enemies = new List<moving_sprite>();
            win = new sprite(Content.Load<Texture2D>("You-Win"), new Vector2(450, 350), Color.White);
            german = new moving_sprite(Content.Load<Texture2D>(/*"german"*/ "soldier3"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(2, 0), 5, 3, 5, new TimeSpan(0, 0, 0, 0, 50), false);
            german.range = new Rectangle((int)german.Location.X - german.Image.Width, (int)german.Location.Y, 300, 30);
            italy = new moving_sprite(Content.Load<Texture2D>(/*"italy"*/ "WWII_Italian_Tank"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 114)), Color.White, new Vector2(3, 0), 150, 20, 150, new TimeSpan(0, 0, 0, 0, 350), false);
            italy.range = new Rectangle((int)italy.Location.X - italy.Image.Width, (int)italy.Location.Y, 325, 30);
            enemybasehealth = new label(new Vector2(0, 0), Color.Black, String.Format("The Enemy's Base has {0} health", enemycamplife), Content.Load<SpriteFont>("Spritefont1"));
            basehealth = new label(new Vector2(0, 25), Color.Black, String.Format("Your Base has {0} health", camplife), Content.Load<SpriteFont>("Spritefont1"));
            money = new label(new Vector2(0, 50), Color.Black, String.Format("You have ${0}", Money), Content.Load<SpriteFont>("Spritefont1"));
            score = new label(new Vector2(0, 75), Color.Black, String.Format("Your score is {0}", scoree), Content.Load<SpriteFont>("Spritefont1"));
            cursor = new moving_sprite(Content.Load<Texture2D>("cursor"), new Vector2(ms.X, ms.Y), Color.White, new Vector2(3, 0), 999999, 5, 999999, new TimeSpan(0, 0, 0, 0, 600), false);
            brown = new sprite(Content.Load<Texture2D>("ground"), new Vector2(0, 700), Color.White);
            lose = new sprite(Content.Load<Texture2D>("go"), new Vector2(0, 100), Color.White);
            jeepbutton = new button(Content.Load<Texture2D>(/*"jeepbutton"*/ "Willys_Jeep_tractor_2Pdrbutton"), new Vector2(GraphicsDevice.Viewport.Width - 144, 0), Color.White, 500);
            wallbutton = new button(Content.Load<Texture2D>("wallbutton"), new Vector2(GraphicsDevice.Viewport.Width - 544, 0), Color.White, 30000);
            jeepbutton2 = new button(Content.Load<Texture2D>("jeepbutton2"), new Vector2(GraphicsDevice.Viewport.Width - 144, 730), Color.White, 300);
            cavalrybutt = new button(Content.Load<Texture2D>("cavalrybutton"), new Vector2(GraphicsDevice.Viewport.Width - 144, 900), Color.White, 5);

            truckbutton = new button(Content.Load<Texture2D>("truckbutton"), new Vector2(GraphicsDevice.Viewport.Width - 144, 605), Color.White, 5000);
            truckbutton3 = new button(Content.Load<Texture2D>("truckbutton2"), new Vector2(GraphicsDevice.Viewport.Width - 144, 805), Color.White, 12000);
            truckbutton2 = new button(Content.Load<Texture2D>("truckbutt2"), new Vector2(GraphicsDevice.Viewport.Width - 144, 665), Color.White, 1500);
            tommybutt = new button(Content.Load<Texture2D>("Tommybutt"), new Vector2(GraphicsDevice.Viewport.Width - 144, 50), Color.White, 50);
            fairleybutt = new button(Content.Load<Texture2D>("faireybutt"), new Vector2(GraphicsDevice.Viewport.Width - 344, 50), Color.WhiteSmoke, 450);
            mouscitobutt = new button(Content.Load<Texture2D>("mosquitobutt"), new Vector2(GraphicsDevice.Viewport.Width - 344, 130), Color.WhiteSmoke, 750);
            helibutt = new button(Content.Load<Texture2D>(/*"Helibutt"*/ "1216139699276399237qubodup_Helicopter.svg.hi[1] - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 344, 270), Color.WhiteSmoke, 1000);
            mechbutt = new button(Content.Load<Texture2D>("mechbutt"), new Vector2(GraphicsDevice.Viewport.Width - 564, 250), Color.White, 10000);
            officerbutton = new button(Content.Load<Texture2D>(/*"officerbutton"*/ "ww1_1914_bef_soldier_pose_01button"), new Vector2(GraphicsDevice.Viewport.Width - 108, 50), Color.White, 10);
            halobutton = new button(Content.Load<Texture2D>("soldier armour [1] - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 144, 120), Color.White, 75);
            medicbutton = new button(Content.Load<Texture2D>(/*"halfbutton"*/ "halftrack_02 - button"), new Vector2(GraphicsDevice.Viewport.Width - 144, 360), Color.White, 75000);
            armorcarbutton = new button(Content.Load<Texture2D>(/*"armcarbutton"*/ "BR300-01 - button"), new Vector2(GraphicsDevice.Viewport.Width - 124, 280), Color.White, 500);
            wwibutton = new button(Content.Load<Texture2D>(/*"wwibutton"*/ "tank_MkIV_palestine_HD - button"), new Vector2(GraphicsDevice.Viewport.Width - 174, 531), Color.White, 475);
            firstbutton = new button(Content.Load<Texture2D>(/*"firstbutton"*/"tank_MkI_proto_HD - button"), new Vector2(GraphicsDevice.Viewport.Width - 174, 480), Color.White, 350);
            lightbutton = new button(Content.Load<Texture2D>(/*"lightbutton"*/"Fiat_3000_modelB_HD - button"), new Vector2(GraphicsDevice.Viewport.Width - 84, 120), Color.White, 200);
            cruisebutton = new button(Content.Load<Texture2D>(/*"cruisebutton"*/ "A9_Cruiser_MkI_libya41_HD - button"), new Vector2(GraphicsDevice.Viewport.Width - 154, 200), Color.White, 2500);
            lancasterbutton = new button(Content.Load<Texture2D>(/*"lanbutton"*/ "Lanchester-6x4-MkII - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 144, 410), Color.White, 750);
            soldierbutton = new button(Content.Load<Texture2D>(/*"soldierbutton"*/ "ww1_1916_somme_soldier_pose_01button"), new Vector2(GraphicsDevice.Viewport.Width - 44, 50), Color.White, 5);
            clouds = new sprite(Content.Load<Texture2D>("clouds"), new Vector2(0, 0), Color.White);
            grenade = new moving_sprite(Content.Load<Texture2D>(/*"granader"*/ "soldier4"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 89)), Color.White, new Vector2(2, 0), 7, 6, 15, new TimeSpan(0, 0, 0, 0, 300), false);
            grenade.range = new Rectangle((int)grenade.Location.X - grenade.Image.Width, (int)grenade.Location.Y, 200, 30);

            italy = new moving_sprite(Content.Load<Texture2D>(/*"italy"*/ "WWII_Italian_Tank"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 114)), Color.White, new Vector2(3, 0), 150, 20, 150, new TimeSpan(0, 0, 0, 0, 350), false);

            lighttank = new moving_sprite(Content.Load<Texture2D>(/*"lightitaly"*/"WWII_Italian_Tank"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 90)), Color.White, new Vector2(4, 0), 75, 10, 80, new TimeSpan(0, 0, 0, 0, 325), false);
            lighttank.range = new Rectangle((int)lighttank.Location.X - lighttank.Image.Width, (int)lighttank.Location.Y, 250, 30);
            Long = new moving_sprite(Content.Load<Texture2D>(/*"long"*/"c43a2cae2eacf3870b07173dd8b580b3"), new Vector2(-300, rand.Next(700, GraphicsDevice.Viewport.Height - 118)), Color.White, new Vector2(1, 0), 1000, 50, 1000, new TimeSpan(0, 0, 0, 0, 700), false);
            Long.range = new Rectangle((int)Long.Location.X - Long.Image.Width, (int)Long.Location.Y, 650, 30);
            carmor = new moving_sprite(Content.Load<Texture2D>(/*"carmor"*/"Left-bis"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 119)), Color.White, new Vector2(4, 0), 150, 25, 175, new TimeSpan(0, 0, 0, 0, 450), false);
            carmor.range = new Rectangle((int)carmor.Location.X - carmor.Image.Width, (int)carmor.Location.Y, 600, 30);
            panzer = new moving_sprite(Content.Load<Texture2D>(/*"panzer"*/"panzer_IV_AusfF2G_HD"), new Vector2(-200, rand.Next(700, GraphicsDevice.Viewport.Height - 120)), Color.White, new Vector2(2, 0), 800, 40, 800, new TimeSpan(0, 0, 0, 0, 550), false);
            panzer.range = new Rectangle((int)panzer.Location.X - panzer.Image.Width, (int)panzer.Location.Y, 550, 30);


            uspbutt = new button(Content.Load<Texture2D>(/*"uspb"*/ "fighter-plane[1] - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 344, 205), Color.WhiteSmoke, 1450);
            slowbutt = new button(Content.Load<Texture2D>(/*"slowbutt"*/ "t37 - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 504, 600), Color.White, 200);
            werdbut1 = new button(Content.Load<Texture2D>(/*"1b"*/ "t67 - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 904, 0), Color.White, 600);
            werdbutt2 = new button(Content.Load<Texture2D>(/*"2b"*/ "Lebedenko - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 904, 450), Color.White, 120);
            werdbutt3 = new button(Content.Load<Texture2D>(/*"3b"*/"205b575"), new Vector2(GraphicsDevice.Viewport.Width - 904, 200), Color.White, 175);
            werdbutt4 = new button(Content.Load<Texture2D>(/*"4b"*/"t4 - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 904, 300), Color.White, 190);
            werdbutt5 = new button(Content.Load<Texture2D>(/*"5b"*/ "chrysler_tv8_by_zaleski007-d764wv5 - Copy"), new Vector2(GraphicsDevice.Viewport.Width - 904, 700), Color.White, 3000);


            enemies.Add(Long);
            enemies.Add(panzer);
            enemies.Add(carmor);
            if (marchsound.State == SoundState.Stopped)
            {
                marchsound.IsLooped = true;
                marchsound.Volume = 0.1f;
                marchsound.Play();
            }

            if (tankSound.State == SoundState.Stopped)
            {
                tankSound.IsLooped = true;
                tankSound.Volume = 0.05f;
                tankSound.Play();
            }
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(music);
                MediaPlayer.IsRepeating = true;
            }
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();

            #region UI
            halobutton.Update(gameTime);
            uspbutt.Update(gameTime);
            helibutt.Update(gameTime);
            mouscitobutt.Update(gameTime);
            fairleybutt.Update(gameTime);
            truckbutton.Update(gameTime);
            jeepbutton.Update(gameTime);
            soldierbutton.Update(gameTime);
            officerbutton.Update(gameTime);
            slowbutt.Update(gameTime);
            mechbutt.Update(gameTime);
            lightbutton.Update(gameTime);
            cruisebutton.Update(gameTime);
            firstbutton.Update(gameTime);
            wwibutton.Update(gameTime);
            truckbutton2.Update(gameTime);
            lancasterbutton.Update(gameTime);
            cavalrybutt.Update(gameTime);
            medicbutton.Update(gameTime);
            armorcarbutton.Update(gameTime);
            wallbutton.Update(gameTime);
            jeepbutton2.Update(gameTime);
            tommybutt.Update(gameTime);
            werdbut1.Update(gameTime);
            werdbutt2.Update(gameTime);
            werdbutt3.Update(gameTime);
            werdbutt4.Update(gameTime);
            truckbutton3.Update(gameTime);
            werdbutt5.Update(gameTime);
            elapsedmarch += gameTime.ElapsedGameTime;
            cursor.Location = new Vector2(ms.X, ms.Y);
            elapsedgerman += gameTime.ElapsedGameTime;
            elapsedlighttank += gameTime.ElapsedGameTime;
            elapsedgrenade += gameTime.ElapsedGameTime;
            basehealth.words = string.Format("Your Base has {0} health", camplife);
            money.words = string.Format("You have ${0}", Money);
            score.words = string.Format("Your score is {0}", scoree);
            enemybasehealth.words = string.Format("The Enemy's Base has {0} health", enemycamplife);
            waitfund += gameTime.ElapsedGameTime;
            elapsedair += gameTime.ElapsedGameTime;
            if (waitfund >= funding)
            {
                Money += 12;
                waitfund = TimeSpan.Zero;
            }
            if (iflose == false && ifwin == false)
            {
                if (elapsedgerman >= untilgerman)
                {
                    elapsedgerman = TimeSpan.Zero;
                    moving_sprite newGerman = new moving_sprite(Content.Load<Texture2D>(/*"german"*/ "soldier3"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(2, 0), 5, 1, 5, TimeSpan.FromMilliseconds(200), false);
                    newGerman.range = new Rectangle((int)newGerman.Location.X - newGerman.Image.Width, (int)newGerman.Location.Y, 300, 30);
                    enemies.Add(newGerman);
                }
                if (elapsedair >= untilair)
                {
                    elapsedair = TimeSpan.Zero;
                    moving_sprite newGerman = new moving_sprite(Content.Load<Texture2D>(/*"air"*/ "Equalist_airship[1]"), new Vector2(-1000, rand.Next(0, 700)), Color.White, new Vector2(2, 0), 2500, 0, 2500, TimeSpan.FromMilliseconds(500), true);
                    newGerman.range = new Rectangle((int)newGerman.Location.X - newGerman.Image.Width, (int)newGerman.Location.Y, 1, 1400);
                    enemies.Add(newGerman);
                }
                if (elapsedgerman >= untilgerman)
                {
                    elapsedgerman = TimeSpan.Zero;
                    moving_sprite newGerman = new moving_sprite(Content.Load<Texture2D>(/*"german"*/ "soldier3"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(2, 0), 5, 1, 5, TimeSpan.FromMilliseconds(200), false);
                    newGerman.range = new Rectangle((int)newGerman.Location.X - newGerman.Image.Width, (int)newGerman.Location.Y, 300, 30);
                    enemies.Add(newGerman);
                }
                elapseditaly += gameTime.ElapsedGameTime;
                if (elapseditaly >= untilitaly)
                {
                    elapseditaly = TimeSpan.Zero;
                    moving_sprite newItaly = new moving_sprite(Content.Load<Texture2D>(/*"italy"*/"El problema fiscal 20140327_Page5"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 114)), Color.White, new Vector2(3, 0), 350, 25, 150, TimeSpan.FromMilliseconds(350), false);
                    newItaly.range = new Rectangle((int)newItaly.Location.X - newItaly.Image.Width, (int)newItaly.Location.Y, 325, 30);
                    newItaly.istank = true;
                    enemies.Add(newItaly);
                }
                if (untilgrenade <= elapsedgrenade)
                {
                    elapsedgrenade = TimeSpan.Zero;
                    moving_sprite newGrenade = new moving_sprite(Content.Load<Texture2D>(/*"granader"*/ "soldier4"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 89)), Color.White, new Vector2(2, 0), 7, 5, 15, TimeSpan.FromMilliseconds(300), false);
                    newGrenade.range = new Rectangle((int)newGrenade.Location.X - newGrenade.Image.Width, (int)newGrenade.Location.Y, 200, 30);
                    enemies.Add(newGrenade);
                }
                if (elapsedlighttank >= untillighttank)
                {
                    elapsedlighttank = TimeSpan.Zero;
                    moving_sprite newLighttank = new moving_sprite(Content.Load<Texture2D>(/*"lightitaly"*/"WWII_Italian_Tank"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 90)), Color.White, new Vector2(4, 0), 175, 12, 80, TimeSpan.FromMilliseconds(325), false);
                    newLighttank.range = new Rectangle((int)newLighttank.Location.X - newLighttank.Image.Width, (int)newLighttank.Location.Y, 275, 30);
                    newLighttank.istank = true;
                    enemies.Add(newLighttank);
                }
                elapsedpanzer += gameTime.ElapsedGameTime;
                elapsedcarmor += gameTime.ElapsedGameTime;
                waitsuper += gameTime.ElapsedGameTime;
                if (elapsedpanzer >= untilpanzer)
                {
                    elapsedpanzer = TimeSpan.Zero;
                    moving_sprite newPanzer = new moving_sprite(Content.Load<Texture2D>(/*"panzer"*/"panzer_IV_AusfF2G_HD"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 120)), Color.White, new Vector2(2, 0), 2000, 50, 500, TimeSpan.FromMilliseconds(500), false);
                    newPanzer.range = new Rectangle((int)newPanzer.Location.X - newPanzer.Image.Width, (int)newPanzer.Location.Y, 475, 30);
                    newPanzer.istank = true;
                    enemies.Add(newPanzer);
                }
                if (waitsuper >= untilsuper)
                {
                    waitsuper = TimeSpan.Zero;
                    moving_sprite newPanzer = new moving_sprite(Content.Load<Texture2D>(/*"super"*/ "wonder_weapon_by_soundwave3591"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 120)), Color.White, new Vector2(2.5f, 0), 100000, 400, 50000, TimeSpan.FromMilliseconds(2000), false);
                    newPanzer.range = new Rectangle((int)newPanzer.Location.X - newPanzer.Image.Width, (int)newPanzer.Location.Y, 1675, 30);
                    newPanzer.istank = true;
                    enemies.Add(newPanzer);
                }
                elapsedlong += gameTime.ElapsedGameTime;
                if (elapsedlong >= untillong)
                {
                    elapsedlong = TimeSpan.Zero;
                    moving_sprite newLong = new moving_sprite(Content.Load<Texture2D>(/*"long"*/"c43a2cae2eacf3870b07173dd8b580b3"), new Vector2(-1F, rand.Next(700, GraphicsDevice.Viewport.Height - 118)), Color.White, new Vector2(1, 0), 4000, 50, 1000, TimeSpan.FromMilliseconds(600), false);
                    newLong.range = new Rectangle((int)newLong.Location.X - newLong.Image.Width, (int)newLong.Location.Y, 525, 30);
                    newLong.istank = true;
                    enemies.Add(newLong);
                }
                elapsedfokkerbi += gameTime.ElapsedGameTime;
                if (elapsedfokkerbi >= untilfokkerbi)
                {
                    elapsedfokkerbi = TimeSpan.Zero;
                    moving_sprite newLong = new moving_sprite(Content.Load<Texture2D>("fokker[1]"), new Vector2(-1100, rand.Next(0, GraphicsDevice.Viewport.Height - 100)), Color.White, new Vector2(7, 0), 100, 1, 100, TimeSpan.FromMilliseconds(20), false);
                    newLong.range = new Rectangle((int)newLong.Location.X - newLong.Image.Width, (int)newLong.Location.Y, 325, 30);
                    newLong.istank = true;
                    enemies.Add(newLong);
                }
                if (untilcarmor <= elapsedcarmor)
                {
                    elapsedcarmor = TimeSpan.Zero;
                    moving_sprite newCarmor = new moving_sprite(Content.Load<Texture2D>(/*"carmor"*/"Left-bis"), new Vector2(-1000, rand.Next(700, GraphicsDevice.Viewport.Height - 119)), Color.White, new Vector2(3, 0), 350, 50, 175, TimeSpan.FromMilliseconds(500), false);
                    newCarmor.range = new Rectangle((int)newCarmor.Location.X - newCarmor.Image.Width, (int)newCarmor.Location.Y, 800, 30);
                    newCarmor.istank = true;
                    enemies.Add(newCarmor);
                }
                if (jeepbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= jeepbutton.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>(/*"jeep"*/ "Willys_Jeep_tractor_2Pdr"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-5, 0), 60, 25, 5, TimeSpan.FromMilliseconds(400), false);
                    Money -= jeepbutton.cost;
                    newjeep.willflip = true;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 800, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (fairleybutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= fairleybutt.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("fairey-swordfish[1]"), new Vector2(3000, rand.Next(0, GraphicsDevice.Viewport.Height - 100)), Color.White, new Vector2(-5, 0), 500, 0, 0, TimeSpan.FromMilliseconds(1000), true);
                    Money -= mouscitobutt.cost;
                    newjeep.willflip = false;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 1, 1400);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (helibutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= helibutt.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>(/*"heli"*/ "1216139699276399237qubodup_Helicopter.svg.hi[1]"), new Vector2(3000, rand.Next(0, GraphicsDevice.Viewport.Height - 100)), Color.White, new Vector2(-6, 0), 800, 10, 2, TimeSpan.FromMilliseconds(2), false);
                    Money -= helibutt.cost;
                    newjeep.willflip = false;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 30, 700, 100);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }

                if (mouscitobutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= mouscitobutt.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("de-havilland-mosquito[1]"), new Vector2(3000, rand.Next(0, GraphicsDevice.Viewport.Height - 100)), Color.White, new Vector2(-10, 0), 750, 5, 5, TimeSpan.FromMilliseconds(4), false);
                    Money -= mouscitobutt.cost;
                    newjeep.willflip = false;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 400, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }

                if (uspbutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= uspbutt.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>(/*"usp"*/ "fighter-plane[1]"), new Vector2(3000, rand.Next(0, GraphicsDevice.Viewport.Height - 100)), Color.White, new Vector2(-17.5f, 0), 1250, 150, 5, TimeSpan.FromMilliseconds(100), false);
                    Money -= uspbutt.cost;
                    newjeep.willflip = false;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 400, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }

                if (cavalrybutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= cavalrybutt.cost)
                {
                    allies newjeep = new allies(Content.Load<Texture2D>("cavalry6xz"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-5, 0), 30, 1, 5, TimeSpan.FromMilliseconds(10), false);
                    Money -= cavalrybutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 0, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (mechbutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= mechbutt.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>(/*"mech"*/ "locustcomp"), new Vector2(3000, rand.Next(300, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-7, 0), 10000, 300, 0, TimeSpan.FromMilliseconds(200), false);
                    Money -= mechbutt.cost;
                    newjeep.ifmech = true;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 250, 500, 100);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (werdbut1.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= werdbut1.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("T67"), new Vector2(3000, rand.Next(350, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2.6f, 0), 1000, 35, 0, TimeSpan.FromMilliseconds(250), false);
                    Money -= werdbut1.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 250, 350, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (werdbutt2.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= werdbutt2.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("Lebedenko"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-1.5f, 0), 500, 3, 0, TimeSpan.FromMilliseconds(1), false);
                    Money -= werdbutt2.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 250, 200, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (werdbutt3.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= werdbutt3.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("205b575"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 650, 15, 0, TimeSpan.FromMilliseconds(450), false);
                    Money -= werdbutt3.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 250, 300, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (werdbutt4.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= werdbutt4.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("t4"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-1.5f, 0), 500, 15, 0, TimeSpan.FromMilliseconds(150), false);
                    Money -= werdbutt4.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 250, 400, 60);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (werdbutt5.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= werdbutt5.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>("chrysler_tv8_by_zaleski007-d764wv5"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-4.5f, 0), 3500, 50, 0, TimeSpan.FromMilliseconds(130), false);
                    Money -= werdbutt5.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y - 250, 400, 60);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (slowbutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= slowbutt.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>(/*"slow"*/ "t37"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-1.5f, 0), 550, 47, 0, TimeSpan.FromMilliseconds(400), false);
                    Money -= slowbutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 400, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (wallbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= wallbutton.cost)
                {
                    allies newjeep = new allies(Content.Load<Texture2D>("wall"), new Vector2(500, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(0, 0), 110000, 500, 5, TimeSpan.FromMilliseconds(2000), false);
                    Money -= wallbutton.cost;
                    newjeep.makebabies = true;
                    newjeep.makemotos = true;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 800, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (jeepbutton2.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= jeepbutton2.cost)
                {

                    allies newjeep = new allies(Content.Load<Texture2D>(/*"jeep2"*/ "mil93"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-5, 0), 400, 1, 5, TimeSpan.FromMilliseconds(30), false);
                    Money -= jeepbutton2.cost;
                    newjeep.willflip = false;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 350, 30);
                    newjeep.istank = true;
                    allies.Add(newjeep);
                }
                if (tommybutt.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= tommybutt.cost)
                {
                    Money -= tommybutt.cost;
                    allies newjeep = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 5, 5, TimeSpan.FromMilliseconds(500), false);
                    Money -= tommybutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                    allies.Add(newjeep);
                    allies newsoldier = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 15);
                    allies.Add(newsoldier);
                    allies newofficer = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 15);
                    allies.Add(newofficer);
                    allies newsoldier2 = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 15);
                    allies.Add(newsoldier2);
                    allies newofficer2 = new allies(Content.Load<Texture2D>(/*"officer"*/ "ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 15);
                    allies.Add(newofficer2);
                    allies.Add(newsoldier2);
                    allies newofficer3 = new allies(Content.Load<Texture2D>(/*"halo"*/ "soldier armour [1]"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-3, 0), 45, 2, 15, new TimeSpan(200), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 15);
                    allies.Add(newofficer3);
                }
                if (halobutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= halobutton.cost)
                {
                    Money -= halobutton.cost;
                    allies newjeep = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 5, 5, TimeSpan.FromMilliseconds(500), false);
                    Money -= halobutton.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                    allies.Add(newjeep);
                    allies newsoldier = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier);
                    allies newofficer = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer);
                    allies newsoldier2 = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier2);
                    allies newofficer2 = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer2);
                }
                if (truckbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= truckbutton.cost)
                {
                    allies newtruck = new allies(Content.Load<Texture2D>("truck"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-4, 0), 100, 0, 5, TimeSpan.FromMilliseconds(1000), false);
                    Money -= truckbutton.cost;
                    newtruck.willflip = true;
                    newtruck.istank = true;
                    newtruck.makebabies = true;
                    allies newjeep = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 5, 5, TimeSpan.FromMilliseconds(500), false);
                    Money -= tommybutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                    allies.Add(newjeep);
                    newtruck.range = new Rectangle((int)newtruck.Location.X, (int)newtruck.Location.Y, 850, 30);
                    allies.Add(newtruck);
                    allies newsoldier = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier);
                    allies newofficer = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer);
                }
                if (truckbutton3.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= truckbutton3.cost)
                {
                    allies newtruck = new allies(Content.Load<Texture2D>("truck"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-4, 0), 100, 0, 5, TimeSpan.FromMilliseconds(1000), false);
                    allies newtruck3 = new allies(Content.Load<Texture2D>(/*"radarbutton"*/"mil89"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 90, 0, 5, TimeSpan.FromMilliseconds(1000), false);
                    newtruck.willflip = true;
                    newtruck3.makemotos = true;
                    newtruck.makebabies = true;
                    newtruck3.istank = true;
                    allies.Add(newtruck3);
                    allies newjeep = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 5, 5, TimeSpan.FromMilliseconds(500), false);
                    Money -= tommybutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                    allies.Add(newjeep);
                    newtruck.range = new Rectangle((int)newtruck.Location.X, (int)newtruck.Location.Y, 850, 30);
                    newtruck3.range = new Rectangle((int)newtruck.Location.X, (int)newtruck.Location.Y, 900, 30);
                    newtruck.istank = true;
                    allies.Add(newtruck);
                    allies newsoldier = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier);
                    allies newofficer = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer);
                    allies newtruck2 = new allies(Content.Load<Texture2D>("truckbutt2"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-5, 0), 120, 0, 5, TimeSpan.FromMilliseconds(3500), false);
                    Money -= truckbutton3.cost;
                    newtruck2.willflip = true;
                    newtruck2.makebabies = true;
                    newtruck2.istank = true;
                    allies.Add(newtruck2);
                    newtruck2.range = new Rectangle((int)newtruck2.Location.X, (int)newtruck2.Location.Y, 850, 30);
                    allies newjeep2 = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 5, 5, TimeSpan.FromMilliseconds(500), false);
                    Money -= tommybutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                    allies.Add(newjeep2);
                    newtruck.range = new Rectangle((int)newtruck.Location.X, (int)newtruck.Location.Y, 850, 30);
                    allies newsoldier2 = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier2);
                    allies newofficer2 = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer2);
                }
                if (truckbutton2.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= truckbutton2.cost)
                {
                    allies newtruck = new allies(Content.Load<Texture2D>("truckbutt2"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-5, 0), 120, 0, 5, TimeSpan.FromMilliseconds(3500), false);
                    Money -= truckbutton2.cost;
                    newtruck.willflip = true;
                    newtruck.istank = true;
                    newtruck.makebabies = true;
                    allies newjeep = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 5, 5, TimeSpan.FromMilliseconds(500), false);
                    Money -= tommybutt.cost;
                    newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                    allies.Add(newjeep);
                    newtruck.range = new Rectangle((int)newtruck.Location.X, (int)newtruck.Location.Y, 850, 30);
                    allies.Add(newtruck);
                    allies newsoldier = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier);
                    allies newofficer = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer);
                }
                if (soldierbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= soldierbutton.cost)
                {
                    Money -= soldierbutton.cost;
                    allies newsoldier = new allies(Content.Load<Texture2D>(/*"soldier"*/"ww1_1916_somme_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 5, 1, 5, new TimeSpan(200), false);
                    newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                    allies.Add(newsoldier);
                }
                if (officerbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= officerbutton.cost)
                {
                    Money -= officerbutton.cost;
                    allies newofficer = new allies(Content.Load<Texture2D>(/*"officer"*/"ww1_1914_bef_soldier_pose_01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 12, 2, 5, new TimeSpan(160), false);
                    newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                    allies.Add(newofficer);
                }
                if (medicbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= medicbutton.cost)
                {
                    Money -= medicbutton.cost;
                    medics.Add(new moving_sprite(Content.Load<Texture2D>(/*"medic"*/"halftrack_02"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-1, 0), 50, 10, 5, new TimeSpan(70), false));
                }
                if (armorcarbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= armorcarbutton.cost)
                {
                    Money -= armorcarbutton.cost;
                    allies newarmorcar = new allies(Content.Load<Texture2D>(/*"armcar"*/"BR300-01"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-3, 0), 250, 23, 5, new TimeSpan(500), false);
                    newarmorcar.range = new Rectangle((int)newarmorcar.Location.X, (int)newarmorcar.Location.Y, 450, 30);
                    newarmorcar.istank = true;
                    allies.Add(newarmorcar);
                }
                if (lightbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= lightbutton.cost)
                {
                    Money -= lightbutton.cost;
                    allies newlight = new allies(Content.Load<Texture2D>(/*"light"*/"Fiat_3000_modelB_HD"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-3, 0), 80, 7, 5, new TimeSpan(350), false);
                    newlight.range = new Rectangle((int)newlight.Location.X, (int)newlight.Location.Y, 250, 30);

                    allies.Add(newlight);
                }
                if (firstbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= firstbutton.cost)
                {
                    Money -= firstbutton.cost;
                    allies newfirst = new allies(Content.Load<Texture2D>(/*"first"*/"tank_MkI_proto_HD"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-1.5f, 0), 450, 10, 5, new TimeSpan(600), false);
                    newfirst.range = new Rectangle((int)newfirst.Location.X, (int)newfirst.Location.Y, 275, 40);
                    newfirst.istank = true;
                    allies.Add(newfirst);
                }
                if (wwibutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= wwibutton.cost)
                {
                    Money -= wwibutton.cost;
                    allies newwwi = new allies(Content.Load<Texture2D>(/*"wwi"*/"tank_MkIV_palestine_HD"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-1.5f, 0), 650, 15, 5, TimeSpan.FromMilliseconds(500), false);
                    newwwi.range = new Rectangle((int)newwwi.Location.X, (int)newwwi.Location.Y, 350, 40);
                    newwwi.istank = true;
                    allies.Add(newwwi);
                }
                if (cruisebutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= cruisebutton.cost)
                {
                    Money -= cruisebutton.cost;
                    allies newcruise = new allies(Content.Load<Texture2D>(/*"cruise"*/"A9_Cruiser_MkI_libya41_HD"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-2, 0), 3560, 150, 5, TimeSpan.FromMilliseconds(300), false);
                    newcruise.range = new Rectangle((int)newcruise.Location.X, (int)newcruise.Location.Y, 650, 35);
                    newcruise.istank = true;
                    allies.Add(newcruise);
                }
                if (lancasterbutton.Hitbox.Intersects(cursor.Hitbox) && ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && Money >= lancasterbutton.cost)
                {
                    Money -= lancasterbutton.cost;
                    allies newlancaster = new allies(Content.Load<Texture2D>(/*"Lancaster"*/"Lanchester-6x4-MkII"), new Vector2(3000, rand.Next(700, GraphicsDevice.Viewport.Height - 92)), Color.White, new Vector2(-3, 0), 400, 30, 5, TimeSpan.FromMilliseconds(350), false);
                    newlancaster.range = new Rectangle((int)newlancaster.Location.X, (int)newlancaster.Location.Y, 500, 60);
                    newlancaster.istank = true;
                    allies.Add(newlancaster);
                }
            }
            #endregion

            if (ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released)
            {
                gunsound.Volume = 0.1f;
                gunsound.Play();
            }
            //errors at the end make it keep moving

            #region medic update
            for (int i = 0; i < medics.Count - 1; i++)
            {
                medics[i].Location += medics[i]._speed;
                if (medics[i].Location.X <= -200)
                {
                    enemycamplife--;
                    medics.RemoveAt(i);
                }
                //medic to ally
                for (int p = 0; p < allies.Count; p++)
                {
                    if (medics.Count > 0)
                    {
                        if (allies[p].Hitbox.Intersects(medics[i].Hitbox))
                        {
                            allies[p].health += medics[i].attack;
                        }
                    }
                }

                //medic to enemy
                for (int e = 0; e < enemies.Count; e++)
                {
                    if (medics.Count > 0)
                    {
                        if (enemies[e].range.Intersects(medics[i].Hitbox))
                        {
                            medics[i].health -= enemies[e].attack;
                        }
                        if (medics[i].health <= 0)
                        {
                            medics.RemoveAt(i);
                        }
                    }
                }
            }
            #endregion

            #region enemies update
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Location += enemies[i]._speed;

                //enemy to cursor
                if (ms.LeftButton == ButtonState.Pressed && lastMs.LeftButton == ButtonState.Released && cursor.Hitbox.Intersects(enemies[i].Hitbox))
                {
                    enemies[i].health -= cursor.attack;
                }

                enemies[i].updaterange();

                if (enemies[i].inshootzone)
                {
                    //enemy to ally
                    for (int p = 0; p < allies.Count; p++)
                    {
                        if (allies[p].inshootzone)
                        {
                            ///////////////////////////////////////////////////////////////////////////////////
                            //were gonna need to make the stuff wait until the enemey they are attacking (or the one in front of them...) is dead before restarting their movement
                            if (enemies[i].range.Intersects(allies[p].Hitbox))
                            {
                                if (allies[p].health <= 0)
                                {
                                    if (allies[p].istank == true)
                                    {
                                        allies[p].Image = Content.Load<Texture2D>("Explosion_Sequence_A 12");

                                    }
                                    enemies[i].wait = TimeSpan.Zero;
                                    enemies[i].stopped = false;
                                }
                                else
                                {
                                    enemies[i].stopped = true;
                                    enemies[i].wait += gameTime.ElapsedGameTime;
                                    if (enemies[i].isbomber == true && enemies[i].wait >= enemies[i].tillshoot)
                                    {
                                        enemies[i].wait = TimeSpan.Zero;
                                        enemies[i].wait = new TimeSpan(10000);
                                        moving_sprite newPanzer = new moving_sprite(Content.Load<Texture2D>("100lb_Bomb[1]"), enemies[i].Location, Color.White, new Vector2(0, 15), 1000, 500, 1, TimeSpan.FromMilliseconds(0), false);
                                        newPanzer.range = new Rectangle((int)newPanzer.Location.X - newPanzer.Image.Width, (int)newPanzer.Location.Y, 1, 1);
                                        newPanzer.istank = true;
                                        newPanzer.ifbomb = true;
                                        enemies.Add(newPanzer);
                                    }
                                    if (enemies[i].ifbomb == true && enemies[i].range.Intersects(allies[p].Hitbox))
                                    {
                                        enemies[i].health = 0;
                                    }
                                    if (enemies[i].wait >= enemies[i].tillshoot)
                                    {
                                        allies[p].health -= enemies[i].attack;
                                        enemies[i].wait = TimeSpan.Zero;
                                        enemies[i].isshoot = true;
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                enemies[i].isshoot = false;
                                enemies[i].stopped = false;
                            }
                        }
                    }
                    if (enemies[i].Location.X >= GraphicsDevice.Viewport.Width + 200)
                    {
                        enemies.RemoveAt(i);
                        i--;
                        camplife--;
                    }
                    else if (enemies[i].health <= 0)
                    {
                        Money += enemies[i].reward;
                        enemies.RemoveAt(i);
                        i--;
                    }
                }
            }
            #endregion

            #region allies update
            for (int p = 0; p < allies.Count; p++)
            {

                allies[p].Location += allies[p]._speed;
                allies[p].updaterange();
                if (allies[p].inshootzone)
                {
                    //ally to side of screen
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].inshootzone)
                        {
                            if (allies[p].range.Intersects(enemies[i].Hitbox))
                            {
                                if (allies[p].willflip == true)
                                {
                                    allies[p].flip = true;
                                }
                                if (enemies[i].health <= 0)
                                {
                                    scoree += enemies[i].reward;
                                    if (enemies[i].istank == true)
                                    {
                                        enemies[i].Image = Content.Load<Texture2D>("Explosion_Sequence_A 12");
                                    }
                                    allies[p].stopped = false;
                                    allies[p].wait = TimeSpan.Zero;
                                }
                                else
                                {
                                    allies[p].stopped = true;
                                    //should add wait here
                                    allies[p].wait += gameTime.ElapsedGameTime;
                                    if (allies[p].wait >= allies[p].tillshoot)
                                    {
                                        enemies[i].health -= allies[p].attack;
                                        if (allies[p].isbomber == true)
                                        {
                                            allies[p].wait = TimeSpan.Zero;
                                            allies newPanzer = new allies(Content.Load<Texture2D>("100lb_Bomb[1]"), allies[p].Location, Color.White, new Vector2(0, 15), 1000, 500, 1, TimeSpan.FromMilliseconds(0), false);
                                            newPanzer.range = new Rectangle((int)newPanzer.Location.X - newPanzer.Image.Width, (int)newPanzer.Location.Y, 1, 1);
                                            newPanzer.istank = true;
                                            newPanzer.ifbomb = true;
                                            allies.Add(newPanzer);
                                        }
                                        if (allies[p].ifbomb)
                                        {
                                            enemies[i].health -= allies[p].attack;
                                            allies[p].health = 0;
                                        }
                                        if (allies[p].makebabies)
                                        {
                                            allies newsoldier = new allies(Content.Load<Texture2D>("ww1_1914_bef_soldier_pose_01button"), new Vector2(allies[p].Location.X, allies[p].Location.Y), Color.White, new Vector2(-2, 0), 3, 1, 5, new TimeSpan(200), false);
                                            newsoldier.range = new Rectangle((int)newsoldier.Location.X, (int)newsoldier.Location.Y, 300, 30);
                                            allies.Add(newsoldier);
                                            allies newofficer = new allies(Content.Load<Texture2D>("ww1_1914_bef_soldier_pose_01"), new Vector2(allies[p].Location.X, allies[p].Location.Y), Color.White, new Vector2(-2, 0), 7, 2, 5, new TimeSpan(160), false);
                                            newofficer.range = new Rectangle((int)newofficer.Location.X, (int)newofficer.Location.Y, 350, 30);
                                            allies.Add(newofficer);
                                            allies newjeep = new allies(Content.Load<Texture2D>("1Tommy150"), new Vector2(allies[p].Location.X, allies[p].Location.Y), Color.White, new Vector2(-2, 0), 4, 5, 5, TimeSpan.FromMilliseconds(500), false);
                                            newjeep.range = new Rectangle((int)newjeep.Location.X, (int)newjeep.Location.Y, 600, 30);
                                            allies.Add(newjeep);
                                            allies newjeep2 = new allies(Content.Load<Texture2D>("cavalry6xz"), new Vector2(allies[p].Location.X, allies[p].Location.Y), Color.White, new Vector2(-5, 0), 30, 1, 5, TimeSpan.FromMilliseconds(10), false);
                                            newjeep2.range = new Rectangle((int)newjeep2.Location.X, (int)newjeep2.Location.Y, 0, 30);
                                            allies.Add(newjeep2);
                                            allies newjeep3 = new allies(Content.Load<Texture2D>("crawler"), new Vector2(allies[p].Location.X, allies[p].Location.Y), Color.White, new Vector2(-1, 0), 15, 2, 5, TimeSpan.FromMilliseconds(200), false);
                                            newjeep3.range = new Rectangle((int)newjeep3.Location.X, (int)newjeep3.Location.Y, 360, 30);
                                            allies.Add(newjeep3);
                                        }
                                        if (allies[p].makemotos)
                                        {
                                            allies newmoto = new allies(Content.Load<Texture2D>("military-motorcycles-14276906"), new Vector2(allies[p].Location.X, allies[p].Location.Y), Color.White, new Vector2(-6, 0), 120, 1, 5, new TimeSpan(30), false);
                                            newmoto.range = new Rectangle((int)newmoto.Location.X, (int)newmoto.Location.Y + 50, 350, 30);
                                            newmoto.istank = true;
                                            newmoto.ifmoto = true;
                                            allies.Add(newmoto);
                                        }
                                        allies[p].wait = TimeSpan.Zero;
                                        allies[p].isshoot = true;
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                allies[p].stopped = false;
                                allies[p].isshoot = false;
                                if (allies[p].willflip == true)
                                {
                                    allies[p].flip = false;
                                }
                            }
                        }
                    }
                }
                if (allies[p].Location.X <= -200)
                {
                    allies.RemoveAt(p);
                    p--;
                    enemycamplife--;
                }
                else if (allies[p].health <= 0)
                {
                    allies.RemoveAt(p);
                    p--;
                }
            }
            #endregion


            if (camplife <= 0)
            {
                dead = true;
                iflose = true;
            }
            if (enemycamplife <= 0)
            {
                ifwin = true;
            }
            lastMs = ms;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.RoyalBlue);
            spriteBatch.Begin();
            brown.draw(spriteBatch);
            clouds.draw(spriteBatch);
            jeepbutton.draw(spriteBatch);
            cavalrybutt.draw(spriteBatch);
            soldierbutton.draw(spriteBatch);
            officerbutton.draw(spriteBatch);
            lightbutton.draw(spriteBatch);
            cruisebutton.draw(spriteBatch);
            armorcarbutton.draw(spriteBatch);
            lancasterbutton.draw(spriteBatch);
            wwibutton.draw(spriteBatch);
            medicbutton.draw(spriteBatch);
            firstbutton.draw(spriteBatch);
            truckbutton.draw(spriteBatch);
            wallbutton.draw(spriteBatch);
            tommybutt.draw(spriteBatch);
            werdbutt4.draw(spriteBatch);
            werdbut1.draw(spriteBatch);
            werdbutt3.draw(spriteBatch);
            werdbutt2.draw(spriteBatch);
            uspbutt.draw(spriteBatch);
            mechbutt.draw(spriteBatch);
            slowbutt.draw(spriteBatch);
            enemybasehealth.draw(spriteBatch);
            basehealth.draw(spriteBatch);
            truckbutton2.draw(spriteBatch);
            jeepbutton2.draw(spriteBatch);
            truckbutton3.draw(spriteBatch);
            werdbutt5.draw(spriteBatch);
            fairleybutt.draw(spriteBatch);
            mouscitobutt.draw(spriteBatch);
            money.draw(spriteBatch);
            cursor.draw(spriteBatch);
            score.draw(spriteBatch);
            helibutt.draw(spriteBatch);
            halobutton.draw(spriteBatch);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].draw(spriteBatch);
                if (enemies[i].isshoot)
                {
                    enemies[i].DrawRange(spriteBatch);
                }
                //enemies[i].Drawhitbox(spriteBatch);
            }
            for (int i = 0; i < allies.Count; i++)
            {
                allies[i].draw(spriteBatch);
                //  allies[i].DrawRange(spriteBatch);
                if (allies[i].isshoot)
                {
                    allies[i].DrawRange(spriteBatch);
                }
            }
            for (int i = 0; i < medics.Count; i++)
            {
                medics[i].draw(spriteBatch);
            }
            if (enemycamplife <= 0)
            {
                win.draw(spriteBatch);
                ifwin = true;
            }
            if (dead == true)
            {
                blood.draw(spriteBatch);
                lose.draw(spriteBatch);
                iflose = true;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
