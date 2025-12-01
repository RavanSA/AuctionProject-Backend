using AutoMapper;
using Common.AutoMapping.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Common.AutoMapping.Profiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            ConfigureMapping();
        }

        private void ConfigureMapping()
        {
            // Sadece APPLICATION katmanındaki mapping class-ları yükle
            var assembly = Assembly.GetExecutingAssembly();

            var allTypes = assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToArray();

            // IMapWith<T> implemente edenler (tek yön — Entity → DTO)
            var withMappings = allTypes
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>))
                    .Select(i => new { DTO = t, Entity = i.GetGenericArguments()[0] })
                )
                .ToArray();

            foreach (var map in withMappings)
            {
                // Entity → DTO (default)
                CreateMap(map.Entity, map.DTO);
            }

            // Custom mappingleri çalıştır
            var customMappings = allTypes
                .Where(t => typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>();

            foreach (var mapping in customMappings)
            {
                mapping.ConfigureMapping(this);
            }
        }
    }
}
