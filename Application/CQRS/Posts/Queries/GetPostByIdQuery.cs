﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Posts.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Posts.Queries
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetPostByIdQuery, PostDto>
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
            
            #region IRequestHandler<GetPostByIdQuery, PostDto>

            public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.Post
                    .Where(p => p.PostId == request.PostId)
                    .ProjectToSingleOrDefaultAsync<PostDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}