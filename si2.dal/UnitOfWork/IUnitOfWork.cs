﻿using si2.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDataflowRepository Dataflows { get; }

        IUniversityRepository Universities { get; }

        IInstitutionRepository Institutions { get; }

        IBookRepository Books { get; }

        ICategoryRepository Categories { get; }

        IBookCategoryRepository BookCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);

        int SaveChanges();

        public void Dispose();
    }
}
