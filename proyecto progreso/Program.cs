using System;
using System.Windows.Forms;
using DataStats.Forms;

namespace DataStats
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.Run(new FormInicio());
        }
    }
}
