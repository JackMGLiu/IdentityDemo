using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

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
                }
            };
        }
    }
}
