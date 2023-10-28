﻿using Microsoft.AspNetCore.Mvc;
using IReturnNodePointerProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Ajax.Utilities;

namespace IReturnNodePointerProject.Controllers
{
	public class AdminController : Controller
    {
        private readonly ApplicationDbContext _storeContext;
        private static AdminListViewModel allLists;
        public AdminController(ApplicationDbContext context)
        {
			AdminListViewModel adminListViewModel = new AdminListViewModel();
			allLists = adminListViewModel;
            _storeContext = context;
        }
        public IActionResult AdminAccountsView()
        {
            var UserTable = _storeContext.User.AsQueryable();
            var PatronTable = _storeContext.Patrons.AsQueryable();
            var CheckoutTable = _storeContext.TO.AsQueryable();
			allLists.Admins = UserTable.Where(x => x.IsAdmin == true).ToList();
            allLists.Employees = UserTable.Where(x => x.IsAdmin != true).ToList();
            allLists.Users = PatronTable.ToList();
            allLists.UserData = CheckoutTable.ToList();
                
            return View(allLists);

        }
        [HttpGet]
        public IActionResult AddAccount()
        {
            return PartialView(new LoginViewModel());
        }
		[HttpGet]
		public IActionResult AddAccount(string userType)
        {
            if (userType.Equals("Patrons"))
            {
				ViewBag.View = "patron";
                Patrons patron = new Patrons();
                return PartialView(new LoginViewModel());
            }
            else
            {
                User user = new User();
				ViewBag.View = "company";
                return PartialView(new LoginViewModel());
            }
        }
        public IActionResult EditAccount(string userType , int UserID)
        {
            return PartialView();
        }
        public IActionResult DeleteUser(string userType, int UserID)
        {
            return PartialView();
        }
    }
}
public class AdminListViewModel
{
	public  List<User> Admins { get; set; }
	public  List<Patrons> Users { get; set; }
    public  List<TO> UserData { get; set; }
	public  List<User> Employees { get; set; }
}