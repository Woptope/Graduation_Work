using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DataBase.Repository;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateDBController : ControllerBase
    {
		private HomeworkRepository _HomeworkRepository;
		private AccountRepository _AccountRepository;
		private AccountTypeRepository _AccountTypeRepository;
		private ChildRepository _ChildRepository;
		private ChildInGroupRepository _ChildInGroupRepository;
		private EventTypeRepository _EventTypeRepository;
		private EventRepository _EventRepository;
		private EventGroupRepository _EventGroupRepository;
		private GroupRepository _GroupRepository;
		private HomeworkForGroupRepository _HomeworkForGroupRepository;
		private HomeworkTypeRepository _HomeworkTypeRepository;
		private MessageRepository _MessageRepository;
		private MessageTypeRepository _MessageTypeRepository;
		private ParentRepository _ParentRepository;
		private TeacherRepository _TeacherRepository;
		private UserRepository _UserRepository;
		private RatingHomeworkRepository _RatingHomeworkRepository;
		private RatingEventRepository _RatingEventRepository;


		public CreateDBController(HomeworkRepository homeworkRepository,
									AccountRepository accountRepository,
									AccountTypeRepository accountTypeRepository,
									ChildRepository childRepository,
									ChildInGroupRepository childInGroupRepository,
									EventTypeRepository eventTypeRepository,
									EventRepository eventRepository,
									EventGroupRepository eventGroupRepository,
									GroupRepository groupRepository,
									HomeworkForGroupRepository homeworkForGroupRepository,
									HomeworkTypeRepository homeworkTypeRepository,
									MessageRepository messageRepository,
									MessageTypeRepository messageTypeRepository,
									ParentRepository parentRepository,
									TeacherRepository teacherRepository,
									UserRepository userRepository,
									RatingHomeworkRepository ratingHomeworkRepository,
									RatingEventRepository ratingEventRepository)
		{
			_HomeworkRepository = homeworkRepository;
			_AccountRepository = accountRepository;
			_AccountTypeRepository = accountTypeRepository;
			_ChildInGroupRepository = childInGroupRepository;
			_ChildRepository = childRepository;
			_EventGroupRepository = eventGroupRepository;
			_EventRepository = eventRepository;
			_EventTypeRepository = eventTypeRepository;
			_GroupRepository = groupRepository;
			_HomeworkForGroupRepository = homeworkForGroupRepository;
			_HomeworkTypeRepository = homeworkTypeRepository;
			_MessageRepository = messageRepository;
			_MessageTypeRepository = messageTypeRepository;
			_ParentRepository = parentRepository;
			_TeacherRepository = teacherRepository;
			_UserRepository = userRepository;
			_RatingHomeworkRepository = ratingHomeworkRepository;
			_RatingEventRepository = ratingEventRepository;
		}

		[Route("", Name = "AddToDB"), HttpGet]
		public ActionResult AddDB()
		{
			//ТИПЫ АККАУНТОВ
			AccountType accountType1 = new AccountType
			{
				Type = "Admin"
			};
			AccountType accountType2 = new AccountType
			{
				Type = "Parent"
			};
			AccountType accountType3 = new AccountType
			{
				Type = "Teacher"
			};
			List<AccountType> accountTypes = new List<AccountType>() { accountType1, accountType2, accountType3 };
			foreach (var at in accountTypes)
			{
				_AccountTypeRepository.Add(at);
			}

			//АККАУНТЫ
			Account account1 = new Account
			{
				Login = "Admin",
				Password = "Admin",
				AccountType = accountType1
			};

			Account account2 = new Account
			{
				Login = "Parent1",
				Password = "parent",
				AccountType = accountType2
			};

			Account account3 = new Account
			{
				Login = "Parent2",
				Password = "wwww",
				AccountType = accountType2
			};

			Account account4 = new Account
			{
				Login = "Parent3",
				Password = "wwww",
				AccountType = accountType2
			};
			Account account5 = new Account
			{
				Login = "Parent4",
				Password = "wwww",
				AccountType = accountType2
			};

			Account account6 = new Account
			{
				Login = "Teacher1",
				Password = "teacher",
				AccountType = accountType3
			};


			Account account7 = new Account
			{
				Login = "Teacher2",
				Password = "teacher",
				AccountType = accountType3
			};

			List<Account> accounts = new List<Account>() { account1, account2, account3, account4, account5, account6, account7};
			foreach (var a in accounts)
			{
				_AccountRepository.Add(a);
			}

			//ПОЛЬЗОВАТЕЛИ
			User user0 = new User                            //Админ
			{
				Fio = "Администратор",
				Phone = "89343432123",
				Email = "test@mail.ru",
				Birthsday = new DateTime(2011, 6, 10),
				Account = account1
			};

			User user1 = new User                            //Родитель
			{
				Fio = "Иванов Иван Иванович",
				Phone = "89343432123",
				Email = "test@mail.ru",
				Birthsday = new DateTime(2011, 6, 10),
				Account = account2
			};

			User user2 = new User                            //Родитель
			{
				Fio = "Иванова Ксения Ивановна",
				Phone = "89343432123",
				Email = "test22@mail.ru",
				Birthsday = new DateTime(1988, 6, 10),
				Account = account3
			};

			User user3 = new User                            //Родитель
			{
				Fio = "Виханов Влад Иванович",
				Phone = "89343432123",
				Email = "test2772@mail.ru",
				Birthsday = new DateTime(1998, 6, 10),
				Account = account4
			};

			User user4 = new User                            //Родитель
			{
				Fio = "Виханова Влада Ивановна",
				Phone = "89343432123",
				Email = "testddd72@mail.ru",
				Birthsday = new DateTime(1974, 6, 9),
				Account = account5
			};

			User user5 = new User                            //Учитель
			{
				Fio = "Гавенас Влада Ивановна",
				Phone = "89343432123",
				Email = "testddd72@mail.ru",
				Birthsday = new DateTime(1974, 6, 9),
				Account = account6
			};

			User user6 = new User                            //Учитель
			{
				Fio = "Петрова Влада Ивановна",
				Phone = "89343432123",
				Email = "testddd72@mail.ru",
				Birthsday = new DateTime(1974, 6, 9),
				Account = account7
			};

			List<User> Users = new List<User>() { user0, user1, user2, user3, user4, user5, user6 };
			foreach (var a in Users)
			{
				_UserRepository.Add(a);
			}

			//УЧИТЕЛЯ
			Teacher teacher1 = new Teacher
			{
				Portfolio = "Porfolio бла бла",
				Experience = "5 лет",
				User = user5
			};

			Teacher teacher2 = new Teacher
			{
				Portfolio = "Porfolio2 бла бла",
				Experience = "10 лет",
				User = user6
			};

			List<Teacher> teachers = new List<Teacher>() { teacher1, teacher2 };
			foreach (var a in teachers)
			{
				_TeacherRepository.Add(a);
			}

			//РОДИТЕЛИ
			Parent parent1 = new Parent
			{
				LargeFamilie = true,
				User = user1
			};

			Parent parent2 = new Parent
			{
				LargeFamilie = true,
				User = user2
			};

			Parent parent3 = new Parent
			{
				LargeFamilie = false,
				User = user3
			};

			Parent parent4 = new Parent
			{
				LargeFamilie = false,
				User = user4
			};

			List<Parent> parents = new List<Parent>() { parent1, parent2, parent3, parent4 };
			foreach (var a in parents)
			{
				_ParentRepository.Add(a);
			}

			//ДЕТИ
			Child child1 = new Child
			{
				Birthsday =  new DateTime(1974, 6, 9),
				Fio = "Иванова Дарья Ивановна",
				Gender = "ж",
				Father = parent1,
				Mother = parent2
			};

			Child child2 = new Child
			{
				Birthsday = new DateTime(1974, 6, 9),
				Fio = "Иванов Влад Иванович",
				Gender = "м",
				Father = parent1,
				Mother = parent2
			};

			Child child3 = new Child
			{
				Birthsday = new DateTime(1974, 6, 9),
				Fio = "Виханов Влад Иванович",
				Gender = "м",
				Father = parent3,
				Mother = parent4
			};

			Child child4 = new Child
			{
				Birthsday = new DateTime(1974, 6, 9),
				Fio = "Виханова Влада Ивановна",
				Gender = "ж",
				Father = parent3,
				Mother = parent4
			};

			List<Child> children = new List<Child>() { child1, child2, child3, child4 };
			foreach (var a in children)
			{
				_ChildRepository.Add(a);
			}

			//ГРУППЫ
			Group group1 = new Group
			{
				Name = "Ранее развитие",
				Timetable = "Втоорник, четверг в 18:00",
				Description = "Уроки развития",
				Teacher = teacher1

			};

			Group group2 = new Group
			{
				Name = "Англ",
				Timetable = "Понедельник, четверг в 18:00",
				Description = "Уроки англа",
				Teacher = teacher2

			};
			List<Group> groups = new List<Group>() { group1, group2 };
			foreach (var a in groups)
			{
				_GroupRepository.Add(a);
			}

			//ДЕТИ В ГРУППАХ
			ChildInGroup childInGroup1 = new ChildInGroup
			{
				Child = child1,
				Group = group1

			};

			ChildInGroup childInGroup2 = new ChildInGroup
			{
				Child = child2,
				Group = group1

			};

			ChildInGroup childInGroup3 = new ChildInGroup
			{
				Child = child3,
				Group = group2

			};
				
			ChildInGroup childInGroup4 = new ChildInGroup
			{
				Child = child4,
				Group = group2
			};

			List<ChildInGroup> childInGroups = new List<ChildInGroup>() { childInGroup1, childInGroup2, childInGroup3, childInGroup4 };
			foreach (var a in childInGroups)
			{
				_ChildInGroupRepository.Add(a);
			}

			//ТИПЫ ДОМАШКИ
			HomeworkType homeworkType1 = new HomeworkType
			{
				Type = "Устно"
			};

			HomeworkType homeworkType2 = new HomeworkType
			{
				Type = "Письменно"
			};

			List<HomeworkType> homeworkTypes = new List<HomeworkType>() { homeworkType1, homeworkType2 };
			foreach (var a in homeworkTypes)
			{
				_HomeworkTypeRepository.Add(a);
			}

			//ДОМАШКА
			Homework homework1 = new Homework
			{
				Name = "Веселые цифры",
				Description = "test",
				HomeWorkDateTime = new DateTime(2020, 6, 9),
				LikesCount = 4,
				DisLikesCount = 2,
				LinkFile = "google.ru",
				Teacher = teacher1,
				HomeworkType = homeworkType1
			};

			Homework homework2 = new Homework
			{
				Name = "Скучные цифры",
				Description = "руки",
				HomeWorkDateTime = new DateTime(2020, 8, 9),
				LikesCount = 10,
				DisLikesCount = 20,
				LinkFile = "Yandex.ru",
				Teacher = teacher2,
				HomeworkType = homeworkType2
			};

			List<Homework> homeworks = new List<Homework>() { homework1, homework2 };
			foreach (var a in homeworks)
			{
				_HomeworkRepository.Add(a);
			}

			//ДОМАШКА ДЛЯ ГРУППЫ
			HomeworkForGroup homeworkForGroup1 = new HomeworkForGroup
			{
				Group = group1,
				Homework = homework1
			};

			HomeworkForGroup homeworkForGroup2 = new HomeworkForGroup
			{
				Group = group2,
				Homework = homework2
			};

			HomeworkForGroup homeworkForGroup3 = new HomeworkForGroup
			{
				Group = group2,
				Homework = homework1
			};

			List<HomeworkForGroup> homeworkForGroups = new List<HomeworkForGroup>() { homeworkForGroup1, homeworkForGroup2, homeworkForGroup3 };
			foreach (var a in homeworkForGroups)
			{
				_HomeworkForGroupRepository.Add(a);
			}

			//ТИП МЕРОПРИЯТИЯ
			EventType eventType1 = new EventType
			{
				Type = "Прогулка"
			};

			EventType eventType2 = new EventType
			{
				Type = "Праздник"
			};

			List<EventType> eventTypes = new List<EventType>() { eventType1, eventType2 };
			foreach (var a in eventTypes)
			{
				_EventTypeRepository.Add(a);
			}

			//МЕРОПРИЯТИЯ
			Event event1 = new Event
			{
				Name = "Веселые старты",
				Description = "идем бегать",
				EventDateTime = new DateTime(2020, 6, 9),
				LikesCount = 4,
				DisLikesCount = 2,
				EventType = eventType1
			};

			Event event2 = new Event
			{
				Name = "Скучныеы старты",
				Description = "идем считать",
				EventDateTime = new DateTime(2020, 6, 9),
				LikesCount = 4,
				DisLikesCount = 18,
				EventType = eventType2
			};

			List<Event> events = new List<Event>() { event1, event2 };
			foreach (var a in events)
			{
				_EventRepository.Add(a);
			}

			//УЧАСТНИКИ МЕРОПРИЯТИЯ
			EventGroup eventGroup1 = new EventGroup
			{
				Group = group1,
				Event = event1
			};

			EventGroup eventGroup2 = new EventGroup
			{
				Group = group2,
				Event = event2
			};

			EventGroup eventGroup3 = new EventGroup
			{
				Group = group1,
				Event = event2
			};

			List<EventGroup> eventGroups = new List<EventGroup>() { eventGroup1, eventGroup2, eventGroup3 };
			foreach (var a in eventGroups)
			{
				_EventGroupRepository.Add(a);
			}

			//ТИП СООБЩЕНИЯ
			MessageType messageType1 = new MessageType
			{
				Type = "Обратная связь"
			};

			MessageType messageType2 = new MessageType
			{
				Type = "Вопрос"
			};

			MessageType messageType3 = new MessageType
			{
				Type = "Ответ"
			};

			MessageType messageType4 = new MessageType
			{
				Type = "Комментарий"
			};

			List<MessageType> messageTypes = new List<MessageType>() { messageType1, messageType2, messageType3, messageType4 };
			foreach (var a in messageTypes)
			{
				_MessageTypeRepository.Add(a);
			}

			//СООБЩЕНИЯ
			Message message1 = new Message
			{
				MessageText = "ПРЭ",
				IsAnonymous = false,
				MessageDateTime = new DateTime(2020, 6, 9),
				User = user1,
				MessageType = messageType1,
				Event = event1
			};

			Message message2 = new Message
			{
				MessageText = "Хей",
				IsAnonymous = false,
				MessageDateTime = new DateTime(2020, 6, 9),
				User = user2,
				MessageType = messageType2,
				Event = event2
			};

			Message message3 = new Message
			{
				MessageText = "Хей",
				IsAnonymous = false,
				MessageDateTime = new DateTime(2020, 6, 9),
				User = user3,
				MessageType = messageType3,
				Homework = homework1
			};

			Message message4 = new Message
			{
				MessageText = "Я тут",
				IsAnonymous = false,
				MessageDateTime = new DateTime(2020, 6, 9),
				User = user4,
				MessageType = messageType3,
				Homework = homework2
			};

			List<Message> messages = new List<Message>() { message1, message2, message3, message4 };
			foreach (var a in messages)
			{
				_MessageRepository.Add(a);
			}

			//Лайки домашка
			RatingHomework ratingHomework1 = new RatingHomework
			{
				User = user1,
				Homework = homework1,
				Rating = true
			};

			RatingHomework ratingHomework2 = new RatingHomework
			{
				User = user2,
				Homework = homework2,
				Rating = true
			};

			RatingHomework ratingHomework3 = new RatingHomework
			{
				User = user1,
				Homework = homework2,
				Rating = true
			};

			RatingHomework ratingHomework4 = new RatingHomework
			{
				User = user2,
				Homework = homework1,
				Rating = false
			};

			RatingHomework ratingHomework5 = new RatingHomework
			{
				User = user3,
				Homework = homework1,
				Rating = false
			};

			RatingHomework ratingHomework6 = new RatingHomework
			{
				User = user3,
				Homework = homework2,
				Rating = false
			};

			List<RatingHomework> ratingHomeworks = new List<RatingHomework>() { ratingHomework1, ratingHomework2, ratingHomework3, ratingHomework4, ratingHomework5, ratingHomework6 };
			foreach (var a in ratingHomeworks)
			{
				_RatingHomeworkRepository.Add(a);
			}

			//Лайки мероприятие
			RatingEvent ratingEvent1 = new RatingEvent
			{
				User = user1,
				Event = event1,
				Rating = true
			};

			RatingEvent ratingEvent2 = new RatingEvent
			{
				User = user2,
				Event = event2,
				Rating = true
			};

			RatingEvent ratingEvent3 = new RatingEvent
			{
				User = user1,
				Event = event2,
				Rating = true
			};

			RatingEvent ratingEvent4 = new RatingEvent
			{
				User = user2,
				Event = event1,
				Rating = false
			};

			RatingEvent ratingEvent5 = new RatingEvent
			{
				User = user3,
				Event = event1,
				Rating = false
			};

			RatingEvent ratingEvent6 = new RatingEvent
			{
				User = user3,
				Event = event2,
				Rating = false
			};

			List<RatingEvent> ratingEvents = new List<RatingEvent>() { ratingEvent1, ratingEvent2, ratingEvent3, ratingEvent4, ratingEvent5, ratingEvent6 };
			foreach (var a in ratingEvents)
			{
				_RatingEventRepository.Add(a);
			}
			//var test = _ParentRepository.GetAll().Where(x => x.LargeFamilie == true);
			return Ok();
		}
	}
}