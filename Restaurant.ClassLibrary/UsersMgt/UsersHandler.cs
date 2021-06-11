using DemoClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.UsersMgt
{
    public class UsersHandler
    {

        public void Add(User user)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                //context.Entry(user.SecurityQuestion).State = EntityState.Unchanged;

                context.Entry(user.Gender).State = EntityState.Unchanged;
                context.Entry(user.Role).State = EntityState.Unchanged;
                context.Entry(user.City).State = EntityState.Unchanged;


                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void AddFeedback(Feedback feedback)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                context.Entry(feedback.User).State = EntityState.Unchanged;

                context.Feedbacks.Add(feedback);
                context.SaveChanges();
            }
        }
        public List<User> GetUsers()
        {
            PakClassifiedContext context = new PakClassifiedContext();            
            using (context)
            {
                return (from u in context.Users
                       
                        select u).ToList();                
            }
        }

        
        public User GetUser(string loginid, string password)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from u in context.Users
                        .Include("Role")
                        //.Include("Address.City.Province.Country")
                        where u.LoginId.Equals(loginid) && u.Password.Equals(password)
                        select u).FirstOrDefault();
            }
        }

        public User GetUserWithId(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from u in context.Users
                     .Include(a=>a.Gender)
                     .Include(a=>a.Image)
                     .Include(a=>a.City)
                     .Include(a=>a.City.Province)
                     .Include(a=>a.City.Province.Country)
                        where u.Id == id
                        select u).FirstOrDefault();
            }
        }

        public List<UserGender> GetGender()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from u in context.Genders
                        select u).ToList();
            }
        }

        public List<Country> GetCountries()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from u in context.Countries
                        select u).ToList();
            }
        }

        //public List<UserSecurityQuestion> GetSecurityQuestion()
        //{
        //    using (PakClassifiedContext context = new PakClassifiedContext())
        //    {
        //        return (from u in context.SecurityQuestions
        //                select u).ToList();
        //    }
        //}
        public List<Role> GetRoles()
        {
            PakClassifiedContext context = new PakClassifiedContext();
            using (context)
            {
                return (from u in context.Roles
                        select u).ToList();
            }
        }

        

    }
}
