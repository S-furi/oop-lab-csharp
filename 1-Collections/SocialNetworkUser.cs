using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {
        private Dictionary<string, List<TUser>> groups;
        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
            FollowedUsers = new List<TUser>();
            groups = new Dictionary<string, List<TUser>>();
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if (FollowedUsers.Contains(user))
            {
                return false;
            }
            FollowedUsers.Add(user);
            List<TUser> existing;
            if (!groups.TryGetValue(group, out existing))
            {
                existing = new List<TUser>();
                groups[group] = existing;
            }
            existing.Add(user);
            return true;
        }

        public IList<TUser> FollowedUsers { get; }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            List<TUser> list;
            if (!groups.TryGetValue(group, out list))
            {
                return new List<TUser>();
            }
            return list;
        }
    }
}
