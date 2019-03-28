using System;
using System.Drawing;

namespace MyGame
{
    class Asteroid : BaseObject
    {
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        Image asteroid = Image.FromFile(@"Images\asteroid.png");

        /// <summary>
        /// Реализация метода базового класса. Вывод через буфер картинок астероида на устройство вывода графики
        /// </summary>
        public override void Draw()
        {            
            Game.Buffer.Graphics.DrawImage(asteroid, new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
        }

        /// <summary>
        /// Переопределение базового метода. Обновление позиции на экране
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width;            
        }
    }
}
