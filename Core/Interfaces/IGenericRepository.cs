using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> FindByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> FindAllAsync();
    Task<IReadOnlyList<TEntity>> FindAsync(ISpecification<TEntity> spec);
    Task<TEntity> FindEntityWithSpec(ISpecification<TEntity> spec);
}
