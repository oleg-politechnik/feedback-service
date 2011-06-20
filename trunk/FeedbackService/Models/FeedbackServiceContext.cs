using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedbackService.Models;

namespace FeedbackService.Models
{
    public class FeedbackServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<FeedbackService.Models.FeedbackServiceContext>());

        public DbSet<Client> Clients { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackType> FeedbackTypes { get; set; }
        public DbSet<FeedbackVote> FeedbackVotes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public bool VoteForFeedback(Feedback feedback, string ip)
        {
            IEnumerable<FeedbackVote> votes = feedback.Votes.Where(v => v.IpAddress == ip);
            if (votes.Count() == 0)
            {
                feedback.Votes.Add(new FeedbackVote { IpAddress = ip });
                //if (IsUp)
                    feedback.Rating++;
                //else
                //    feedback.Rating--;
                return true;
            }
            else// if (votes.First().IsUp == IsUp)
            {
                this.FeedbackVotes.Remove(votes.First());
                //if (IsUp)
                    feedback.Rating--;
                //else
                //    feedback.Rating++;
                return false;
            }
        }

        //public Client CurrentClient() { return Clients.ClientForCurrentUser(); }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<FeedbackTypeProxy>().HasKey(fs => new { fs.SiteId, fs.FeedbackTypeId });
        //}

        /*   1. 
   2.   ) { 
   3.   .Entity<ConferenceTrack>().HasKey(
   4.     ct => ct.TrackId); 
   5. }
         
    public class StoreRepository
    {
        
        StoreDataContext db = new StoreDataContext();

        //
        // разные методы чтения (выборки)

        public IQueryable<Department> FindAllDepartments()
        {
            return db.Department;
        }

        public IQueryable<Product> FindAllProducts()
        {
            return db.Product;
        }

        public IQueryable<Product> FindProductsByDepartment(int departmentID)
        {
            return db.Product.Where(p => p.DepartmentID == departmentID);
        }

        public Department GetDepartment(int id)
        {
            return db.Department.SingleOrDefault(d => d.DepartmentID == id);
        }

        public Product GetProduct(int id)
        {
            return db.Product.SingleOrDefault(p => p.ProductID == id);
        }

        public IQueryable<Order> FindAllOrders(string username)
        {
            return db.Order.Where(o => o.UserName == username & o.IsShoppingCart == false);
        }

        public Order GetOrder(string username, int id)
        {
            return db.Order.SingleOrDefault(o => o.UserName == username & o.IsShoppingCart == false & o.OrderID == id);
        }

        public Order UNSAFEGetOrder(int id)
        {
            return db.Order.SingleOrDefault(o => o.IsShoppingCart == false & o.OrderID == id);
        }

        public IQueryable<Order> UNSAFEFindOrders()
        {
            return db.Order.Where(o => o.IsShoppingCart == false);
        }

        public Order GetShoppingCart(string username)
        {
            return db.Order.Where(sc => sc.UserName == username & sc.IsShoppingCart == true).SingleOrDefault();
        }

        public OrderItem GetShoppingCartProduct(string username, int productId)
        {
            return db.OrderItem.Where(op => op.Order.UserName == username & op.ProductID == productId & op.Order.IsShoppingCart == true).SingleOrDefault();
        }

        public IQueryable<OrderItem> FindAllOrderItems(string username)
        {
            var orderitems = from orderitem in db.OrderItem
                             where orderitem.Order.UserName == username
                             select orderitem;
            return orderitems;
        }

        //
        // Insert/Delete Methods

        public void Add(Department department)
        {
            db.Department.InsertOnSubmit(department);
        }

        public void Add(Product product)
        {
            db.Product.InsertOnSubmit(product);
        }

        public void Add(Order order)
        {
            db.Order.InsertOnSubmit(order);
        }

        public void Add(OrderItem orderitem)
        {
            db.OrderItem.InsertOnSubmit(orderitem);
        }

        public void Delete(Department department)
        {
            db.Department.DeleteOnSubmit(department);
        }

        public void Delete(Product product)
        {
            db.Product.DeleteOnSubmit(product);
        }

        public void Delete(OrderItem orderitem)
        {
            db.OrderItem.DeleteOnSubmit(orderitem);
        }

        //
        // Пока не вызовем, транзакция не завершится

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}


         */
    }
}
