using Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleanTask.Domains;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using CleanTask.Tools;
using System.Web.Mvc;
using Infrastructure.Data.Entitties;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ProductRepositories : ProductRepository
    {
        private readonly UserManager<CleanTask.Domains.userapp> _userManager;
        private readonly SignInManager<CleanTask.Domains.userapp> _signInManager;
        private readonly MyDBContex _contex;
        private readonly IEmailSend _emailSend;
        private readonly IViewRenderService _viewRenderService;
        private ClaimsPrincipal User;

        public ProductRepository _prosuctRepository { get; }
        public ProductRepositories(ProductRepository productRepository, MyDBContex contex, UserManager<CleanTask.Domains.userapp> userManager, SignInManager<CleanTask.Domains.userapp> signInManager,IEmailSend emailsend)
        {
            _prosuctRepository = productRepository;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSend = emailsend;
        }
        public void CreateProduct([FromBody] CleanTask.Domains.productModel product)
        {
          
                product.ProduceDate = DateTime.Now;
                product.Is_A_Valiable = true;
                _contex.product.Add(product);
                _contex.SaveChanges();
            
        }

        public void DeleteProduct(int id)
        {
            var q = _contex.product.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                string userid;
                userid = _userManager.GetUserId(User);
                if (Convert.ToString(q.user.id) == userid)
                {
                    q.DeleteStatus = true;
                    _contex.SaveChanges();
                }
            }
        }

        public List<CleanTask.Domains.productModel> FilterProductByUserName(string username)
        {
            var q = _contex.product.Where(i => i.user.UserName == username).ToList();
            return q;
        }

        public List<CleanTask.Domains.productModel> ReadProductByUser(string username)
        {
            var q = _contex.product.Where(i => i.user.UserName == username).ToList();
            if (q != null)
            {
                return q;
            }
            return null;
        }

        public async Task<List<CleanTask.Domains.productModel>> ReadProductByUserNow()
        {
                string userid;
                int iduser;
                userid = _userManager.GetUserId(User);
                iduser = Convert.ToInt32(userid);
                var username = _contex.userme.Where(i => i.id == iduser).Select(i => i.UserName).FirstOrDefault();
                var q = _contex.product.Where(i => i.user.UserName == username).ToList();
                if (q != null)
                {
                    return q;
                }
                return null;
        }

        public void UpdateProduct(int id, [FromBody] CleanTask.Domains.productModel product)
        {
                string userid;
                userid = _userManager.GetUserId(User);
                int productuser;
                productuser = product.user.id;
                if (Convert.ToString(productuser) == userid)
                {
                    var q = _contex.product.Where(i => i.id == id).FirstOrDefault();
                    if (q != null)
                    {
                        q.Name = product.Name;
                        q.Price = product.Price;
                        q.Is_A_Valiable = product.Is_A_Valiable;
                        _contex.SaveChanges();
                    }
                }
        }

        public async Task<List<CleanTask.Domains.productModel>> ReadProduct()
        {
            var q = _contex.product.Where(i => i.DeleteStatus == false).ToList();
            return q;
        }


    }
}
