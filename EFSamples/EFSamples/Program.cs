using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();

            var userSet = context.Set<User>();

            // 添加
            var user = new User { Name = Guid.NewGuid().ToString() };
            userSet.Add(user);
            context.SaveChanges();

            // 修改
            user.Name = Guid.NewGuid().ToString();
            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

            // 保存
            userSet.Remove(user);
            context.SaveChanges();
        }
    }
}
