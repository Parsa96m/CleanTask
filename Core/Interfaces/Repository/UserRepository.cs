using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanTask.Domains;
using System.Web.Mvc;

namespace Core.Interfaces.Repository
{
    public interface UserRepository
    {
        public void Create(CleanTask.Domains.UserMe userme);
        public void Delete(int id);
        public void Update(int id , CleanTask.Domains.UserModel userme);
        public List<CleanTask.Domains.UserModel> Read();
    }
}
