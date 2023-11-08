using CleanTask.Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Interfaces.Repository
{
    public interface ProductRepository
    {
         void CreateProduct([FromBody] CleanTask.Domains.productModel product);
         void UpdateProduct(int id, [FromBody] CleanTask.Domains.productModel product);
         void DeleteProduct(int id);
         List<CleanTask.Domains.productModel> ReadProductByUser(string username);
         List<CleanTask.Domains.productModel> FilterProductByUserName(string username);
         Task<List<CleanTask.Domains.productModel>> ReadProductByUserNow();
         Task<List<CleanTask.Domains.productModel>> ReadProduct();


    }
}
