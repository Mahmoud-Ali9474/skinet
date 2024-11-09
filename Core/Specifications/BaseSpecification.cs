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

    public Expression<Func<TEntity, object>> OrderBy { get; private set; }

    public Expression<Func<TEntity, object>> OrderByDesending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public void AddInclude(Expression<Func<TEntity, object>> include)
    {
        Includes.Add(include);
    }
    public void AddOrderBy(Expression<Func<TEntity, object>> expression)
    {
        OrderBy = expression;
    }

    public void AddOrderByDesending(Expression<Func<TEntity, object>> expression)
    {
        OrderByDesending = expression;
    }
    public void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
