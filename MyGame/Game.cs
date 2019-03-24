using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MyGame
{   
    static class Game
    {
        //Высота и ширина игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        //Массив объектов фона
        //public static BaseObject[] _objs;
        static List<BaseObject> objectInSpace = new List<BaseObject>();

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        static Game() { }
        public static void Init(Form form)
        {
            //Устройство для вывода графики
            Graphics g;

            //Предоставляет доступ к главному буферу графического контекста для 
            //текущего приложения
            _context = BufferedGraphicsManager.Current;

            //Создаем объект и связываем его с формой
            g = form.CreateGraphics();

            //Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;


            //Связываем буфер в памяти с графическим объектом, чтобы рисовать в памяти
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();            

            Timer timer = new Timer { Interval = 80 };
            timer.Start();
            timer.Tick += (s, e) => { Draw(); Update(); };
        }

        
        /// <summary>
        /// Вывод через буфер на устройство вывода графики
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objectInSpace)
                obj.Draw();            
            Buffer.Render();            
        }

        /// <summary>
        /// Обновление позиции на экране для каждого из элементов массива объектов фона
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in objectInSpace)
            {
                obj.Update();
            }            
        }

        /// <summary>
        /// Создает объекты фона
        /// </summary>
        public static void Load()
        {
            Random r = new Random();
            for (int i = 0; i < 15; i++)
            {
                switch (r.Next(0, 4))
                {
                    case 0: objectInSpace.Add(new Asteroid(new Point(0, i * 40), new Point(2 * i, i), new Size(20, 20))); break;
                    case 1: objectInSpace.Add(new Star(new Point(600, i * 40), new Point(i, 0), new Size(20, 20))); break;
                    case 2: objectInSpace.Add(new Sputnik(new Point(300, i * 40), new Point(i, 0), new Size(30, 25))); break;
                    case 3: objectInSpace.Add(new NyanCat(new Point(700, i * 40), new Point(i, 15 + i), new Size(60, 25))); break;
                }
            }            
        }        
    }
}
