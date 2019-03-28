using System;
using System.Windows.Forms;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            try
            {                
                form.Width = 1001;
                form.Height = 801;
                if (form.Width > 1000) throw new ArgumentOutOfRangeException("Превышена максимальная ширина экрана!");                
                if (form.Height > 800) throw new ArgumentOutOfRangeException("Превышена максимальная высота экрана!");                
            }
            catch (ArgumentOutOfRangeException ex)
            {                
                MessageBox.Show($"{ex.Message} Попытаемся исправить...");
                if (form.Width > 1000) form.Width = 1000;
                if (form.Height > 800) form.Height = 800;
                MessageBox.Show($"Установлены максимально возможные размеры экрана.");                
            }
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
