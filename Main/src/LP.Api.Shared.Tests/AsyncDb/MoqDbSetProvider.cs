using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Moq;

namespace LP.Api.Shared.Tests.AsyncDb
{
    public class MoqDbSetProvider<T> where T : class, new()
    {
        public static Mock<DbSet<T>> GetInstance<TEntities>(TEntities entities) where TEntities : IEnumerable<T>
        {
            var moqDbSetProvider = new MoqDbSetProvider<T>();

            return moqDbSetProvider.DbSet(entities);
        }

        public Mock<DbSet<T>> DbSet<TEntities>(TEntities entities) where TEntities : IEnumerable<T>
        {
            var queryableEntities = entities.AsQueryable();

            var mockDbSet = new Mock<DbSet<T>>();

            mockDbSet.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(queryableEntities.GetEnumerator()));


            mockDbSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(queryableEntities.Provider));

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableEntities.Expression);
            
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableEntities.ElementType);
            
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableEntities.GetEnumerator());

            return mockDbSet;

        }

    }
}
