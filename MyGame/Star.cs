using System;
using System.Drawing;

namespace MyGame
{
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) {}

        /// <summary>
        /// Переопределение базового метода. Вывод через буфер картинок звездочки на устройство вывода графики
        /// </summary>
        public override void Draw()
        {
            Image star = Image.FromFile("star.png");
            Game.Buffer.Graphics.DrawImage(star, new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
        }
        /// <summary>
        /// Переопределение базового метода. Обновление позиции на экране
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width;            
        }
    }
}
