using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class TestPackagingContext: DbContext
    {//定义属性，便于外部访问数据表
        public DbSet<LogonPeson> LogonPesons { get { return Set<LogonPeson>(); } }

        public TestPackagingContext() : base("TestPackagingDbContext")//配置使用的连接名
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            var init = new SqliteDropCreateDatabaseWhenModelChanges<TestPackagingContext>(modelBuilder);
            Database.SetInitializer(init);
        }
    }
}
