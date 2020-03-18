using si2.dal.Context;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Repositories
{
    public class DataflowRepository : Repository<Dataflow>, IDataflowRepository
    {
        public DataflowRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
