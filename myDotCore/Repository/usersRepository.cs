using IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class usersRepository : TestRepositoryBase<users>, IusersRepository
    {
        public usersRepository(TestDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
