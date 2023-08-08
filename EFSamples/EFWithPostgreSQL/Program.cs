using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWithPostgreSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();

            var userSet = context.Set<User>();

            var user = new User { Name = Guid.NewGuid().ToString() };
            userSet.Add(user);
            context.SaveChanges();

            user.Name = Guid.NewGuid().ToString();
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

            userSet.Remove(user);
            context.SaveChanges();
        }
    }
}
