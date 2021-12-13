using System;
using System.Collections.Generic;

namespace Collections
{
    public class User : IUser
    {
        public User(string fullName, string username, uint? age)
        {
            Age = age;
            FullName = fullName;
            Username = username;
        }

        public uint? Age { get; }

        public string FullName { get; }

        public string Username { get; }

        public bool IsAgeDefined => Age != null;

        // TODO implement missing methods (try to autonomously figure out which are the necessary methods)
        private sealed class AgeFullNameUsernameEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Age == y.Age && x.FullName == y.FullName && x.Username == y.Username;
            }

            public int GetHashCode(User obj)
            {
                return HashCode.Combine(obj.Age, obj.FullName, obj.Username);
            }
        }

        public static IEqualityComparer<User> AgeFullNameUsernameComparer { get; } = new AgeFullNameUsernameEqualityComparer();
    }
}
