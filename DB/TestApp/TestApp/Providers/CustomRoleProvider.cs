﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TestApp.Models;

namespace TestApp.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        DatabaseWork db = new DatabaseWork();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            db.ConnectionString = "";
            User user = db.GetUser(username);
            if (user != null)
            {
                RoleOfUser role = db.GetRoleForUser(user.IdUser);
                if (role != null)
                    roles = new string[] { role.NameRole };
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool isUserInRole = false;
            db.ConnectionString = "";
            User user = db.GetUser(username);
            if(user!=null)
            {
                RoleOfUser role = db.GetRoleForUser(user.IdUser);
                if (role != null && role.NameRole == roleName)
                    isUserInRole = true;
            }
            return isUserInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}