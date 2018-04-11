using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPServerV2.Library.Models
{
    public class FileResponse
    {
        public FileResponse(String filename)
        {
            this.filename = filename;
            FileInfo info = new FileInfo(filename);
            creationTime = info.CreationTime;
        }

        private String filename;
        private DateTime creationTime;
        public String Filename { get { return Path.GetFileName(filename); } }
        public String FullFilename { get { return filename; }  }
        public String Directory { get { return Path.GetDirectoryName(filename); }  }
        public DateTime CreationTime { get { return creationTime; } }
    }
}
