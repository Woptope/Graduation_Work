using System;
using System.Collections.Generic;

namespace Client.Models.ServerModels
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            RatingHomework = new HashSet<RatingHomework>();
        }

        public Guid Id { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthsday { get; set; }
        public Guid AccountId { get; set; }

        public  Account Account { get; set; }
        public  Parent Parent { get; set; }
        public  Teacher Teacher { get; set; }
        public  ICollection<Message> Messages { get; set; }
        public ICollection<RatingHomework> RatingHomework { get; set; }
        public ICollection<RatingEvent> RatingEvent { get; set; }
    }
}
