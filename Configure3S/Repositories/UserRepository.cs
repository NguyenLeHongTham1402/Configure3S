using Configure3S.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Configure3S.Repositories
{
    public class UserRepository
    {
        private Connfigure3SContext context;

        public TbUser Login(IFormCollection collection)
        {
            var user = new TbUser();

            string userID = collection["UserId"].ToString();
            string password = collection["Password"].ToString();

            using (context = new Connfigure3SContext())
            {
                try
                {
                    user = context.TbUsers.AsEnumerable().SingleOrDefault(x => x.UserId.Equals(userID, StringComparison.InvariantCulture) &&
                            x.Password.Equals(password, StringComparison.InvariantCulture) && x.IsActive);
                }
                catch (Exception){ }
            }

            return user;
        }
    }
}
