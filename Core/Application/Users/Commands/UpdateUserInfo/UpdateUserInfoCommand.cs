using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Users.Commands.UpdateUserInfo
{
    public class UpdateUserInfoCommand : IRequest
    {
        public string Id { get; set; }

        public string PhoneNumber {get;set;}

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Birthday { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string ProfilePicture { get; set; }

        public string Title { get; set; }


    }
}
