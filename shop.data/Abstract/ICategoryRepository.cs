using System.Collections.Generic;
using shop.entity;

namespace shop.data.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Category GetByIdWithProducts(int categoryId);

        void DeleteFromCategory(int productId,int categoryId);

        
    }
}