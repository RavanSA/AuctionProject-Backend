namespace Application.Items.Commands.UpdateItem
{
    using System;
    using System.Collections.Generic;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class UpdateItemCommand : IRequest
    {

        public Guid Id { get; set; }

        public string MainItemPicture { get; set; }
    }
}