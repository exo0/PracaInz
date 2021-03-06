﻿using AutoMapper;
using PracaInz.BLL;
using PracaInz.DAL.EF;
using PracaInz.ViewModels.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracaInz.Services
{
    public class CategoryServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CategoryListViewModel GetAllCategories()
        {
            var vm = new CategoryListViewModel
            {
                Categories = _context.Categories.Select(x=> new CategoryListItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title
                })
            };
            return vm;
        }

        public IList<Category> ReturnAllCategoryToDropDown()
        {
            var cat = _context.Categories.ToList();

            IList<Category> categories = new List<Category>();

            foreach (var category in cat)
            {
                categories.Add(new Category
                {
                    Id = category.Id,
                    Title = category.Title
                });
            }

            return categories;
        }

        public CategoryListItemViewModel GetCategory(int id)
        {
            var cat = _context.Categories
                .Where(b => b.Id == id)
                .FirstOrDefault();
            var vm1 = _mapper.Map<CategoryListItemViewModel>(cat);
            return vm1;
        }


        public void UpdateCategory(int id, string title)
        {
            var catINDB = _context.Categories
                .Where(b => b.Id == id)
                .FirstOrDefault();
            catINDB.Title = title;
            _context.Categories.Update(catINDB);
            _context.SaveChanges();
        }

        public void Add(string Title)
        {
            var cat = new Category
            {
                Title = Title

            };

            _context.Categories.Add(cat);
            _context.SaveChanges();
        }

        public void DeleteCat(int id)
        {
            var cat = _context.Categories.Find(id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
        }
    }
}
