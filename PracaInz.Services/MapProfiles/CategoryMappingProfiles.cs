using AutoMapper;
using PracaInz.ViewModels.CategoryViewModels;
using PracaInz.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.Services.MapProfiles
{
    public class CategoryMappingProfiles : Profile
    {

        public CategoryMappingProfiles()
        {
            // Due to the fact that all properties in both classess are the same, we don't have to create any special maps
            CreateMap<Category, CategoryListItemViewModel>();
            
        }
        
    }
}
