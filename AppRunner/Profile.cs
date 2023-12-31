using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner
{
    /// <summary>
    /// Stores information about the profile
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Name of the profile
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Applications from the profile
        /// </summary>
        public List<App> Applications { get; set; }

        /// <summary>
        /// Create a new profile with the given name
        /// </summary>
        /// <param name="name"></param>
        public Profile(String name) {
            this.Name = name;
            this.Applications = new List<App>();
        }

        /// <summary>
        /// Find an app
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public App FindApp(String name)
        {
            foreach (var item in Applications)
            {
                if(item.Name == name) return item;
            }
            return null;
        }

        /// <summary>
        /// Removes an app
        /// </summary>
        /// <param name="name"></param>
        public void RemoveApp(String name)
        {
            var app = this.FindApp(name);
            if(app != null) { 
                this.Applications.Remove(app);  
            }
        }
    }
}
