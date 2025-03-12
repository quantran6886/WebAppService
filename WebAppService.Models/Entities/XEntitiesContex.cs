using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAppService.Models.Entities
{
    public class XEntitiesContex : IdentityDbContext<IdentityUser>
    {
        public XEntitiesContex(DbContextOptions<XEntitiesContex> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Tùy chỉnh bảng Identity nếu cần
        }
    }
}
