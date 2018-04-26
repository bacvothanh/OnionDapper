using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure
{
    public class ApplicationConstant
    {
        public const string DebugMail = "bacvothanh1988@gmail.com";
        public const string ApplicationName = "Dapper";
        public const string DefaultAvatar = "/App_Themes/admin/plugins/images/users/agent.jpg";

        public class CacheName
        {
            public const string Categories = "categories";
            public const string Tags = "tags";
        }

        public class Role
        {
            public const string Admin = "admin";
            public const string User = "user";
        }

        public class Folder
        {
            public static string FolderImageVideo = ConfigurationManager.AppSettings["FolderImageVideo"];
        }

        public class Facebook
        {
#if DEBUG
            public static string AppId = ConfigurationManager.AppSettings["FacebookAppIdTest"];
            public static string AppSecret = ConfigurationManager.AppSettings["FacebookAppSecretTest"];
#else
            public static string AppId = ConfigurationManager.AppSettings["FacebookAppId"];
            public static string AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
#endif
        }

    }
}
