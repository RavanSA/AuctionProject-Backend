﻿namespace Application.Items.Commands.CreateItem
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Models;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Http;    

    public class CreateItemCommand : IRequest<Response<ItemResponseModel>>, IMapWith<Item>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal StartingPrice { get; set; }

        public decimal MinIncrease { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid SubCategoryId { get; set; }

        public Guid CategoryId { get; set; }

        public String UserId { get; set; }
        


        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<CreateItemCommand, Item>()
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.StartTime.ToUniversalTime()))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.EndTime.ToUniversalTime()));
                
        }
    }
}