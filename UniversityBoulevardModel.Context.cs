﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegistrarConsole
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UniversityBoulevardEntities : DbContext
    {
        public UniversityBoulevardEntities()
            : base("name=UniversityBoulevardEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<FacultyMajor> FacultyMajors { get; set; }
        public virtual DbSet<FacultySchedule> FacultySchedules { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Requirement> Requirements { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentMajor> StudentMajors { get; set; }
        public virtual DbSet<StudentSchedule> StudentSchedules { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
    }
}
