using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Items.Commands.UpdateItemPicture
{
    public class UpdateItemPictureCommand: IRequest
    {
        public Guid ItemId { get; set; }

        public string MainItemPicture { get; set; }

    }
}
