// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using Oogarts.Data;
// using Oogarts.Domain.Users;
// using Oogarts.Domain;

namespace Services
{
    public class UserService
    {
        // private readonly ApplicationDbContext _context;

        // public UserService(ApplicationDbContext context)
        // {
        //     _context = context;
        // }

        // public List<User> GetAllUsers()
        // {
        //     return _context.Users.ToList();
        // }

        // public User? GetUserById(int id)
        // {
        //     return _context.Users.FirstOrDefault(u => u.Id == id);
        // }

        // public void CreateUser(User user)
        // {
        //     _context.Users.Add(user);
        //     _context.SaveChanges();
        // }

        // public void UpdateUser(int id, User updatedUser)
        // {
    
        //     User? existingUser = _context.Users.FirstOrDefault(e => e.Id == id);

        //     if (existingUser != null)
        //     {
        
        //         existingUser.TeamMemberId = updatedUser.TeamMemberId;
        //         existingUser.Email = updatedUser.Email;
        //         existingUser.PasswordHash = updatedUser.PasswordHash;
        //         existingUser.GoogleLoginId = updatedUser.GoogleLoginId;
        //         existingUser.TwoFactorAuthEnabled = updatedUser.TwoFactorAuthEnabled;
        
        //         existingUser.Updated();
        //         _context.SaveChanges();
        //     }
        // }
    }
}
