using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoLibrary
{
    public class LibraryMetadata
    {
        public LibraryMetadata(string name, string coverPicPath)
        {
            this.Name = name;
            this.CoverPicPath = coverPicPath;
        }

        public string Name { get; set; }

        public string CoverPicPath { get; set; }
    }
}
