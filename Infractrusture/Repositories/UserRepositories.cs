using CleanTask.Tools;
using Core.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;
using CleanTask.Domains;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepositories:UserRepository
    {
        private readonly MyDBContex _contex;
        public UserRepositories(MyDBContex contex)
        {
            _contex = contex;
        }
        public void Create(CleanTask.Domains.UserMe userme)
        {
            _contex.userme.Add(userme);
            _contex.SaveChangesAsync();
        }

    

        public void Delete(int id)
        {
            var q = _contex.userme.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.DeleteStatus = true;
                _contex.SaveChanges();
            }
        }
        public List<CleanTask.Domains.UserMe> Read()
        {
            var q = _contex.userme.Where(i => i.DeleteStatus == false).Select(i => i).ToList();
            return q;
        }

        public void Update(int id, CleanTask.Domains.UserMe userme)
        {
            var q = _contex.userme.Where(i => i.id == id).FirstOrDefault();
            if (q != null)
            {
                q.Name = userme.Name;
                q.UserName = userme.UserName;
                q.PhoneUser = userme.PhoneUser;
                q.Email = userme.Email;
                q.Password = userme.Password;
                _contex.SaveChanges();
            }  
        }

        public void Update(int id, CleanTask.Domains.UserModel userme)
        {
            throw new NotImplementedException();
        }

        List<CleanTask.Domains.UserModel> UserRepository.Read()
        {
            throw new NotImplementedException();
        }
    }
}
