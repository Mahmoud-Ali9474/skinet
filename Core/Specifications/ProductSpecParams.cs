using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 6;
    public int PageIndex { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(PageSize));
            }
            _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
    public int? TypeId { get; set; }
    public int? BrandId { get; set; }
    public string Sort { get; set; }
    private string _searchTerm;
    public string SearchTerm
    {
        get => _searchTerm;
        set => _searchTerm?.ToLower();
    }

}
