using System;
using System.Drawing;

namespace MyGame
{
    class Asteroid : BaseObject
    {
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size){}

        /// <summary>
        /// Переопределение базового метода. Вывод через буфер картинок астероида на устройство вывода графики
        /// </summary>
        public override void Draw()
        {
            Image asteroid = Image.FromFile("asteroid.png");
            Game.Buffer.Graphics.DrawImage(asteroid, new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
        }

        /// <summary>
        /// Переопределение базового метода. Обновление позиции на экране
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;

            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;

            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
