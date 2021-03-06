﻿using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Id4ServerTest
{
    public class Config
    {
        //所有可以访问的Resource
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","API Application")
            };
        }

        //客户端
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "mvc",
                    AllowedGrantTypes = GrantTypes.Implicit, //模式：最简单的模式
                    ClientSecrets =
                    {
                        //私钥
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        //可以访问的Resource
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId
                        //IdentityServerConstants.StandardScopes.Email
                    },
                    RedirectUris = {"http://localhost:5001/signin-oidc"}, //跳转登录到的客户端的地址
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback-oidc"}, //跳转登出到的客户端的地址
                    RequireConsent = false //是否需要用户点击确认进行跳转
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
                    SubjectId = "10000",
                    Username = "liujian",
                    Password = "123qweA"
                }
            };
        }

        //定义系统中的资源
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}