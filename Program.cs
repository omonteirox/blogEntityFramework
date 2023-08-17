using blogEntityFramework.Data;
using blogEntityFramework.Models;

public class Program
{
    public static void Main()
    {
        using var ctx = new BlogDataContext();
        //ctx.Users.Add(new User
        //{
        //    Name = "Gustavo",
        //    Email = "gussmonteiro@gmail.com",
        //    PasswordHash = "123456",
        //    Image = "123456",
        //    Slug = "gustavo-monteiro",
        //    Bio = "Gustavo Monteiro",
        //});
        //ctx.SaveChanges();
        var user = ctx.Users.FirstOrDefault();
        var post = new Post
        {
            Author = user,
            Body = "Meu artigo",
            Category = new Category
            {
                Name = "Backend",
                Slug = "backend"
            },
            CreateDate = DateTime.Now,
            Slug = "meu-artigo",
            Summary = "Neste artigos vamos conferir...",
            Title = "Meu Artigo",
            //LastUpdateDate = DateTime.Now,
            //Tags = null
        };
        ctx.Posts.Add(post);
        ctx.SaveChanges();
    }
}