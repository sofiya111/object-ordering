using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лаб_9
{
    static class data
    {
        public static string Value { get; set; }
    }
    static class lis
    {
        public static List<double> Value { get; set; }
    }
    static class listvar
    {
        public static List<string> Value { get; set; }
    }
    static class listkrit
    {
        public static List<string> Value { get; set; }
    }
    static class listbest
    {
        public static List<double> Value { get; set; }
    }
    static class listbestKrit
    {
        public static List<double> Value { get; set; }
    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
