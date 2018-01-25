using IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class usersApp
    {
        private readonly IusersRepository _repository;

        public usersApp(IusersRepository repository)
        {
            _repository = repository;
        }

        public List<users> GetALL()
        {
            return _repository.GetAllList();
        }

        public bool Add(users model)
        {
            return _repository.Insert(model);
        }

        public bool Update(users model)
        {
            return _repository.Update(model);
        }


        public users Find(int id)
        {
            return _repository.GetAllList().FindLast(o => o.id == id);
        }

        public async Task<bool> IsExisit(int user_id)
        {
            return await _repository.IsExistAsync(o => o.id == user_id);
        }
    }
}
