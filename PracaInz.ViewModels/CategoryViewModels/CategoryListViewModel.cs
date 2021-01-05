using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.ViewModels.CategoryViewModels
{
    public class CategoryListViewModel
    {
        public IEnumerable<CategoryListItemViewModel> Categories { get; set; }
    }
}
