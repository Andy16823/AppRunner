using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner
{
    /// <summary>
    /// Stores information about the application name and path
    /// </summary>
    public class App
    {
        /// <summary>
        /// Name of the application
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Path of the application
        /// </summary>
        public String Path { get; set; }

        /// <summary>
        /// Creats an new application
        /// </summary>
        /// <param name="Path"></param>
        public App(String Path)
        {
            FileInfo fileInfo = new FileInfo(Path);
            this.Path = Path;
            this.Name = fileInfo.Name;
        }
    }
}
