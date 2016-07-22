using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrarConsole
{
    static class Program
    {
        /// <summary>
        /// A reference to the database context we are currently using for this session.
        /// </summary>
        public static UniversityBoulevardEntities Database;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
