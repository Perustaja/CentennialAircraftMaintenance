using System;

namespace CAM.Web.ViewModels.Shared
{
    public class EmployeeViewModel
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Initials => $"{Char.ToUpper(FirstName[0])}{LastName}";
    }
}