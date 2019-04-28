using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class FileCommon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        /**
         * remarks contains file extantion (ex: test.txt)
         **/
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool InRecycleBin { get; set; }
        public DateTime UploadDate { get; set; }
        public int SizeInBytes { get; set; }
        public bool IsPublic { get; set; }
        /**
         * value not null only when downloading or uploading the file
         **/
        public byte[] Data { get; set; }

    }
}
