using BOL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StoryDB : IStoryDB
    {
        SSDBContext _context;
        public StoryDB(SSDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Approve(Story stry)
        {
            _context.Stories.Update(stry);
            var result =  await _context.SaveChangesAsync();
            if (result != 0)
                return true;
            else
                return false;
        }

        public IQueryable<Story> GetAll()
        {
            return _context.Stories;
        }

        public async Task<bool> Create(Story story)
        {
            _context.Add(story);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(int SSid)
        {
            var story = await _context.Stories.FindAsync(SSid);
            _context.Stories.Remove(story);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
                return true;
            else
                return false;
        }

        public IQueryable<Story> GetStoriesByStatus(bool IsApproved)
        {
            return _context.Stories.Where(x => x.IsApproved == IsApproved);
        }


        public IQueryable<Story> GetById(int SSid)
        {
            return _context.Stories.Where(x=> x.SSid == SSid);
        }

        public IQueryable<Story> GetByUserId(string Id)
        {
            return _context.Stories.Where(x => x.Id == Id);
        }

        public Task<bool> Update(Story story)
        {
            throw new NotImplementedException();
        }
    }
}
