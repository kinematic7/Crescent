using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService
{
    public class Settings
    {
        public static string ConnectionString { get; set; }

        public static string EncryptionKey { get; set; }

        public static int KeepTokenAliveInMinutes { get; set; }
    }
}
