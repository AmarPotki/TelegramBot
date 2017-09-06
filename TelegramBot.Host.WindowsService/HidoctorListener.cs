using System;
using System.ServiceProcess;
using System.Threading;
using Autofac;
using TelegramBot.Business.Bootstrapper.DependencyResolution;
using TelegramBot.Business.Telegram;
namespace TelegramBot.Host.WindowsService{
    public partial class HidoctorListener : ServiceBase{
        private readonly IContainer _container;
        private readonly TelegramListener _telegram;
        public HidoctorListener(){
            _container = IoC.Initialize();
            _telegram = _container.Resolve<TelegramListener>();
            InitializeComponent();
        }
        protected override void OnStart(string[] args){
            try{
                _telegram.Start(); 
                
            }
            catch (Exception e){
              Thread.Sleep(new TimeSpan(0,0,5,0));
                OnStart(null);
            }
           
        }
        protected override void OnStop(){
            _telegram.Stop();
        }
        public void StartDebug(){
            OnStart(null);
        }
    }
}