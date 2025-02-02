﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Categories.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public Guid CategoryId { get; set; }

        public class Handler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IMapper _mapper;

            #endregion
            
            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion
            
            #region IRequestHandler<GetCategoryByIdQuery, CategoryDto>

            public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.Category
                    .Where(c => c.CategoryId == request.CategoryId)
                    .ProjectToSingleOrDefaultAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}