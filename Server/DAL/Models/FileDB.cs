using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public class FileDB : FileCommon
    {
        public UserDB User { get; set; }
    }
}
