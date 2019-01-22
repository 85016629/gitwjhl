using AutoMapper;
using fx.Application.Order.ViewModels;
using fx.Domain.OrderContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Order.AutoMapper
{
    public class ViewModelToDomainMapper : Profile
    {
        public ViewModelToDomainMapper()
        {
            CreateMap<CreateOrderCommand, OrderViewModel>();

        }
    }
}
