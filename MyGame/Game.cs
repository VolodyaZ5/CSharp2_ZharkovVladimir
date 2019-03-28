using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Media;

namespace MyGame
{   
    static class Game
    {
        //Высота и ширина игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        //Массив объектов фона        
        static List<BaseObject> objectInSpace = new List<BaseObject>();

        static Random rnd = new Random();

        //Объект класса пули
        static Bullet bullet;

        //Звук выстрела
        static SoundPlayer sp;

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        static Game()
        {
            bullet = new Bullet(new Point(0, rnd.Next(0, Game.Height)), new Point(5, 0), new Size(101, 65));
            sp = new SoundPlayer(@"Sounds\gun.wav");
        }
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
            bullet.Draw();
            Buffer.Render();
        }


        /// <summary>
        /// Обновление позиции на экране для каждого из элементов массива объектов фона
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in objectInSpace)
            {
                if (obj.Collision(bullet))
                {
                    //SystemSounds.Exclamation.Play();                    
                    sp.Play();
                }
                obj.Update();
            }
            bullet.Update();
        }
        

        /// <summary>
        /// Создает объекты фона
        /// </summary>
        public static void Load()
        {            
            for (int i = 0; i < 15; i++)
            {
                int r = rnd.Next(5, 50);

                switch (rnd.Next(0, 4))
                {
                    case 0: objectInSpace.Add(new Asteroid(new Point(800, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(20, 20))); break;
                    case 1: objectInSpace.Add(new Star(new Point(600, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(20, 20))); break;
                    case 2: objectInSpace.Add(new Sputnik(new Point(300, rnd.Next(0, Game.Height)), new Point(-r / 2, r), new Size(30, 25))); break;
                    case 3: objectInSpace.Add(new NyanCat(new Point(700, rnd.Next(0, Game.Height)), new Point(-r / 3, r), new Size(60, 25))); break;
                }
            }            
        }        
    }
}
