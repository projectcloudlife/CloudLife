using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public class UserDB
    {
        public int Id { get; set; }
        public AuthInfo AuthInfo { get; set; }
        public List<FileDB> Files { get; set; }
    }
}
