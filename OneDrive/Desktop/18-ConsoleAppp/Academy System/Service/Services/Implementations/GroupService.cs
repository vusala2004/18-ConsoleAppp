using Domain.Entities;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private Gr
        private int _count;
       
        public Group Create(Group group)
        {
           group.id = _count;
            _group.Cre
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Group GeyById(int id)
        {
            throw new NotImplementedException();
        }

        public Group Update(int id, Group group)
        {
            throw new NotImplementedException();
        }
    }
}
