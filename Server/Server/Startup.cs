using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Server.DataBase;
using Server.DataBase.Repository;
using Server.Models;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 
            services.AddRazorPages();
            var connectionString = Configuration["ConnectionStrings:MyConnection"];
            services.AddDbContext<DduContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IRepository<AccountType>, AccountTypeRepository>();          
            services.AddTransient<IRepository<Child>, ChildRepository>(); 
            services.AddTransient<IRepository<Account>, AccountRepository>();
            services.AddTransient<IRepository<ChildInGroup>, ChildInGroupRepository>();
            services.AddTransient<IRepository<Event>, EventRepository>();
            services.AddTransient<IRepository<EventGroup>, EventGroupRepository>();
            services.AddTransient<IRepository<EventType>, EventTypeRepository>();
            services.AddTransient<IRepository<Group>, GroupRepository>();
            services.AddTransient<IRepository<Homework>, HomeworkRepository>();
            services.AddTransient<IRepository<HomeworkForGroup>, HomeworkForGroupRepository>();
            services.AddTransient<IRepository<HomeworkType>, HomeworkTypeRepository>();
            services.AddTransient<IRepository<Message>, MessageRepository>();
            services.AddTransient<IRepository<MessageType>, MessageTypeRepository>();
            services.AddTransient<IRepository<Parent>, ParentRepository>();
            services.AddTransient<IRepository<Teacher>, TeacherRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<RatingHomework>, RatingHomeworkRepository>();
            services.AddTransient<IRepository<RatingEvent>, RatingEventRepository>();
            services.AddTransient<AccountRepository>();
            services.AddTransient<AccountTypeRepository>();
            services.AddTransient<ChildRepository>();
            services.AddTransient<ChildInGroupRepository>();
            services.AddTransient<EventRepository>();
            services.AddTransient<EventGroupRepository>();
            services.AddTransient<EventTypeRepository>();
            services.AddTransient<GroupRepository>();
            services.AddTransient<HomeworkRepository>();
            services.AddTransient<HomeworkForGroupRepository>();
            services.AddTransient<HomeworkTypeRepository>();
            services.AddTransient<MessageRepository>();
            services.AddTransient<MessageTypeRepository>();
            services.AddTransient<ParentRepository>();
            services.AddTransient<TeacherRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<RatingHomeworkRepository>();
            services.AddTransient<RatingEventRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(o => o.AllowAnyHeader());
            app.UseCors(o => o.AllowAnyOrigin());
            app.UseCors(o => o.AllowAnyMethod());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
