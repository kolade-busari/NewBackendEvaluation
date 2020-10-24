using AutoMapper;
using IntegraAdmin.Core.Models;
using IntegraAdmin.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegraAdmin.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Domain object to Resource
            CreateMap<Product, ProductResource>();
            CreateMap<Sponsor, SponsorResource>();
            CreateMap<ApplicationUser, ReadUserResource>();
            //CreateMap<Make, KeyValuePairResource>();
            //CreateMap<Model, KeyValuePairResource>();
            //CreateMap<Customer, SaveCustomerResource>()
            //    .ForMember(v => v.Contact, opt => opt.MapFrom(vr => new ContactResource { Name = vr.ContactName, Email = vr.ContactEmail, Phone = vr.ContactPhone }))
            //    .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(f => f.FeatureId)));
            CreateMap<Customer, ReadCustomerResource>()
                .ForMember(v => v.Products, opt => opt.MapFrom(vr => vr.Products.Select(x => x.Product)));

            //// From Resource to Domain object
            CreateMap<ProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SponsorResource, Sponsor>()
                .ForMember(s => s.Id, opt => opt.Ignore());
            CreateMap<SaveCustomerResource, Customer>()
              .ForMember(c => c.Id, opt => opt.Ignore())
              .ForMember(c => c.Products, opt => opt.Ignore())
              .AfterMap((cr, c) =>
              {
                  // Remove unselected products
                  var removedProducts = c.Products.Where(f => !cr.Products.Contains(f.ProductId)).ToList();
                  foreach (var f in removedProducts)
                      c.Products.Remove(f);

                  // Add new product
                  var addedProducts = cr.Products.Where(id => !c.Products.Any(f => f.ProductId == id)).Select(id => new CustomerProduct { ProductId = id }).ToList();
                  foreach (var f in addedProducts)
                      c.Products.Add(f);
              });
        }

    }
}
