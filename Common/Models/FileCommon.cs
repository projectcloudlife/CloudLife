using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class FileCommon
    {
        /**
         * remarks Contains file extantion (ex: test.txt)
         **/
        public string Name { get; set; }

        public int SizeInBytes { get; set; }
        /**
         * value not null only when downloading or uploading the file
         **/
        public byte[] Data { get; set; }

    }
}
