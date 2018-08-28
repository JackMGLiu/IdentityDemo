using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServerCenter
{
    public class Config
    {
        //所有可以访问的Resource
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "My Api")
            };
        }

        //客户端
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //模式：最简单的模式
                    ClientSecrets =
                    {
                        //私钥
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        //可以访问的Resource
                        "api"
                    }
                },
                new Client()
                {
                    ClientId = "pwdClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //用户密码模式
                    ClientSecrets = new[] {new Secret("mysecret123qweASD".Sha256())},
                    //如果信任的第三方，不想加入密码，可以在授权服务的Config.cs中Client添加设置RequireClientSecret=false即可
                    //RequireClientSecret=false,
                    AllowedScopes = {"api"}
                }
            };
        }

        //测试用户
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "liujian",
                    Password = "123456"
                }
            };
        }
    }
}
