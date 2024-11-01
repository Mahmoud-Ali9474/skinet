using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
{
    public BaseSpecification()
    {

    }
    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<TEntity, bool>> Criteria { get; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

    public void AddInclude(Expression<Func<TEntity, object>> include)
    {
        Includes.Add(include);
    }
}
