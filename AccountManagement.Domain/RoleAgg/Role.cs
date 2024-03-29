﻿using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }

        public bool IsDeleted { get; private set; }

        public List<Account> Accounts { get; private set; }

        public List<RolePermissions> RolePermissions { get; set; }

        public Role(string name)
        {
            Name = name;
            Accounts = new List<Account>();
            IsDeleted = false;
        }

        public void Edit(string name)
        {
            Name = name;
        }

        public void Remove()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
