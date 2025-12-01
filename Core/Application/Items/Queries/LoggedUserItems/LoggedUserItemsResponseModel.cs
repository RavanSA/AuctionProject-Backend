using Application.Common.Models;
using Application.Items.Queries.List;
using Application.Pictures;
using Common.AutoMapping.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Items.Queries.LoggedUserItems
{


    public class LoggedUserItemsResponseModel : IMapWith<Item>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal MinIncrease { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public string SubCategoryId { get; set; }

        public string CategoryId { get; set; }

        public string MainItemPicture { get; set; }

        public ICollection<PictureResponseModel> Pictures { get; set; }
    }
}
