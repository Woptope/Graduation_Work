using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataBase
{
    public class DduContext : DbContext
    {
        public DduContext(DbContextOptions<DduContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<ChildInGroup> ChildInGroups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventGroup> EventGroups { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<HomeworkForGroup> HomeworkForGroups { get; set; }
        public DbSet<HomeworkType> HomeworkTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageType> MessageTypes { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RatingHomework> RatingHomeworks { get; set; }
        public DbSet<RatingEvent> RatingEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<AccountType>().ToTable("AccountType");
            modelBuilder.Entity<Child>().ToTable("Child");
            modelBuilder.Entity<ChildInGroup>().ToTable("ChildInGroup");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<EventGroup>().ToTable("EventGroup");
            modelBuilder.Entity<EventType>().ToTable("EventType");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<HomeworkForGroup>().ToTable("HomeworkForGroup");
            modelBuilder.Entity<HomeworkType>().ToTable("HomeworkType");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<MessageType>().ToTable("MessageType");
            modelBuilder.Entity<Parent>().ToTable("Parent");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<RatingHomework>().ToTable("RatingHomework");
            modelBuilder.Entity<RatingEvent>().ToTable("RatingEvent");



            modelBuilder.Entity<Account>(entity =>
            {

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId);
            });


            modelBuilder.Entity<Child>(entity =>
            {

                entity.HasOne(d => d.Father)
                    .WithMany(p => p.FathersChildren)
                    .HasForeignKey(d => d.FatherId);

                entity.HasOne(d => d.Mother)
                    .WithMany(p => p.MothersChildren)
                    .HasForeignKey(d => d.MotherId);
            });

            modelBuilder.Entity<ChildInGroup>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.ChildId });

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.ChildInGroups)
                    .HasForeignKey(d => d.ChildId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ChildrenInGroup)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<Event>(entity =>
            {

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventTypeId);
            });

            modelBuilder.Entity<EventGroup>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.GroupId });

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventGroups)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.EventsGroup)
                    .HasForeignKey(d => d.GroupId);
            });

            modelBuilder.Entity<Group>(entity =>
            {

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.TeacherId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {

                entity.HasOne(d => d.HomeworkType)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.HomeworkTypeId);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.TeacherId);
            });

            modelBuilder.Entity<HomeworkForGroup>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.HomeworkId });

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.HomeworkForGroup)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.HomeworkForGroups)
                    .HasForeignKey(d => d.HomeworkId);
            });


            modelBuilder.Entity<Message>(entity =>
            {

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.HomeworkId);

                entity.HasOne(d => d.MessageType)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageTypeId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId);
            });


            modelBuilder.Entity<Parent>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithOne(p => p.Parent)
                    .HasForeignKey<Parent>(d => d.Id);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.Id);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithOne(p => p.Account)
                    .HasForeignKey<User>(d => d.AccountId);
            });

            modelBuilder.Entity<RatingHomework>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.HomeworkId });

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.RatingHomework)
                    .HasForeignKey(d => d.HomeworkId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RatingHomework)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<RatingEvent>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.EventId });

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.RatingEvent)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RatingEvent)
                    .HasForeignKey(d => d.UserId);
            });
        }
    }
}
