using System;
using System.Drawing;

namespace MyGame
{
    class NyanCat : BaseObject
    {
        public NyanCat(Point pos, Point dir, Size size) : base(pos, dir, size){}

        /// <summary>
        /// Переопределение базового метода. Вывод через буфер картинок nyanCat на устройство вывода графики
        /// </summary>
        public override void Draw()
        {
            Image nyanCat = Image.FromFile("nyanCat.png");
            Game.Buffer.Graphics.DrawImage(nyanCat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Переопределение базового метода. Обновление позиции на экране
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width) Pos.X = 0;
        }
    }
}
