using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Application.Order.AutoMapper
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDomainMapper());
                //cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
