using Hangfire.MySql;
using MySqlConnector;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemHttpApiModule),
        typeof(SharedHostingMicroserviceModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AutomaticElectricitySystemEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAccountWebModule),
        typeof(AutomaticElectricitySystemApplicationModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpCachingStackExchangeRedisModule)
    )]
    public class AutomaticElectricitySystemHttpApiHostModule : AbpModule
    {
        private readonly IConfigurationRoot _configuration;
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ConfigureCache(context);
            ConfigureSwaggerServices(context);
            ConfigureJwtAuthentication(context, configuration);
            ConfigureMiniProfiler(context);
            ConfigureIdentity(context);
            ConfigureAuditLog(context);
            ConfigurationSignalR(context);
            ConfigureCors(context);
            // ConfigureHangfire(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseAbpRequestLocalization();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseMiniProfiler();
            app.UseRouting();
            // app.UseCors(AutomaticElectricitySystemHttpApiHostConst.DefaultCorsPolicyName);
            app.UseCors("MyAllowSpecificOrigins");
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/AutomaticElectricitySystem/swagger.json", "AutomaticElectricitySystem API");
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();

            app.UseUnitOfWork();
            app.UseConfiguredEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); });
            // app.UseCors("MyAllowSpecificOrigins");
            app.UseHangfireDashboard();
            // app.UseHangfireServer();
            // RecurringJob.AddOrUpdate(()=>Console.WriteLine("测试定时任务执行"),Cron.Minutely);
        }
        private void ConfigurationSignalR(ServiceConfigurationContext context)
        {
            var redisConnection = context.Services.GetConfiguration()["Redis:Configuration"];

            if (redisConnection.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException(message: "Redis连接字符串未配置.");
            }

            context.Services.AddSignalR().AddStackExchangeRedis(redisConnection, options => { options.Configuration.ChannelPrefix = "Lion.AbpPro"; });
        }
        /// <summary>
        /// 配置MiniProfiler
        /// </summary>
        private void ConfigureMiniProfiler(ServiceConfigurationContext context)
        {
            context.Services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();
        }

        /// <summary>
        /// 配置JWT
        /// </summary>
        private void ConfigureJwtAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters()
                        {
                            // 是否开启签名认证
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            //ClockSkew = TimeSpan.Zero,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.ASCII.GetBytes(configuration["Jwt:SecurityKey"]))
                        };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = currentContext =>
                        {
                            var path = currentContext.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/login"))
                            {
                                return Task.CompletedTask;
                            }

                            var accessToken = string.Empty;
                            if (currentContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
                            {
                                accessToken = currentContext.HttpContext.Request.Headers["Authorization"];
                                if (!string.IsNullOrWhiteSpace(accessToken))
                                {
                                    accessToken = accessToken.Split(" ").LastOrDefault();
                                }
                            }

                            if (accessToken.IsNullOrWhiteSpace())
                            {
                                accessToken = currentContext.Request.Query["access_token"].FirstOrDefault();
                            }

                            if (accessToken.IsNullOrWhiteSpace())
                            {
                                accessToken = currentContext.Request.Cookies[AutomaticElectricitySystemHttpApiHostConst.DefaultCookieName];
                            }

                            currentContext.Token = accessToken;
                            currentContext.Request.Headers.Remove("Authorization");
                            currentContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");

                            return Task.CompletedTask;
                        }
                    };
                });
        }

   

        /// <summary>
        /// Redis缓存
        /// </summary>
        private void ConfigureCache(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedCacheOptions>(
                options => { options.KeyPrefix = "AutomaticElectricitySystem:"; });
            var configuration = context.Services.GetConfiguration();
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "AutomaticElectricitySystem-Protection-Keys");
        }



        /// <summary>
        /// 配置Identity
        /// </summary>
        private void ConfigureIdentity(ServiceConfigurationContext context)
        {
            context.Services.Configure<IdentityOptions>(options => { options.Lockout = new LockoutOptions() { AllowedForNewUsers = false }; });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    // 文件下载类型
                    options.MapType<FileContentResult>(() => new OpenApiSchema() { Type = "file" });

                    options.SwaggerDoc("AutomaticElectricitySystem",
                        new OpenApiInfo { Title = "YuannishaAutomaticElectricitySystem API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.EnableAnnotations(); // 启用注解
                    options.DocumentFilter<HiddenAbpDefaultApiFilter>();
                    options.SchemaFilter<EnumSchemaFilter>();
                    // 加载所有xml注释，这里会导致swagger加载有点缓慢
                    var xmlPaths = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                    foreach (var xml in xmlPaths)
                    {
                        options.IncludeXmlComments(xml, true);
                    }

                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                        new OpenApiSecurityScheme()
                        {
                            Description = "直接在下框输入JWT生成的Token",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.Http,
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            BearerFormat = "JWT"
                        });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });

                    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Name = "Accept-Language",
                        Description = "多语言设置，系统预设语言有zh-Hans、en，默认为zh-Hans",
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                            },
                            Array.Empty<string>()
                        }
                    });
                });
        }


    
        /// <summary>
        /// 审计日志
        /// </summary>
        private void ConfigureAuditLog(ServiceConfigurationContext context)
        {
            Configure<AbpAuditingOptions>
            (
                options =>
                {
                    options.IsEnabled = true;
                    options.EntityHistorySelectors.AddAllEntities();
                    options.ApplicationName = "Yuannisha.AutomaticElectricitySystem";
                }
            );

            Configure<AbpAspNetCoreAuditingOptions>(
                options =>
                {
                    options.IgnoredUrls.Add("/AuditLogs/page");
                    options.IgnoredUrls.Add("/hangfire/stats");
                    options.IgnoredUrls.Add("/cap");
                });
        }

        private void ConfigureCors(ServiceConfigurationContext context)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:44315/","http://localhost:4200","http://localhost:44315/hangfire/stats")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        // private void ConfigureHangfire(ServiceConfigurationContext context)
        // {
        //     if (HangfireCommonConsts.HangfireDashboardEnabled)
        //     {
        //         //Hangfire(Enable to use Hangfire instead of default job manager)
        //         context.Services.AddHangfire(config =>
        //         {
        //             config.UseStorage(new MySqlStorage(
        //                 new MySqlConnection("server=localhost;database=YuannishaAutomaticElectricitySystemDB;uid=root;pwd=1q2w3E*;Allow User Variables=true;SslMode=None"),
        //                 new MySqlStorageOptions
        //                 {
        //                     PrepareSchemaIfNecessary= true,
        //                     // TablesPrefix = "Hangfire",
        //                     // QueuePollInterval = TimeSpan.FromMicroseconds(500),
        //                     // TransactionTimeout = TimeSpan.FromSeconds(30)
        //                 }));
        //         });
        //     }
        // }
        
    }
}