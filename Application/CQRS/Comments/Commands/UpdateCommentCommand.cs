﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Comments.Commands
{
    public class UpdateCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }

        public class Handler : IRequestHandler<UpdateCommentCommand>
        {
            #region Fields

            private readonly IXNewsDbContext _context;

            #endregion
            
            #region Constructors

            public Handler(IXNewsDbContext context)
            {
                _context = context;
            }

            #endregion
            
            #region IRequestHandler<UpdateCommentCommand>

            public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                Comment comment = await _context.Comment.FindAsync(request.CommentId)
                    .ConfigureAwait(false);
                if (comment == null)
                {
                    throw new NotFoundException();
                }

                UpdateCommentProperties(comment, request);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Updates <paramref name="comment"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="comment">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="comment"/> parameter</param>
            private void UpdateCommentProperties(Comment comment, UpdateCommentCommand request)
            {
                comment.Content = request.Content;
            }

            #endregion
        }
    }
}