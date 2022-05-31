﻿namespace ClientApp.Models.ApiModels.Output
{
    public class UserInfo
    {
        public Status Status { get; set; }
        public UserInfoParams Params { get; set; }
    }

    public class UserInfoParams
    {
        public User[] List { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Mobile { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int EmployeeEID { get; set; }
        public bool IsActive { get; set; }
    }
}