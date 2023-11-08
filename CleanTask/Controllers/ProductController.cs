using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using CleanTask.Domains;
using CleanTask.Tools;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Infrastructure.Data.Entitties;
using Infrastructure.Data;

namespace CleanTask.Controllers
{
    [System.Web.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController //ControllerBase
    {
        private readonly UserManager<CleanTask.Domains.userapp> _userManager;
        private readonly SignInManager<CleanTask.Domains.userapp> _signInManager;
        private readonly MyDBContex _contex;
        //private readonly IEmailSend _emailSend;
        //private readonly IViewRenderService _viewRenderService;
        public ProductRepository _productRepository { get; }
        public ProductController(ProductRepository productRepository, MyDBContex contex, UserManager<CleanTask.Domains.userapp> userManager, SignInManager<CleanTask.Domains.userapp> signInManager)
        {
            _productRepository = productRepository;
            _contex = contex;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("api/User/CreateP")]
        public async Task<IActionResult> CreateProduct([FromBody] Domains.productModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   _productRepository.CreateProduct(product);
                }
                return CustomOk(true);
            }
            catch(Exception ex)
            {
                return CustomError(ex.Message);
            }
       
        }    

        [HttpPut("{id}")]
        [Authorize]
        [Route("api/User/Update")]
        public async Task<string> UpdateProduct(int id, [FromBody] Domains.productModel product)
        {
           if (ModelState.IsValid)
           {
               _productRepository.UpdateProduct(id, product);
               return "";
           }
            return "";
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<string> DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                _productRepository.DeleteProduct(id);
                return "";
            }
            return "";
        }

        [HttpPost("{username}")]
        [Route("api/User/ReadByUser")]
        public async Task<List<Domains.productModel>> ReadProductByUser(string username)
        {
            _productRepository.ReadProductByUser(username);
            return null;
        }
        public async Task<List<Domains.productModel>> FilterProductByUserName(string username)
        {
            if (ModelState.IsValid)
            {
                return _productRepository.FilterProductByUserName(username);
            }
            return null;
        }

        [HttpGet]
        [Route("api/User/ReadByUser")]
        [Authorize]
        public async Task<List<Domains.productModel>> ReadProductByUserNow()
        {
            if (ModelState.IsValid)
            {
                return await _productRepository.ReadProductByUserNow();
            }
            return null;
        }
        [HttpGet]
        [Route("api/User/ReadProduct")]
        public async Task<List<Domains.productModel>> ReadProduct()
        {
            if (!ModelState.IsValid)
            {
                return await _productRepository.ReadProduct();
            }
            return null;
        }


    }
}
