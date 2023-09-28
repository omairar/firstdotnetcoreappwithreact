using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStoryDB
    {
        IQueryable<Story> GetAll();
        IQueryable<Story> GetStoriesByStatus(bool IsApproved);
        IQueryable<Story> GetById(int SSid);
        IQueryable<Story> GetByUserId(string Id);
        Task<bool> Create(Story story);
        Task<bool> Update(Story story);
        Task<bool> Delete(int SSid);
        Task<bool> Approve(Story stry);
    }
}
