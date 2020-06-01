using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
