using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhotoGallery.API.Models;

namespace PhotoGallery.API.Data
{
	public class AppDbContext: IdentityDbContext<AppUser>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

		public DbSet<Photo> Photos { get; set; }
		public DbSet<Gallery> Galleries { get; set; }
		public DbSet<PhotoInGallery> PhotoInGallery { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			
			builder.Entity<PhotoInGallery>().HasKey(x => new {x.GalleryId, x.PhotoId});
		}
	}
}
