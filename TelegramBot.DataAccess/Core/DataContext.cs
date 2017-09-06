using System;
using System.Data.Entity;
using System.Reflection;

namespace TelegramBot.DataAccess.Core{
    public class DataContext : DbContext{
        public DataContext(string nameOrConnectionStrin) : base(nameOrConnectionStrin){
            Configuration.LazyLoadingEnabled = false;
        }

        public DataContext() : this("HiDoctor")
        {
        }

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class{
            return Set<TEntity>();
        }

        public virtual void MarkAsModified<TEntity>(TEntity instance) where TEntity : class{
            Entry(instance).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){
            if (modelBuilder == null){
                throw new ArgumentException("modelBuilder");
            }
            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("TelegramBot.DataAccess"));
        }
    }
}