using System.Collections.Generic;

namespace Essiq.Showroom.Server.Controllers
{
    public interface IPagedResult<TItem>
    {
        IEnumerable<TItem> Items { get; set; }
        long PageNumber { get; set; }
        long PageSize { get; set; }
        long TotalItems { get; set; }
    }
}
